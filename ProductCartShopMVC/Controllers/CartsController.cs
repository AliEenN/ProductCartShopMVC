using ProductCartShop.Models;
using ProductCartShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductCartShopMVC.Controllers
{
    public class CartsController : Controller
    {
        // GET: Carts
        public ActionResult Index()
        {
            var cartDetailsVM = new CartDetailsViewModel()
            {
                OrderTotal = 0,
                Count = Session["count"] == null ? 0 : (int)Session["count"]
            };

            var obj = (Product)Session["product"];

            if (obj == null)
                return View(cartDetailsVM);

            cartDetailsVM.Product = obj;

            if (cartDetailsVM.Product.Description.Length > 75)
            {
                cartDetailsVM.Product.Description = cartDetailsVM.Product.Description.Substring(0, 74);
            }

            cartDetailsVM.OrderTotal = cartDetailsVM.Product.Price * cartDetailsVM.Count;
            cartDetailsVM.TotalAndShippingRate = cartDetailsVM.Product.ShippingRate * cartDetailsVM.Count + cartDetailsVM.OrderTotal;

            return View(cartDetailsVM);
        }

        // POST
        public ActionResult Plus()
        {
            var obj = (Product)Session["product"];

            var cartDetailsVM = new CartDetailsViewModel()
            {
                Count = (int)Session["count"] + 1,
                Product = obj
            };

            cartDetailsVM.OrderTotal = obj.Price * cartDetailsVM.Count;
            cartDetailsVM.TotalAndShippingRate = cartDetailsVM.Product.ShippingRate * cartDetailsVM.Count + cartDetailsVM.OrderTotal;

            Session["count"] = cartDetailsVM.Count;

            return View("Index", cartDetailsVM);
        }

        // POST
        public ActionResult Minus()
        {
            var obj = (Product)Session["product"];

            var cartDetailsVM = new CartDetailsViewModel()
            {
                Count = (int)Session["count"] - 1 == 0 ? (int)Session["count"] : (int)Session["count"] - 1,
                Product = obj
            };

            cartDetailsVM.OrderTotal = obj.Price * cartDetailsVM.Count;
            cartDetailsVM.TotalAndShippingRate = cartDetailsVM.Product.ShippingRate * cartDetailsVM.Count + cartDetailsVM.OrderTotal;

            Session["count"] = cartDetailsVM.Count;

            return View("Index", cartDetailsVM);
        }

        // GET
        [HttpGet]
        public ActionResult Continue()
        {
            var obj = (Product)Session["product"];

            var cartDetailsVM = new CartDetailsViewModel()
            {
                Count = (int)Session["count"],
                Product = obj
            };

            cartDetailsVM.OrderTotal = cartDetailsVM.Product.Price * cartDetailsVM.Count;
            cartDetailsVM.TotalAndShippingRate = cartDetailsVM.Product.ShippingRate * cartDetailsVM.Count + cartDetailsVM.OrderTotal;
            cartDetailsVM.TotalAfterTax = cartDetailsVM.TotalAndShippingRate * (decimal)0.14 + cartDetailsVM.TotalAndShippingRate;

            return View(cartDetailsVM);
        }
    }
}