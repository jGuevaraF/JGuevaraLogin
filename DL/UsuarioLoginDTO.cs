﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class UsuarioLoginDTO
    {
        public int IdUsuario { get; set; }
        public string? UsuarioNombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? RolNombre { get; set; }
    }
}
