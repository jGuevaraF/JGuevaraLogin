using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UsuarioLogin
    {
        private readonly JguevaraProgramacionNcapasFebreroContext _context;

        public UsuarioLogin(JguevaraProgramacionNcapasFebreroContext context)
        {
            _context = context;
        }


        public ML.Result Login(ML.Login login)
        {
            ML.Result result = new ML.Result();

            try
            {
                var query = _context.UsuarioLoginDTO.FromSqlRaw($"UsuarioLogin '{login.Email}' , '{login.Password}'").AsEnumerable().SingleOrDefault();

                if (query != null)
                {
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Nombre = query.UsuarioNombre;
                    usuario.Rol = new ML.Rol();
                    usuario.Rol.Nombre = query.RolNombre;

                    result.Object = usuario;

                    result.Correct  = true;
                } else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Correo o contraseña son invalidos";
                }

            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
    }
}
