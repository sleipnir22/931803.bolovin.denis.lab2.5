using System;
using System.Threading.Tasks;
using Lab5.Models;
using Lab5.ViewModels.Hospitals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Controllers
{
    public class HospitalsController : Controller
    {
        private ApplicationDbContext _db;
        public HospitalsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Hospitals.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HospitalCreatingViewModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Hospitals.Add(new Hospital()
                {
                    Name = model.Name,
                    Address = model.Address,
                    Phones = model.Phones
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
                var hospital = await _db.Hospitals.Include(h => h.Doctors)
                    .FirstOrDefaultAsync(h => h.Id == id);

                if (hospital is null)
                    return NotFound();

                return View(hospital);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (ModelState.IsValid)
            {
                var hospital = await _db.Hospitals.FirstOrDefaultAsync(h => h.Id == id);

                if (hospital is null)
                    return NotFound();

                var model = new HospitalEditingViewModel
                {
                    Id = hospital.Id,
                    Name = hospital.Name,
                    Address = hospital.Address,
                    Phones = hospital.Phones
                };

                return View(model);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HospitalEditingViewModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Hospitals.Update(new Hospital
                {
                    Id = model.Id,
                    Name = model.Name,
                    Address = model.Address,
                    Phones = model.Phones
                });
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                var hospital = await _db.Hospitals.FirstOrDefaultAsync(h => h.Id == id);

                if (hospital is null)
                    return NotFound();

                _db.Hospitals.Remove(hospital);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return BadRequest();
        }
    }
}
