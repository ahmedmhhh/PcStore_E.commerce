using PcStore.Domain.Abstract;
using PcStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PcStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IPcRepository repository;
        public AdminController(IPcRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()
        {
            return View(repository.products);
        }
        public ViewResult Edit(int id)
        {
            Product product = repository.products.FirstOrDefault(b => b.Id == id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if(ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = product.Name + " has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }

        }
    }
}