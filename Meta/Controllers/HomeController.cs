using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Meta.Models;
using Microsoft.EntityFrameworkCore;

namespace Meta.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await db.Employees.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee emp)
        {
            db.Employees.Add(emp);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Employee emp = await db.Employees.FirstOrDefaultAsync(p => p.Id == id);
                if (emp != null)
                    return View(emp);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Employee emp)
        {
            db.Employees.Update(emp);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Employee emp = await db.Employees.FirstOrDefaultAsync(p => p.Id == id);
                if (emp != null)
                    return View(emp);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Employee emp = await db.Employees.FirstOrDefaultAsync(p => p.Id == id);
                emp = await db.Employees.FirstOrDefaultAsync(p => p.Id == id);
                if (emp != null)
                {
                    db.Employees.Remove(emp);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
