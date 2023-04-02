using Entidades.Clases;
using Entidades.Utilitarios;
using Microsoft.AspNetCore.Mvc;
using Negocios.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        protected readonly ILoginNEG _loginNEG;
        public LoginController(ILoginNEG loginNEG)
        {
            _loginNEG = loginNEG;
        }

        // GET: api/<LoginController>
        [HttpPut("RegistrarCierreLogin/{id}")]
        public ActionResult<RespuestaENT> RegistrarCierreLogin(int id)
        {
            RespuestaENT respuestaENT = new RespuestaENT();
            try
            {
                _loginNEG.RegistrarCierreLogin(id);
                respuestaENT.Success("Se cerró la sesión correctamente.");
                return Ok(respuestaENT);
            }
            catch (Exception ex)
            {
                respuestaENT.Error(ex);
                return BadRequest(respuestaENT);
            }
        }

        // GET: api/<LoginController>
        [HttpPost("InicioSesion")]
        public ActionResult<RespuestaENT<UsuarioENT>> InicioSesion([FromBody] UsuarioLoginENT usuarioLogin)
        {
            RespuestaENT<UsuarioENT> respuestaENT = new RespuestaENT<UsuarioENT>();
            try
            {
                respuestaENT.Resultado = _loginNEG.InicioSesion(usuarioLogin);
                respuestaENT.Success("Se inició la sesión correctamente.");
                return Ok(respuestaENT);
            }
            catch (Exception ex)
            {
                respuestaENT.Error(ex);
                return BadRequest(respuestaENT);
            }
        }
    }
}
