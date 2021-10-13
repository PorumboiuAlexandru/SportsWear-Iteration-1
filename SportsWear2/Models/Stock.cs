using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWear2.Models
{
    public class Stock
    {
        [Key]
        [DisplayName("Product ID")]
        public int StockID { get; set; }
        public int ProductID { set; get; }
        public string Name { get; set; }
        [DisplayName("Quantity")]
        public int Qty { get; set; }
        [DisplayName("Price (€)")]
        public double Price { get; set; }
        public virtual Product Product { set; get; }
    }
}
