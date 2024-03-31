#nullable disable

namespace Todo.DTOs
{

    public class DtoTodo
    {
        public string titulo { get; set; } = "";
        public string desc { get; set; } = "";
        public DateTime dt_criacao { get; set; }
        public DateTime? dt_conclusao { get; set; }
        public int idUsuario { get; set; }
    }
}