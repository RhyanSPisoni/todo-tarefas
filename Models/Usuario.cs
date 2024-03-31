namespace Todo.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string name { get; set; } = "";
        public string email { get; set; } = "";
        public string senha { get; set; } = "";
        public ICollection<TodoLista>? todoListas { get; set; }
    }
}