using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PcStore.Domain.Entities
{
    public class Product
    {
        [HiddenInput(DisplayValue=false)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter a Product Name")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage ="Please enter discription of product")]
        public string Description { get; set; }
        [Required]
        [Range(0.1,100.000,ErrorMessage ="Please a vaild pirce")]
        public decimal Price { get; set; }
        public string Brands { get; set; }
        [Required]
        public string Specilization { get; set; }
    }
}
