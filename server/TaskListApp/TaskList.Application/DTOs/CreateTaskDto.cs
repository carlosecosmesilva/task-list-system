namespace TaskList.Application.DTOs;

public class CreateTaskDto
{
    public string Name { get; set; }
    public double Cost { get; set; }
    public DateTime DueDate { get; set; }
}