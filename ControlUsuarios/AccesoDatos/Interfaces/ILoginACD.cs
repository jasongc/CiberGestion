﻿using Entidades.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Interfaces
{
    public interface ILoginACD
    {
        public UsuarioENT RegistrarInicioLogin(int iIdUsuario, string sJWT);
        public void RegistrarCierreLogin(int piIdUsuario);
        public void RegistrarIntentoFallidoLogin(int piIdUsuario);
    }
}
