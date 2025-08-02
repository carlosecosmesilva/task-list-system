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
            return await _dbContext.Set<TaskItem>().ToListAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _dbContext.Set<TaskItem>().FindAsync(id);
        }

        public async Task<TaskItem?> GetTaskByNameAsync(string name)
        {
            return await _dbContext.Set<TaskItem>().FirstOrDefaultAsync(t => t.Name == name);
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

        public async Task<int> GetNextDisplayOrderAsync()
        {
            var maxOrder = await _dbContext.Set<TaskItem>().MaxAsync(t => (int?)t.DisplayOrder);
            return (maxOrder ?? 0) + 1;
        }
    }
}