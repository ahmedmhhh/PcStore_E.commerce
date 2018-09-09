using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcStore.Domain.Entities
{
    public class Cart
    {
        private List<CardLine> lineCollection = new List<CardLine>();
        public void Additem(Product product,int quantity = 1)
        {
            CardLine line = lineCollection.Where(b => b.products.Id == product.Id)
                            .FirstOrDefault();
            if (line == null)
            {
                lineCollection.Add(new CardLine { products = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void Removeline(Product product)
        {
            lineCollection.RemoveAll(b => b.products.Id == product.Id);
        }
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(b => b.products.Price * b.Quantity);
        }
        public void Clear()
        {
            lineCollection.Clear();
        }
        public IEnumerable<CardLine> lines
        {
            get
            {
                return lineCollection;
            }
        }
    }
    public class CardLine
    {
        public Product products { get; set; }
        public int Quantity { get; set; }
    }
}
