using PcStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PcStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IPcRepository repository;
        public NavController(IPcRepository repo)
        {
            repository = repo;
        }
        public PartialViewResult Menu(string specilization=null)
        {
            ViewBag.SelectedSpec = specilization;
            IEnumerable<string> spec = repository.products
                .Select(b => b.Specilization)
                .Distinct();

            //string viewName = MobileLayout ? "MenuMobile" : "Menu";
            return PartialView("FlexMenu",spec);
        }
    }
}