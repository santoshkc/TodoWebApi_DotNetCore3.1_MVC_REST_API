namespace SimpleTodoApi.Dtos
{
    public class ReadTodoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted {get; set;}
        public bool IsExpired {get; set;}
    }
    
}