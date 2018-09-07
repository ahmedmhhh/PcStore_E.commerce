using PcStore.Domain.Abstract;
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
            return View(repository.products
                .OrderBy(b=>b.Id)
                .Skip((page-1)*PageSize)
                .Take(PageSize)
                );
        }
        public ViewResult ListAll()
        {
            return View(repository.products);
        }
    }
}