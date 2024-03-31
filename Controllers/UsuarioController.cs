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

        // [HttpGet]
        // [Authorize]
        // public async Task<List<UsuarioView>> BuscaDadosUsuarios()
        // {
        //     try
        //     {
        //         return await _usuarioService.BuscaDadosUsuarios();
        //     }
        //     catch
        //     {
        //         ExceptionService.MensagemErro();
        //     }

        //     return new List<UsuarioView>();
        // }

        [HttpGet]
        [Route("dados")]
        [Authorize]
        public async Task<UsuarioView> BuscaDadosUsuario()
        {
            try
            {
                return await _usuarioService.BuscaDadosUsuario(User.Identity.Name);
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
        public async Task<string> CriaUsuario([FromBody] DtoUsuario login)
        {
            try
            {
                return await _usuarioService.CriaUsuario(login);
            }
            catch
            {
                ExceptionService.MensagemErro();
            }

            return "";

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
            }

            return "";

        }
    }

}