using System.Collections.Generic;
using SimpleTodoApi.Dtos;
using SimpleTodoApi.Models;

namespace SimpleTodoApi.Data
{
    public interface ITodoRepository {
        IEnumerable<Todo> GetAllTodos();
        Todo GetTodoById(int id);
        Todo CreateTodo(CreateTodoDto todoCreateDto);

        Todo UpdateTodo(UpdateTodoDto updateTodoDto);

        Todo DeleteTodo(int id);
        bool SaveChanges();
    }
    
}