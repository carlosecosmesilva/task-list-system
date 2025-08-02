using System.Collections.Generic;
using System.Threading.Tasks;
using TaskList.Application.DTOs;
using TaskList.Domain.Entities;

namespace TaskList.Application.Services;

public interface ITaskService
{
    Task<List<TaskItem>> GetAllTasksAsync();
    Task<TaskItem?> GetTaskByIdAsync(int id);
    Task<TaskItem> CreateTaskAsync(CreateTaskDto createTaskDto);
    Task<TaskItem?> UpdateTaskAsync(int id, UpdateTaskDto updateTaskDto);
    Task<bool> DeleteTaskAsync(int id);
}
