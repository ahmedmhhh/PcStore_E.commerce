using PcStore.Domain.Abstract;
using PcStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PcStore.WebUI.Controllers
{
    public class PcController : Controller
    {
        private IPcRepository repository;
        public int PageSize = 3;
        public PcController(IPcRepository PcParam)
        {
            repository = PcParam;
        }
        public ViewResult List(int page)
        {
            ProductListViewModel model = new ProductListViewModel()
            {
                Products = repository.products
                .OrderBy(b => b.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new Models.PagingInfo{
                    CurrentPage = page,
                    ItemsPerPages = PageSize,
                    TotalItems = repository.products.Count()
                }
            };
            return View(model);
        }
        public ViewResult ListAll()
        {
            return View(repository.products);
        }
    }
}