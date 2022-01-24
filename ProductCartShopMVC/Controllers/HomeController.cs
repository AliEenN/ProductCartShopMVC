using ProductCartShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProductCartShopMVC.Controllers
{
    public class HomeController : Controller
    {
        public static List<Product> products;

        public HomeController()
        {
            products = new List<Product>()
            {
                new Product { Id = 1, Name = "Car", Description = "Car Description", Price = 1100, ShippingRate = 75 }
            };
        }

        public ActionResult Index()
        {
            return View(products);
        }

        public ActionResult AddToShoppingCarts(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var productExist = products.FirstOrDefault(e => e.Id == id);

            if (productExist == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            Session["product"] = productExist;

            Session["count"] = 1;

            return RedirectToAction(nameof(Index));
        }
    }
}