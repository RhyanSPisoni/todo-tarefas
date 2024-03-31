namespace Todo.Models
{
    public class TodoLista
    {
        public int id { get; set; }
        public string titulo { get; set; } = "";
        public string desc { get; set; } = "";
        public DateTime dt_criacao { get; set; }
        public DateTime? dt_conclusao { get; set; }
    }
}