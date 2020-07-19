using System.ComponentModel.DataAnnotations;

namespace SimpleTodoApi.Dtos
{
    public class UpdateTodoDto {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name {get; set;}
        
        [Required]
        public bool? IsCompleted {get;set;}
    }
}