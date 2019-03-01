using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Interfaces;
using Warehouse.Models;

namespace Warehouse.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IProductEntitiesService ProductEntitiesService;

        public HomeController(IProductEntitiesService productEntitiesService = null)
        {
            ProductEntitiesService = productEntitiesService;
        }

        public IActionResult Index()
        {
            return View(ProductEntitiesService.GetProducts().ToList());
        }

        [HttpPost]
        public IActionResult UpdateEntities([FromBody]RequestModel model)
        {
            ProductEntitiesService.UpdateEntities(model);
            return Content("OK");
        }

        public IActionResult ShoppingList()
        {
            return View(ProductEntitiesService.GetShoppingList().ToList());
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
