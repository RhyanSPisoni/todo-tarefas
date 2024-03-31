using Todo.DTOs;
using Todo.Models;
using Todo.Views;

namespace Todo.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioView> BuscaDadosUsuario(string email);
        Task<List<UsuarioView>> BuscaDadosUsuarios();
        Task<Usuario> BuscaDadosUsuarioEmail(string email);
        Task<string> CriaUsuario(DtoUsuario login);
        string CriptografarSenha(string senha);
        Task<dynamic> VerificaLogin(DtoLogin login);
    }
}