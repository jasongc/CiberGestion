using Entidades.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Interfaces
{
    public interface ILoginNEG
    {
        public void RegistrarCierreLogin(int piIdUsuario);
        public UsuarioENT InicioSesion(UsuarioLoginENT usuarioLogin);
    }
}
