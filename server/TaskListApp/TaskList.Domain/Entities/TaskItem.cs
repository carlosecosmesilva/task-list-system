namespace TaskList.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public DateTime DueDate { get; set; }
        public int DisplayOrder { get; set; }

        public static implicit operator bool(TaskItem? v)
        {
            throw new NotImplementedException();
        }
    }
}