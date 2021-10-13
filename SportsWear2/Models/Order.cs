using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWear2.Models
{
    public class Order
    {
        
        [Key]
        [DisplayName("Order ID")]
        public int OrderID { get; set; }
        public string Address { get; set; }
        [DisplayName("Order Details")]
        public string Details { get; set; }
    }
}
