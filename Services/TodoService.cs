using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using Todo.Db;
using Todo.DTOs;
using Todo.Interfaces;
using Todo.Models;
using Todo.Views;

namespace Todo.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoContext _dbTodo;
        private readonly UsuarioService _usuarioService;
        public TodoService(TodoContext dbTodo,
                            UsuarioService usuarioService)
        {
            _dbTodo = dbTodo;
            _usuarioService = usuarioService;
        }

        public async Task<string> CriaTarefa(DtoTodo dtoLista, string email)
        {
            var id = _usuarioService.BuscaDadosUsuarioEmail(email).Result.id;

            var todos = new TodoLista
            {
                titulo = dtoLista.titulo,
                dt_criacao = dtoLista.dt_criacao,
                desc = dtoLista.desc,
                dt_conclusao = null,
                idUsuario = id
            };

            await _dbTodo.todo.AddRangeAsync(todos);
            await _dbTodo.SaveChangesAsync();

            return "Criado com sucesso!";
        }

        public async Task<List<TodoView>> RetornaTodos(string email)
        {
            var user = _usuarioService.BuscaDadosUsuarioEmail(email).Result.id;

            var todoModel = await _dbTodo.todo
                                         .Where(x => x.idUsuario == user)
                                         .ToListAsync();

            return todoModel.Select(dto => new TodoView
            {
                desc = dto.desc,
                dt_conclusao = dto.dt_conclusao,
                dt_criacao = dto.dt_criacao,
                id = dto.id,
                titulo = dto.titulo
            }).ToList();
        }

        public async Task<TodoView> RetornaTodo(int idItem, string email)
        {
            var usuario = _usuarioService.BuscaDadosUsuarioEmail(email).Result.id;

            var res = await _dbTodo.todo
                                   .Where(x => x.idUsuario == usuario && x.id == idItem)
                                   .Select(d => new TodoView
                                   {
                                       id = usuario,
                                       desc = d.desc,
                                       dt_conclusao = d.dt_conclusao,
                                       dt_criacao = d.dt_criacao,
                                       titulo = d.titulo
                                   })
                                   .FirstOrDefaultAsync();

            return res;
        }

        public async Task<string> AlteraTodo(int id, DtoTodo body, string email)
        {
            var usuario = _usuarioService.BuscaDadosUsuarioEmail(email).Result.id;

            _dbTodo.todo.Update(new TodoLista
            {
                id = id,
                desc = body.desc,
                dt_conclusao = body.dt_conclusao,
                dt_criacao = body.dt_criacao,
                titulo = body.titulo,
                idUsuario = usuario
            });

            await _dbTodo.SaveChangesAsync();

            return "Todo atualizada";
        }

        public async Task<string> DeletaTodo(int id, string email)
        {
            var usuario = _usuarioService.BuscaDadosUsuarioEmail(email).Result.id;

            using (var todo = _dbTodo.todo.Where(x => x.idUsuario == usuario && x.id == id)
                                          .FirstOrDefaultAsync())
            {
                if (todo.Result == null)
                    throw new Exception("Usuário não existente");

                _dbTodo.todo.Remove(todo.Result);
                await _dbTodo.SaveChangesAsync();

                return "Usuário deletado";
            }
        }
    }
}
