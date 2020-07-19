using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SimpleTodoApi.Data;
using SimpleTodoApi.Dtos;
using SimpleTodoApi.Models;

namespace SimpleTodoApi.Controllers
{
    [ApiController]
    [Route("/api/todos")]
    public class TodosController : ControllerBase {
        private readonly ITodoRepository _todoRepository;
        public TodosController(ITodoRepository todoRepository) {
            _todoRepository = todoRepository;
        }

        // GET /api/todos
        [HttpGet]
        public ActionResult<IEnumerable<ReadTodoDto>> GetAllTodos() {
            
            var li = _todoRepository.GetAllTodos();

            var todos = li.Select( todo => MapTodoToReadTodoDto(todo));

            return Ok(todos);
        }

        private static ReadTodoDto MapTodoToReadTodoDto(Todo todo) {
            if(todo == null) {
                return null;
            }

            return new ReadTodoDto {
                Id = todo.Id,
                IsCompleted = todo.IsCompleted,
                Name = todo.Name,
                IsExpired =  todo.DueDate.HasValue == false 
                    || DateTime.Now >= todo.DueDate.Value
            };
        }

        // GET /api/todos/{id}
        [HttpGet("{id}")]
        public ActionResult<ReadTodoDto> GetTodoById(int id) {
            var todo = _todoRepository.GetTodoById(id);

            if(todo == null) {
                return NotFound();
            }
            return Ok(MapTodoToReadTodoDto(todo));
        }

        // POST /api/todos
        [HttpPost]
        public ActionResult<ReadTodoDto> CreateTodoItem(CreateTodoDto todoCreateDto) {
            var createdTodo = _todoRepository.CreateTodo(todoCreateDto);

            if(_todoRepository.SaveChanges()) {

                return CreatedAtAction(nameof(GetTodoById),
                    new {
                        Id = createdTodo.Id
                        }, 
                    MapTodoToReadTodoDto(createdTodo));
            }
            return BadRequest();
        }

        // DELETE /api/todos/{id}
        [HttpDelete("{id}")]
        public ActionResult<ReadTodoDto> DeleteTodoItem(int id) {
            var todo = _todoRepository.DeleteTodo(id);
            if(todo == null) {
                return BadRequest();
            }

            if(_todoRepository.SaveChanges()) {
                return Ok(MapTodoToReadTodoDto(todo));
            }
            return BadRequest();
        }

        // PUT /api/todos/{id}
        [HttpPut("{id}")]
        public ActionResult<ReadTodoDto> UpdateTodoItem(int id, UpdateTodoDto updateTodoDto) {

            if(id != updateTodoDto.Id) {
                return BadRequest();
            }

            var todo = _todoRepository.UpdateTodo(updateTodoDto);

            if(todo == null) {
                return NotFound();
            }

            if(_todoRepository.SaveChanges()) {
                return NoContent();
                //return Ok(MapTodoToReadTodoDto(todo));
            }

            return BadRequest();
        }
    }
}