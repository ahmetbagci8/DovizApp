using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DovizApp.Controllers
{
    public class DovizAppController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public string Welcome()
        {
            //ViewData["Message"] = "Hello " + name;  welcome name parametresi alır
            return "This is the Welcome action method...";

        }
    }
}