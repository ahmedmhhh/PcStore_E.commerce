using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage ="Please enter a name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter a first address line")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        [Required(ErrorMessage = "Please enter the city")]
        public string City { get; set; }
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter a the country")]
        public string Country { get; set; }
        public bool GiftWrap { get; set; }
    }
}
