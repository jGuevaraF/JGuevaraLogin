using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

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
                return Ok("TOKEN");
            } else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        //codigo para generar el token
    }
}
