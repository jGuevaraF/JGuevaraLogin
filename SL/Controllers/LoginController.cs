using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BL.UsuarioLogin _BLLogin;
        public LoginController(BL.UsuarioLogin BLLogin)
        {
            _BLLogin = BLLogin;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] ML.Login login)
        {
            ML.Result result = _BLLogin.Login(login);

            if (result.Correct)
            {
                //GENERAR EL TOKEN

                //CLAIMS

                ML.Usuario usuario = (ML.Usuario) result.Object;

                var issuer = "localhost";
                var audience = "localhost";
                var key = "S3cr3t_k3y!.123_S3cr3t_k3y!.123.Pass@word1";

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, usuario.Rol.Nombre)
                };

                var token = new JwtSecurityToken (
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: credentials
                );

                string tokenGenerado = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(tokenGenerado);
            } else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

    }
}
