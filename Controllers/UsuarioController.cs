using Microsoft.AspNetCore.Mvc;
using Todo.Db;
using Todo.Services;
using Todo.Views;
using Todo.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Todo.Controllers
{
    [ApiController]
    [Route("api/usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Route("dados/{id}")]
        [Authorize]
        public async Task<UsuarioView> BuscaDadosUsuario(int id)
        {
            try
            {
                return await _usuarioService.BuscaDadosUsuario(id);
            }
            catch
            {
                ExceptionService.MensagemErro();
            }

            return new UsuarioView();
        }

        [HttpPost]
        [Route("novo/usuario")]
        [AllowAnonymous]
        public async Task<bool> CriaUsuario([FromBody] DtoUsuario login)
        {
            try
            {
                return await _usuarioService.CriaUsuario(login);
            }
            catch
            {
                ExceptionService.MensagemErro();
                return false;
            }

        }

        [HttpPost]
        [Route("verificalogin")]
        public async Task<ActionResult<dynamic>> VerificaLogin([FromBody] DtoLogin login)
        {
            try
            {
                return await _usuarioService.VerificaLogin(login);
            }
            catch
            {
                ExceptionService.MensagemErro();
                return false;
            }

        }
    }

}