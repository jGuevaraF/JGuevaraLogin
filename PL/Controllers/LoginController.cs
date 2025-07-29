using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

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
        public async Task<IActionResult> Form(ML.Login login)
        {

            //Peticion HttpClient Login

            //200 => TOKEN

            //ERROR 

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://localhost:5055");

                var jsonContent = JsonConvert.SerializeObject(login);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync("/api/Login/Login", content);

                if (response.IsSuccessStatusCode)
                {
                    string token = await response.Content.ReadAsStringAsync();

                    Response.Cookies.Append("jwt_token", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true, 
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddMinutes(1)
                    });

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    ViewBag.Error = error;
                }
            }

            return View();
        }
    }
}
