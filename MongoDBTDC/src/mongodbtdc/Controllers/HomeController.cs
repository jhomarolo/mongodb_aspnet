using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDBTDC.Model;

namespace MongoDBTDC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TDC()
        {
            var person = new Person { Name = "Rafael" };
            var repository = new MongoDBService.MongoRepository<Person>("tdc");
            repository.Add(person);

            var interpolation = $"A pessoa {person.Name} do ID {person.PersonId} foi criada em {person.CreateDate}";

            ViewData["Message"] = interpolation;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
