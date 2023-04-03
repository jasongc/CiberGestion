using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Interfaces
{
    public interface IJsonWebTokenNEG
    {
        public string CreateToken(string psUsuario, string psPerfil, string psNombres, int piIdUsuario);
    }
}
