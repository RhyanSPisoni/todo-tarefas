using Todo.DTOs;
using Todo.Views;

namespace Todo.Interfaces
{
    public interface ITodoService
    {
        Task<string> CriaTarefa(DtoTodo dtoLista, string email);
        Task<List<TodoView>> RetornaTodos(string email);
        Task<TodoView> RetornaTodo(int idItem, string email);
        Task<string> AlteraTodo(int id, DtoTodo body, string email);
        Task<string> DeletaTodo(int id, string email);

    }
}