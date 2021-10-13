using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SportsWear2.Models
{
    public enum ProductType { TShirts, Footware, Hoodies, jackets, Tracksuits }
    public enum Size { Small, Medium, Large }
    public enum Gender { Male, Femal }
    public class Product
    {
        [Key]
        public int ProductID { set; get; }
        public String Name { set; get; }
        [DisplayName("Upload File")]
        public string Image { set; get; }
        public Gender Gender { set; get; }
        public ProductType ProductType { set; get; }
        public String Description { set; get; }
        public Size Size { set; get; }
        public double Price { set; get; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { set; get; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
    public class CartItems : Product
    {
        public int Qty { get; set; }
    }
}
