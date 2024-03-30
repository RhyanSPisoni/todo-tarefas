using Todo.Db;
using Todo.Interfaces;

namespace Todo.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoContext _dbTodo;
        public TodoService(TodoContext dbTodo)
        {
            _dbTodo = dbTodo;
        }

        public bool CriaTarefa()
        {
            return true;
        }
    }
}
