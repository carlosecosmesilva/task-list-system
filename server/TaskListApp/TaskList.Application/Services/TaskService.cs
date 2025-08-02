using TaskList.Domain.Entities;
using TaskList.Domain.Repositories;

namespace TaskList.Application.Services;

public class TaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TaskItem>> GetAllTasksAsync()
        => await _repository.GetAllTasksAsync();

    public async Task<TaskItem?> GetTaskByIdAsync(int id)
        => await _repository.GetTaskByIdAsync(id);

    public async Task<bool> AddTaskAsync(string name, double cost, DateTime dueDate)
    {
        if (await _repository.GetTaskByNameAsync(name) != null)
            return false;

        int order = await _repository.GetNextDisplayOrderAsync();
        var task = new TaskItem
        {
            Name = name,
            Cost = cost,
            DueDate = dueDate,
            DisplayOrder = order
        };

        await _repository.AddTaskAsync(task);
        return true;
    }

    public async Task<bool> UpdateTaskAsync(int id, string newName, double newCost, DateTime newDueDate)
    {
        var task = await _repository.GetTaskByIdAsync(id);
        if (task == null)
            return false;

        if (task.Name != newName && await _repository.GetTaskByNameAsync(newName) != null)
            return false;

        task.Name = newName;
        task.Cost = newCost;
        task.DueDate = newDueDate;

        await _repository.UpdateTaskAsync(task);
        return true;
    }

    public async Task<bool> DeleteTaskAsync(int id)
    {
        await _repository.DeleteTaskAsync(id);
        return true;
    }
}
