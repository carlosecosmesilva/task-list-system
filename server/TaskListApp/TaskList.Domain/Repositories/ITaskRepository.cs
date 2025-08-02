using TaskList.Domain.Entities;

namespace TaskList.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetTaskByIdAsync(int id);
        Task<TaskItem?> GetTaskByNameAsync(string name);
        Task AddTaskAsync(TaskItem task);
        Task UpdateTaskAsync(TaskItem task);
        Task DeleteTaskAsync(int id);
        Task<TaskItem?> GetByDisplayOrderAsync(int displayOrder);
        Task<int> GetNextDisplayOrderAsync();
    }
}