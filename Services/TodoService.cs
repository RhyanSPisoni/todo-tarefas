using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        public TodoService(TodoContext dbTodo)
        {
            _dbTodo = dbTodo;
        }


        internal async Task<bool> CriaTarefa(List<DtoTodo> dtoLista)
        {
            var todos = dtoLista.Select(dto => new TodoLista
            {
                titulo = dto.titulo,
                dt_criacao = dto.dt_criacao,
                desc = dto.desc,
                dt_conclusao = null
            }).ToList();


            await _dbTodo.todo.AddRangeAsync(todos);
            await _dbTodo.SaveChangesAsync();

            dtoLista.Clear();
            todos.Clear();

            return true;
        }

        internal async Task<List<TodoView>> RetornaTodos()
        {
            var todoModel = await _dbTodo.todo.ToListAsync();

            return todoModel.Select(dto => new TodoView
            {
                desc = dto.desc,
                dt_conclusao = dto.dt_conclusao,
                dt_criacao = dto.dt_criacao,
                id = dto.id,
                titulo = dto.titulo
            }).ToList();
        }

        internal async Task<TodoView> RetornaTodo(int idItem)
        {
            var res = await _dbTodo.todo
                                   .Select(d => new TodoView
                                   {
                                       desc = d.desc,
                                       dt_conclusao = d.dt_conclusao,
                                       dt_criacao = d.dt_criacao,
                                       id = d.id,
                                       titulo = d.titulo
                                   })
                                   .FirstOrDefaultAsync(x => x.id == idItem);

            if (res == null)
                res = new TodoView();

            return res;
        }

        internal async Task AlteraTodo(int id, DtoTodo body)
        {
            _dbTodo.todo.Update(new TodoLista
            {
                id = id,
                desc = body.desc,
                dt_conclusao = body.dt_conclusao,
                dt_criacao = body.dt_criacao,
                titulo = body.titulo
            });

            await _dbTodo.SaveChangesAsync();
        }

        internal async Task DeletaTodo(int id)
        {
            using (var todo = _dbTodo.todo.FirstOrDefaultAsync(x => x.id == id))
            {
                if (todo.Result == null)
                    throw new Exception("Usuário não existente");

                _dbTodo.todo.Remove(todo.Result);
                await _dbTodo.SaveChangesAsync();
            }
        }
    }
}
