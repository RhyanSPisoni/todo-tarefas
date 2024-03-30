using Microsoft.AspNetCore.Mvc;
using Todo.Services;
using Todo.Views;

namespace todo.Controllers
{
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpPost]
        [Route("/api/todos")]
        public TodoView CriaTarefasUsuario(TodoService todoService)
        {
            try
            {
                _todoService.CriaTarefa();
                return new TodoView();
            }
            catch
            {
                ExceptionService.MensagemErro();
            }

            return new TodoView();
        }

        [HttpGet]
        [Route("/api/todos")]
        public string RetornaTodos()
        {
            return "";
        }

        [HttpGet]
        [Route("/api/todos/{id}")]
        public string RetornaTodo()
        {
            return "";
        }

        [HttpPut]
        [Route("/api/todos/{id}")]
        public string AlteraTodo()
        {
            return "";
        }

        [HttpDelete]
        [Route("/api/todos/{id}")]
        public string RemoveTodo()
        {
            return "";
        }

    }
}