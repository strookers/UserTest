using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UserTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

                var httpCookie = Request.Cookies["User"];
                if (httpCookie != null)
                {
                    string name = httpCookie["name"];
                    string email = httpCookie["email"];

                    ViewBag.Message = $"du er logget ind som: {name} med email: {email}";
                }
                else
                {
                    ViewBag.Message = "Du er ikke logget ind";
                }
            

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LogIn()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult LogInPost()
        {
            string email = Request.Form["email"];
            string password = Request.Form["password"];


            if (email == "michael_k@mail.dk" && password == "1234")
            {
                string name = "Michael Kirkegaard";
                string phone = "31123161";

                HttpCookie userCookie = new HttpCookie("User");
                userCookie.Values.Add("name", name);
                userCookie.Values.Add("phone", phone);
                userCookie.Values.Add("email", email);

                Response.Cookies.Add(userCookie);
            }
            return RedirectToAction("Index");
        }

        public ActionResult LogOut()
        {
            var httpCookie = Request.Cookies["User"];
            if (httpCookie != null)
            {
                httpCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(httpCookie);
            }
            return RedirectToAction("Index");
        }

    }
}