using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Db;
using Todo.DTOs;
using Todo.Interfaces;
using Todo.Models;
using Todo.Views;

namespace Todo.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly TodoContext _db;
        private readonly TokenService _tokenService;
        public UsuarioService(TodoContext db,
                              TokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        internal async Task<UsuarioView> BuscaDadosUsuario(int id)
        {
            var res = await _db.Usuario
                               .Select(x => new UsuarioView
                               {
                                   email = x.email,
                                   name = x.name
                               })
                                .FirstOrDefaultAsync();

            if (res == null)
                res = new UsuarioView()
                {
                    email = "",
                    name = ""
                };

            return res;
        }

        internal async Task<bool> CriaUsuario(DtoUsuario login)
        {
            await _db.Usuario.AddAsync(
                new Usuario
                {
                    email = login.email,
                    name = login.name,
                    senha = login.senha
                }
            );
            await _db.SaveChangesAsync();

            return true;
        }

        internal async Task<dynamic> VerificaLogin(DtoLogin login)
        {
            var token = _tokenService.Generate(login);

            return new
            {
                user = login,
                token = token
            };

        }
    }
}