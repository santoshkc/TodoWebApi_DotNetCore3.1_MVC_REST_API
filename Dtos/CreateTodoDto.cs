using System.ComponentModel.DataAnnotations;

namespace SimpleTodoApi.Dtos
{
    public class CreateTodoDto {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public bool? IsCompleted { get; set; }
    }
}