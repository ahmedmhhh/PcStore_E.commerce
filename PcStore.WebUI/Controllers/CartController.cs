using PcStore.Domain.Abstract;
using PcStore.Domain.Entities;
using PcStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PcStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IPcRepository repository;
        private IOrderProcessor orderProcessor;
        public CartController(IPcRepository repo,IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }
        public RedirectToRouteResult AddToCart(Cart cart,int id,string returnUrl)
        {
            Product product = repository.products
                                .FirstOrDefault(b => b.Id == id);
            if (product != null)
            {
                cart.Additem(product);
            }
            return RedirectToAction("index", new { returnUrl });
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart,int id,string returnUrl)
        {
            Product product = repository.products
                                .FirstOrDefault(b => b.Id == id);
            if (product != null)
            {
                cart.Removeline(product);
            }
            return RedirectToAction("index", new { returnUrl });
        }
        private Cart GetCart()
        {
            Cart cart =(Cart) Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        // GET: Cart
        public ViewResult Index(Cart cart,string returnUrl)
        {
            return View(new CartIndexViewModel {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart,ShippingDetails shippingDetails)
        {
            if (cart.lines.Count() == 0)
                ModelState.AddModelError("", "Sorry your Cart is empty");
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessorOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else {
                return View(shippingDetails);
            }
        }
    }
}