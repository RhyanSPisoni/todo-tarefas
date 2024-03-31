using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.DTOs;
using Todo.Models;
using Todo.Services;
using Todo.Views;

namespace todo.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;
        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        [Authorize]
        public async Task<List<TodoView>> RetornaTodos()
        {
            return await _todoService.RetornaTodos(User.Identity.Name);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<TodoView> RetornaTodo(int id)
        {
            return await _todoService.RetornaTodo(id, User.Identity.Name);
        }

        [HttpPost]
        [Authorize]
        public async Task<string> CriaTarefasUsuario([FromBody] DtoTodo todoLista)
        {
            try
            {
                return await _todoService.CriaTarefa(todoLista, User.Identity.Name);
            }
            catch
            {
                ExceptionService.MensagemErro();
            }

            return "";
        }
        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<string> AlteraTodo(int id, [FromBody] DtoTodo body)
        {
            try
            {
                return await _todoService.AlteraTodo(id, body, User.Identity.Name);
            }
            catch
            {
                ExceptionService.MensagemErro();
            }

            return "";
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<string> RemoveTodo(int id)
        {
            try
            {
                return await _todoService.DeletaTodo(id, User.Identity.Name);
            }
            catch
            {
                ExceptionService.MensagemErro();
            }

            return "";
        }

    }
}