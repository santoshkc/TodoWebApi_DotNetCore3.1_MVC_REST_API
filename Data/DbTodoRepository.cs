using System;
using System.Collections.Generic;
using System.Linq;
using SimpleTodoApi.Dtos;
using SimpleTodoApi.Models;

namespace SimpleTodoApi.Data
{
    public class DbTodoRepository : ITodoRepository
    {
        private readonly TodoContext _todoContext;
        public DbTodoRepository(TodoContext todoContext) {
            _todoContext = todoContext;
        }
        
        public Todo CreateTodo(CreateTodoDto todoCreateDto)
        {
            var todo = new Todo {
                DueDate = DateTime.Now.AddDays(7),
                IsCompleted = todoCreateDto.IsCompleted.HasValue ? todoCreateDto.IsCompleted.Value : false,
                Name = todoCreateDto.Name
            };
            _todoContext.Todos.Add(todo);
            return todo;
        }

        public Todo DeleteTodo(int id)
        {
            var query = _todoContext.Todos.Where(item => item.Id == id);
            if(query.Any()) {
                var itemToDelete = query.First();
                _todoContext.Remove(itemToDelete);
                return itemToDelete;
            }
            return null;
        }

        public IEnumerable<Todo> GetAllTodos()
        {
            return _todoContext.Todos.AsQueryable();
        }

        public Todo GetTodoById(int id)
        {
            var todo = _todoContext.Todos.Where(x => x.Id == id);
            if(todo.Any())
                return todo.First();
            return null;
        }

        public bool SaveChanges()
        {
            return _todoContext.SaveChanges() >= 0;
        }

        public Todo UpdateTodo(UpdateTodoDto updateTodoDto)
        {
            var todos = _todoContext.Todos.Where(x => x.Id == updateTodoDto.Id);
            if(todos.Any()) {
                var todo = todos.First();

                todo.Name = updateTodoDto.Name;
                if(updateTodoDto.IsCompleted.HasValue)
                    todo.IsCompleted = updateTodoDto.IsCompleted.Value;
                
                _todoContext.Update(todo);

                return todo;
            }
            return null;
        }
    }

}