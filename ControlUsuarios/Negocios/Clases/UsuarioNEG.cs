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
    public class UsuarioNEG : IUsuarioNEG
    {
        protected readonly IUsuarioACD _usuarioACD;
        public UsuarioNEG(IUsuarioACD usuarioACD)
        {
            _usuarioACD = usuarioACD;
        }
        public void CambiarContrasenia(UsuarioLoginENT usuarioLogin)
        {
            try
            {
                _usuarioACD.CambiarContrasenia(usuarioLogin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
