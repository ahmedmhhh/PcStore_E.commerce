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
        [HttpPost]
        public ViewResult Index(string searchValue)
        {
            IEnumerable<Product> products;
            if (searchValue != null)
            {
                products = from b in repository.products
                           where b.Description.IndexOf(searchValue)>0 || b.Name.IndexOf(searchValue)>0
                           || b.Specilization.IndexOf(searchValue)>0
                           select b;
            }
            else
            {
                products = from b in repository.products select b;
            }
            return View("Index", products);
        }
        public ViewResult Index()
        {
            return View("Index", repository.products);
        }
        public ViewResult Edit(int id)
        {
            Product product = repository.products.FirstOrDefault(b => b.Id == id);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product,HttpPostedFileBase image=null)
        {
            if(ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                repository.SaveProduct(product);
                TempData["message"] = product.Name + " Has been Saved Successfully !!";

                //TempData["message"] = string.Format("Has been Saved Successfully !! {0} ", book.Title);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }

        }
        public ViewResult Create()
        {
            return View("Edit", new Product());
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            Product deleteProduct = repository.DeleteProduct(Id);
            if(deleteProduct != null)
            {
                TempData["message"] = deleteProduct.Name + " Was deleted";
            }
            return RedirectToAction("Index");
        }
    }
}