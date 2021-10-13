using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsWear2.Data;
using SportsWear2.Models;

namespace SportsWear2.Controllers
{
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            this._context = context;
            _context.Database.EnsureCreated();
        }

        public void ReduceStock(Stock enrty)
        {
            var xy = _context.Stocks.Find(enrty.ProductID);
            if(xy != null)
            {
                xy.Qty --;

                _context.SaveChanges();
            }
        }

        public void IncreaseStock(Stock enrty)
        {
            var xy = _context.Stocks.Find(enrty.ProductID);
            if (xy != null)
            {
                xy.Qty++;

                _context.SaveChanges();
            }
        }


        // GET: Shop
        public async Task<IActionResult> Index()
        {
            var Shop = await _context.Stocks.ToListAsync();
            ViewBag.TotalPrice = String.Format(CartController.c1.CalcTotal().ToString("C2"));
            return View(Shop);
        }

        // GET: Shop/Details/5
        public async Task<IActionResult> Add(int code)
        {
            var Shop = await _context.Stocks.ToListAsync();

            Stock itm = Shop.FirstOrDefault(i => i.ProductID== code);
            if (itm != null)
            {
                CartController.c1.AddItem(itm);
                //ReduceStock(itm);
            }

            
            return RedirectToAction("Index");
        }

        // GET: Shop/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shop/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdID,Name,Qty,Price")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stock);
        }

        // GET: Shop/Edit/5
        public async Task<IActionResult> Edit(int code)
        {
            var Shop = await _context.Stocks.ToListAsync();

            Stock itm = Shop.FirstOrDefault(i => i.ProductID == code);
            if (itm != null)
            {
                CartController.c1.RemoveItem(itm);
                //IncreaseStock(itm);
                 
            }
            
            return RedirectToAction("Index");
        }

        // POST: Shop/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Qty,Price")] Stock stock)
        {
            if (id != stock.ProductID)
            {
                return NotFound();
            }

           
            return View(stock);
        }

        // GET: Shop/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // POST: Shop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockExists(int id)
        {
            return _context.Stocks.Any(e => e.ProductID == id);
        }
    }
}
