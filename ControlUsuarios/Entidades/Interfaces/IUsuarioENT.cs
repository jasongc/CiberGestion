using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Interfaces
{
    public interface IUsuarioENT
	{
		public int iIdUsuario { get; set; }
		public int iIdPerfil { get; set; }
		public string sPerfil { get; set; }
		public string sNombres { get; set; }
		public string sApellidoMaterno { get; set; }
		public string sApellidoPaterno { get; set; }
		public string sEmail { get; set; }
		public string sContrasenia { get; set; }
	}
}
