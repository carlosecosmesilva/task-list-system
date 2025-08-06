using TaskList.Application.DTOs;
using TaskList.Domain.Entities;
using TaskList.Domain.Repositories;

namespace TaskList.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllTasksAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetTaskByIdAsync(id);
        }

        public async Task<TaskItem> CreateTaskAsync(CreateTaskDto createTaskDto)
        {
            // Verificar se já existe uma tarefa com o mesmo nome
            var existingTask = await _taskRepository.GetTaskByNameAsync(createTaskDto.Name);
            if (existingTask != null)
            {
                throw new InvalidOperationException($"Uma tarefa com o nome '{createTaskDto.Name}' já existe.");
            }

            // Obter próxima ordem de exibição
            var nextOrder = await _taskRepository.GetNextDisplayOrderAsync();

            var task = new TaskItem
            {
                Name = createTaskDto.Name,
                Cost = createTaskDto.Cost,
                DueDate = createTaskDto.DueDate,
                DisplayOrder = nextOrder
            };

            await _taskRepository.AddTaskAsync(task);
            return task;
        }

        public async Task<TaskItem?> UpdateTaskAsync(int id, UpdateTaskDto updateTaskDto)
        {
            var existingTask = await _taskRepository.GetTaskByIdAsync(id);
            if (existingTask == null)
            {
                return null;
            }

            // Verificar se o novo nome já existe (exceto para a tarefa atual)
            if (existingTask.Name != updateTaskDto.Name)
            {
                var taskWithSameName = await _taskRepository.GetTaskByNameAsync(updateTaskDto.Name);
                if (taskWithSameName != null && taskWithSameName.Id != id)
                {
                    throw new InvalidOperationException($"Uma tarefa com o nome '{updateTaskDto.Name}' já existe.");
                }
            }

            existingTask.Name = updateTaskDto.Name;
            existingTask.Cost = updateTaskDto.Cost;
            existingTask.DueDate = updateTaskDto.DueDate;

            await _taskRepository.UpdateTaskAsync(existingTask);
            return existingTask;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null) return false;

            await _taskRepository.DeleteTaskAsync(id);
            return true;
        }

        public async Task<bool> MoveTaskAsync(int id, string direction)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null) return false;

            int targetOrder = direction.ToLower() switch
            {
                "up" => task.DisplayOrder - 1,
                "down" => task.DisplayOrder + 1,
                _ => task.DisplayOrder
            };

            if (targetOrder == task.DisplayOrder || targetOrder < 1) return false;

            var neighbor = await _taskRepository.GetByDisplayOrderAsync(targetOrder);
            if (neighbor == null) return false;

            // Solução: Utilização de valor temporário para evitar conflito de índice único
            await _taskRepository.SwapTaskOrdersAsync(task, neighbor);

            return true;
        }
    }
}