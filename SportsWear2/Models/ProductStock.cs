using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWear2.Models
{
    public class ProductStock
    {
        [Key]
        public int ProductID { set; get; }
        public String Name { set; get; }
        public string Image { set; get; }
        public Gender Gender { set; get; }
        public ProductType ProductType { set; get; }
        public String Description { set; get; }
        public Size Size { set; get; }
        public double Price { set; get; }

        [DisplayName("Quantity")]
        public int Qty { get; set; }

        public int StockID { get; set; }






    }
}
