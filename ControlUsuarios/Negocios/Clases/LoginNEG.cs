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
        public LoginNEG(ILoginACD loginACD, IUsuarioACD usuarioACD)
        {
            _loginACD = loginACD;
            _usuarioACD = usuarioACD;
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
                    usuarioENT.dtFechaUltimoAcceso = _loginACD.RegistrarInicioLogin(usuarioLogin).dtFechaUltimoAcceso;
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
