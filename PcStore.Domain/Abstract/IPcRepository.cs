using PcStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcStore.Domain.Abstract
{
    public interface IPcRepository
    {
        IEnumerable<Product> products { get; }
        void SaveProduct(Product product);
    }
}
