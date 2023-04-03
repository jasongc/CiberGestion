using AccesoDatos.Interfaces;
using Entidades.Clases;
using Negocios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Clases
{
    public class LoginNEG : ILoginNEG
    {
        protected readonly ILoginACD _loginACD;
        protected readonly IUsuarioACD _usuarioACD;
        protected readonly IJsonWebTokenNEG _jsonWebTokenNEG;
        public LoginNEG(ILoginACD loginACD, IUsuarioACD usuarioACD, IJsonWebTokenNEG jsonWebTokenNEG)
        {
            _loginACD = loginACD;
            _usuarioACD = usuarioACD;
            _jsonWebTokenNEG = jsonWebTokenNEG;
        }
        public void RegistrarCierreLogin(int piIdUsuario)
        {
            try
            {
                _loginACD.RegistrarCierreLogin(piIdUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UsuarioENT InicioSesion(UsuarioLoginENT usuarioLogin)
        {
            try
            {
                UsuarioENT usuarioENT = _usuarioACD.ObtenerUsuario(usuarioLogin.sEmail);
                if (usuarioENT.sContrasenia == usuarioLogin.sContrasenia)
                {
                    string sNombres = string.Format("{0} {1} {2}", usuarioENT.sNombres, usuarioENT.sApellidoPaterno, usuarioENT.sApellidoMaterno);
                    usuarioENT.sJsonWebToken = _jsonWebTokenNEG.CreateToken(usuarioENT.sEmail, usuarioENT.sPerfil, sNombres, usuarioENT.iIdUsuario);
                    usuarioENT.dtFechaUltimoAcceso = _loginACD.RegistrarInicioLogin(usuarioENT.iIdUsuario, usuarioENT.sJsonWebToken).dtFechaUltimoAcceso;

                }
                else
                    _loginACD.RegistrarIntentoFallidoLogin(usuarioENT.iIdUsuario);

                return usuarioENT;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
