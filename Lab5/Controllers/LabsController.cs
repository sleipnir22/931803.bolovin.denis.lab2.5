using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Lab5.Models;
using Lab5.ViewModels.Laboratories;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Controllers
{
    public class LabsController : Controller
    {
        private ApplicationDbContext _db;

        public LabsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var labs = await _db.Laboratories.ToListAsync();
            return View(labs);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LaboratoryCreatingViewModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Laboratories.Add(new Laboratory
                {
                    Name = model.Name,
                    Address = model.Address,
                });
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (ModelState.IsValid)
            {
                var lab = await _db.Laboratories.FirstOrDefaultAsync(l => l.Id == id);

                if (lab is null)
                    return NotFound();

                return View(lab);

            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (ModelState.IsValid)
            {
                var lab = await _db.Laboratories.FirstOrDefaultAsync(l => l.Id == id);
                if (lab is null)
                    return NotFound();

                return View(lab);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Laboratory lab)
        {
            if (ModelState.IsValid)
            {
                _db.Laboratories.Update(lab);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(lab);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                var lab = await _db.Laboratories.FirstOrDefaultAsync(l => l.Id == id);

                if (lab is null)
                    return NotFound();

                _db.Laboratories.Remove(lab);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return BadRequest();
        }
    }
}
