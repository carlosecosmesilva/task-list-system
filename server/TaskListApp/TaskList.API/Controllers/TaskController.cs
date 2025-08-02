using Microsoft.AspNetCore.Mvc;
using TaskList.Application.DTOs;
using TaskList.Application.Services;
using TaskList.Domain.Entities;

namespace TaskList.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    #region Variables
    private readonly ITaskService _taskService;
    #endregion

    #region Constructor
    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }
    #endregion

    #region Methods
    /// <summary>
    /// Obtém todas as tarefas.
    /// Retorna uma lista de tarefas com uma resposta 200 OK.
    /// Se não houver tarefas, retorna uma lista vazia.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<List<TaskItem>>> GetAllTasks()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    /// <summary>
    /// Obtém uma tarefa pelo ID.
    /// Retorna uma resposta 404 Not Found se a tarefa não existir.
    /// Retorna a tarefa encontrada com uma resposta 200 OK.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetTaskById(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    /// <summary>
    /// Cria uma nova tarefa.
    /// Retorna uma resposta 201 Created com a localização da nova tarefa.
    /// Se uma tarefa com o mesmo nome já existir, retorna uma resposta 409 Conflict.
    /// </summary>
    /// <param name="createTaskDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<TaskItem>> CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
        bool created = await _taskService.CreateTaskAsync(createTaskDto);
        if (!created)
            return Conflict("Uma tarefa com esse nome já existe.");

        return CreatedAtAction(nameof(GetAllTasks), null);
    }

    /// <summary>
    /// Atualiza uma tarefa existente pelo ID.
    /// Retorna uma resposta 404 Not Found se a tarefa não existir.
    /// Retorna uma resposta 204 No Content se a atualização for bem-sucedida.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updateTaskDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<TaskItem>> UpdateTask(int id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        var updatedTask = await _taskService.UpdateTaskAsync(id, updateTaskDto);
        if (updatedTask == null)
            return Conflict("Falha ao atualizar: tarefa não encontrada ou nome já em uso.");

        return NoContent();
    }

    /// <summary>
    /// Exclui uma tarefa pelo ID.
    /// Retorna uma resposta 404 Not Found se a tarefa não existir.
    /// Retorna uma resposta 204 No Content se a exclusão for bem-sucedida.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var result = await _taskService.DeleteTaskAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
    #endregion
}