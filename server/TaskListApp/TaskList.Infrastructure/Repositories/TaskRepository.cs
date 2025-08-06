using Microsoft.EntityFrameworkCore;
using TaskList.Domain.Entities;
using TaskList.Domain.Repositories;
using TaskList.Infrastructure.Persistence;

namespace TaskList.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _dbContext;

        public TaskRepository(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            return await _dbContext.Set<TaskItem>()
                .OrderBy(t => t.DisplayOrder)
                .ToListAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _dbContext.Set<TaskItem>().FindAsync(id);
        }

        public async Task<TaskItem?> GetTaskByNameAsync(string name)
        {
            return await _dbContext.Set<TaskItem>()
                .FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            await _dbContext.Set<TaskItem>().AddAsync(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            _dbContext.Set<TaskItem>().Update(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await GetTaskByIdAsync(id);
            if (task != null)
            {
                _dbContext.Set<TaskItem>().Remove(task);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<TaskItem?> GetByDisplayOrderAsync(int displayOrder)
        {
            return await _dbContext.Set<TaskItem>()
                .FirstOrDefaultAsync(t => t.DisplayOrder == displayOrder);
        }

        public async Task<int> GetNextDisplayOrderAsync()
        {
            var maxOrder = await _dbContext.Set<TaskItem>()
                .MaxAsync(t => (int?)t.DisplayOrder);

            return (maxOrder ?? 0) + 1;
        }

        /// <summary>
        /// Troca as ordens de duas tarefas.
        /// Garante que as tarefas sejam atualizadas corretamente no banco de dados.
        /// Utiliza uma transação para garantir a atomicidade da operação.
        /// Se uma das tarefas não for encontrada, lança uma exceção.
        /// Se ocorrer um erro durante a troca, reverte a transação.
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task SwapTaskOrdersAsync(TaskItem task1, TaskItem task2)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                // Busca TODAS as tarefas em ordem
                var allTasks = await _dbContext.Set<TaskItem>()
                    .AsNoTracking() // Importante: não rastrear para evitar conflitos
                    .OrderBy(t => t.DisplayOrder)
                    .ThenBy(t => t.Id)
                    .ToListAsync();

                // Encontra posições das tarefas a trocar
                var index1 = allTasks.FindIndex(t => t.Id == task1.Id);
                var index2 = allTasks.FindIndex(t => t.Id == task2.Id);

                if (index1 == -1 || index2 == -1)
                    throw new InvalidOperationException("Uma das tarefas não foi encontrada");

                // Troca posições na lista
                (allTasks[index1], allTasks[index2]) = (allTasks[index2], allTasks[index1]);

                // ESTRATÉGIA: Atualiza um por vez para evitar conflitos
                for (int i = 0; i < allTasks.Count; i++)
                {
                    var newOrder = i + 1;
                    var taskId = allTasks[i].Id;

                    // Atualiza diretamente no banco, uma tarefa por vez
                    await _dbContext.Database.ExecuteSqlRawAsync(
                        "UPDATE \"Tarefas\" SET \"DisplayOrder\" = {0} WHERE \"Id\" = {1}",
                        newOrder, taskId);
                }

                await transaction.CommitAsync();

                // Limpa o contexto para forçar reload das entidades
                _dbContext.ChangeTracker.Clear();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new InvalidOperationException($"Erro ao trocar ordens: {ex.Message}", ex);
            }
        }

        // Método auxiliar para reordenar todas as tarefas se necessário
        public async Task ReorderAllTasksAsync()
        {
            var tasks = await _dbContext.Set<TaskItem>()
                .OrderBy(t => t.DisplayOrder)
                .ThenBy(t => t.Id)
                .ToListAsync();

            for (int i = 0; i < tasks.Count; i++)
            {
                await _dbContext.Database.ExecuteSqlRawAsync(
                    "UPDATE \"Tarefas\" SET \"DisplayOrder\" = {0} WHERE \"Id\" = {1}",
                    i + 1, tasks[i].Id);
            }

            _dbContext.ChangeTracker.Clear();
        }
    }
}