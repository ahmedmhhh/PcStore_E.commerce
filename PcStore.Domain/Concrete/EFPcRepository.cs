using PcStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcStore.Domain.Entities;

namespace PcStore.Domain.Concrete
{
    public class EFPcRepository : IPcRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Product> products
        {
            get
            {   
                return context.Products;
            }
        }
    }
}
