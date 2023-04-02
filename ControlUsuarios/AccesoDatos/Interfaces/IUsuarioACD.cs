using Entidades.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Interfaces
{
    public interface IUsuarioACD
    {
        public UsuarioENT ObtenerUsuario(string sEmail);
        public void CambiarContrasenia(UsuarioLoginENT usuarioLogin);

    }
}
