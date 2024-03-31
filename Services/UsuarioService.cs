using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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

        public async Task<UsuarioView> BuscaDadosUsuario(string email)
        {
            var usuario = BuscaDadosUsuarioEmail(email).Result.id;

            var res = await _db.Usuario
                               .Where(x => x.id == usuario)
                               .Select(x => new UsuarioView
                               {
                                   id = usuario,
                                   email = x.email,
                                   name = x.name
                               })
                                .FirstOrDefaultAsync();

            return res;
        }

        public async Task<List<UsuarioView>> BuscaDadosUsuarios()
        {
            var res = await _db.Usuario
                               .Select(x => new UsuarioView
                               {
                                   id = x.id,
                                   email = x.email,
                                   name = x.name
                               })
                                .ToListAsync();

            return res;
        }

        public async Task<Usuario> BuscaDadosUsuarioEmail(string email)
        {
            return await _db.Usuario.FirstOrDefaultAsync(x => x.email == email);
        }

        public async Task<string> CriaUsuario(DtoUsuario login)
        {
            var senha = CriptografarSenha(login.senha);

            var usuario = BuscaDadosUsuarioEmail(login.email);
            if (usuario.Result != null)
                ExceptionService.MensagemErro();

            await _db.Usuario.AddAsync(
                new Usuario
                {
                    email = login.email,
                    name = login.name,
                    senha = senha
                }
            );
            await _db.SaveChangesAsync();

            return "Usuário criado!";
        }

        public string CriptografarSenha(string senha)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            // Criar o hash da senha utilizando SHA256 com salt
            byte[] hash;
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(senha);
                byte[] passwordWithSalt = new byte[passwordBytes.Length + salt.Length];
                Array.Copy(passwordBytes, passwordWithSalt, passwordBytes.Length);
                Array.Copy(salt, 0, passwordWithSalt, passwordBytes.Length, salt.Length);

                hash = sha256.ComputeHash(passwordWithSalt);
            }

            // Concatenar o salt com o hash
            byte[] hashWithSalt = new byte[salt.Length + hash.Length];
            Array.Copy(salt, 0, hashWithSalt, 0, salt.Length);
            Array.Copy(hash, 0, hashWithSalt, salt.Length, hash.Length);

            // Converter o hash com salt para uma string base64
            return Convert.ToBase64String(hashWithSalt);
        }

        public async Task<dynamic> VerificaLogin(DtoLogin login)
        {
            var token = _tokenService.Generate(login);

            return token;
        }
    }
}