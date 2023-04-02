using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Clases
{
    public class UsuarioLoginENT
    {
        public int iIdUsuario { get; set; }
        public string sEmail { get; set; } = string.Empty;
        public string sContrasenia { get; set; } = string.Empty;
    }
}
