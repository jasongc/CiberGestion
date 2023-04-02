using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entidades.Utilitarios
{
    public class RespuestaENT
    {
        private static RespuestaENT instance = null;

        [JsonPropertyName("exito")]
        public bool bExito { get; set; }
        [JsonPropertyName("mensaje")]
        public string? Mensaje { get; set; }

        public void Success(string stMensaje)
        {
            bExito = true;
            Mensaje = stMensaje;
        }
        public void Error(Exception ex)
        {
            bExito = false;
            Mensaje = ex.Message;
        }

        public static RespuestaENT Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RespuestaENT();
                }
                return instance;
            }
        }
    }
    public class RespuestaENT<T> : RespuestaENT, IRespuestaENT
    {
        [JsonPropertyName("resultado")]
        public T? Resultado { get; set; }
    }
    public interface IRespuestaENT
    {
        public bool bExito { get; set; }
        public string? Mensaje { get; set; }
        abstract void Success(string stMensajee);
        abstract void Error(Exception ex);

    }
}
