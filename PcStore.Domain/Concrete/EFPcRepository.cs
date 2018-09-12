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

        public Product DeleteProduct(int Id)
        {
            Product dbEntity = context.Products.Find(Id);
            if (dbEntity != null)
            {
                context.Products.Remove(dbEntity);
                context.SaveChanges();
            }
            return dbEntity;
        }

        public void SaveProduct(Product product)
        {
           Product dbEntity = context.Products.Find(product.Id);
            if (dbEntity == null)
            {
                context.Products.Add(product);
            }
            else
            {
                dbEntity.Name = product.Name;
                dbEntity.Description = product.Description;
                dbEntity.Brands = product.Brands;
                dbEntity.Price = product.Price;
                dbEntity.Specilization = product.Specilization;
                dbEntity.ImageData = product.ImageData;
                dbEntity.ImageMimeType = product.ImageMimeType;
            }
            context.SaveChanges();
            
        }
    }
}
