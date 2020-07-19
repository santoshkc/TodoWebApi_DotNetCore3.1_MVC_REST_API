using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleTodoApi.Models
{
    public class Todo {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public bool IsCompleted { get; set; }
        
        public DateTime? DueDate {get;set;}
    }
}