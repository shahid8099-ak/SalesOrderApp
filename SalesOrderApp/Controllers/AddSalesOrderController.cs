using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesOrderApp.Data;
using SalesOrderApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesOrderApp.Controllers
{
    public class AddSalesOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AddSalesOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var salesOrders = await _context.SalesOrders.ToListAsync();
            return View(salesOrders);
        }

        
        public IActionResult Add()
        {
            var model = new SalesOrder();
            model.SalesOrderNo = "SON-" + DateTime.Now.ToString("yyMMmm");//+ new Random().Next(10000, 99999).ToString();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SalesOrder model)
        {
            if (ModelState.IsValid)
            {
                if (model.Items != null)
                {
                    _context.SalesOrders.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index"); // Redirect to the list view
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesOrder = await _context.SalesOrders
                .Include(so => so.Items) // Eager load the related items
                .FirstOrDefaultAsync(m => m.Id == id);

            if (salesOrder == null)
            {
                return NotFound();
            }

            return View(salesOrder);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesOrder = await _context.SalesOrders
                .Include(so => so.Items)
                .FirstOrDefaultAsync(so => so.Id == id);

            if (salesOrder != null)
            {
                _context.SalesOrders.Remove(salesOrder);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
