using Moq;
using Ninject;
using PcStore.Domain.Abstract;
using PcStore.Domain.Entities;
using PcStore.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PcStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        private void AddBindings()
        {
            //Mock<IPcRepository> mock = new Mock<IPcRepository>();
            //mock.Setup(p => p.products).Returns(
            //    new List<Product> { new Product { Name="VGA",Brands="hp",Description="graphic card"},new Product { Name = "Mouse", Brands = "dell", Description = "Pc Accessoiris" },new Product { Name = "pc", Brands = "optipix", Description = "desktop pc" } }
            //    );
            kernel.Bind<IPcRepository>().To<EFPcRepository>();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}