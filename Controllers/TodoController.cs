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
            return await _todoService.RetornaTodos();
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<TodoView> RetornaTodo(int id)
        {
            return await _todoService.RetornaTodo(id);
        }

        [HttpPost]
        [Authorize]
        public async Task<bool> CriaTarefasUsuario(
                   [FromBody] List<DtoTodo> todoLista)
        {
            try
            {
                await _todoService.CriaTarefa(todoLista);

                return true;
            }
            catch
            {
                ExceptionService.MensagemErro();
            }

            return false;
        }
        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<bool> AlteraTodo(int id, [FromBody] DtoTodo body)
        {
            try
            {
                await _todoService.AlteraTodo(id, body);
                return true;
            }
            catch
            {
                ExceptionService.MensagemErro();
            }

            return true;
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<bool> RemoveTodo(int id)
        {
            try
            {
                await _todoService.DeletaTodo(id);
                return true;
            }
            catch
            {
                ExceptionService.MensagemErro();
            }

            return true;
        }

    }
}