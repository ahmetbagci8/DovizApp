using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DovizApp.Models;
using System.Net;

namespace DovizApp.Controllers
{
    public class DovizlerController : Controller
    {
        private readonly DovizAppContext _context;

        public DovizlerController(DovizAppContext context)
        {
            _context = context;
        }

        // GET: Dovizler
        public async Task<IActionResult> Index()
        {
            return View(await _context.Doviz.ToListAsync());
        }

        // GET: Dovizler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doviz = await _context.Doviz
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doviz == null)
            {
                return NotFound();
            }

            return View(doviz);
        }

        // GET: Dovizler/Create
        public IActionResult Create()
        {
            return View();
        }

       

        // POST: Dovizler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Buy,Sell,Date")] Doviz doviz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doviz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doviz);
        }

        [HttpGet]
        public IActionResult CreateAuto()
        {
            Doviz doviz = new Doviz();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAuto(Doviz doviz)
        {
            WebClient client = new WebClient();
            String downloadedString = client.DownloadString("https://api.canlidoviz.com/web/items?marketId=1&type=0");
            char[] seps = { ' ', ',', ':', '"' };
            string[] data = downloadedString.Split(seps);
            int i = 0;
            while (data[i] != "buyPrice")
            {
                i++;
            }
            i++;
            i++;
            doviz.Buy = (float)Convert.ToDouble(data[i]);
            while (data[i] != "sellPrice")
            {
                i++;
            }
            i++;
            i++;
            doviz.Sell = (float)Convert.ToDouble(data[i]);
            doviz.Date = DateTime.Now;


            if (ModelState.IsValid)                             
            {
                _context.Add(doviz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doviz);
        }

        // GET: Dovizler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doviz = await _context.Doviz.FindAsync(id);
            if (doviz == null)
            {
                return NotFound();
            }
            return View(doviz);
        }

        // POST: Dovizler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Buy,Sell,Date")] Doviz doviz)
        {
            if (id != doviz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doviz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DovizExists(doviz.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(doviz);
        }

        // GET: Dovizler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doviz = await _context.Doviz
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doviz == null)
            {
                return NotFound();
            }

            return View(doviz);
        }

        // POST: Dovizler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doviz = await _context.Doviz.FindAsync(id);
            _context.Doviz.Remove(doviz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DovizExists(int id)
        {
            return _context.Doviz.Any(e => e.Id == id);
        }
    }
}
