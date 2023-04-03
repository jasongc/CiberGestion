using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entidades.Clases
{
    public class UsuarioLoginENT
    {
        [JsonIgnore]
        public int iIdUsuario { get; set; }
        [JsonPropertyName("usuario")]
        public string sEmail { get; set; } = string.Empty;
        [JsonPropertyName("contrasenia")]
        public string sContrasenia { get; set; } = string.Empty;
        [JsonPropertyName("nueva_contrasenia")]
        public string sNuevaContrasenia { get; set; } = string.Empty;
    }
}
