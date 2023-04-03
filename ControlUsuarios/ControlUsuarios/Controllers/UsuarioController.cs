using Entidades.Clases;
using Entidades.Utilitarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Negocios.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlUsuarios.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        protected readonly IUsuarioNEG _usuarioNEG;
        public UsuarioController(IUsuarioNEG usuarioNEG)
        {
            _usuarioNEG = usuarioNEG;
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("CambiarContrasenia/{id}")]
        public ActionResult<RespuestaENT> CambiarContrasenia(int id, [FromBody] UsuarioLoginENT  usuarioLogin)
        {

            RespuestaENT respuestaENT = new RespuestaENT();
            try
            {
                usuarioLogin.iIdUsuario = id;
                _usuarioNEG.CambiarContrasenia(usuarioLogin);
                respuestaENT.Success("Se actualizó la contraseña correctamente.");
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
