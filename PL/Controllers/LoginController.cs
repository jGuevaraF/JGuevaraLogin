using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Form()
        {
            ML.Login login = new ML.Login();
            return View(login);
        }

        [HttpPost]
        //[Authorize]
        public IActionResult Form(ML.Login login)
        {
           
            //Peticion HttpClient Login

            //200 => TOKEN

            //ERROR 
            return View();
        }
    }
}
