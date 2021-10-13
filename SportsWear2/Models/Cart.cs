using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsWear2.Models
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        public List<CartItems> items;

        public Cart()
        {
            items = new List<CartItems>();
        }

        public void AddItem(Stock choice)
        {
            CartItems found = items.FirstOrDefault(p => p.ProductID == choice.StockID);
            if (found != null)
            {
                found.Qty++;
            }
            else
            {
                items.Add(new CartItems() { ProductID = choice.StockID, Name = choice.Name, Price = choice.Price, Qty = 1 });
            }


        }

        public List<CartItems> ReturnCart()
        {
            return items;
        }

        public void EmptyCart()
        {
            items.Clear();
        }

        public double CalcTotal()
        {
            return items.Sum(p => p.Price * p.Qty);
        }

        public void RemoveItem(Stock choice)
        {

            CartItems found = items.FirstOrDefault(p => p.ProductID == choice.StockID);
            if (found != null)
            {
                found.Qty--;
                if (found.Qty <= 0)
                {
                    items.Remove(found);
                }
            }

        }
    }
}
