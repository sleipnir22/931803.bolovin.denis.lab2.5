using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab5.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Controllers
{
    public class PatientsController : Controller
    {
        private ApplicationDbContext _db;
        public PatientsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Patients.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _db.Patients.Add(patient);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(patient);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (ModelState.IsValid)
            {
                var patient = await _db.Patients.FirstOrDefaultAsync(p => p.Id == id);

                if (patient is null)
                    return NotFound();

                return View(patient);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (ModelState.IsValid)
            {
                var patient = await _db.Patients.FirstOrDefaultAsync(p => p.Id == id);

                if (patient is null)
                    return NotFound();
                

                return View(patient);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _db.Patients.Update(patient);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(patient);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                var patient = await _db.Patients.FirstOrDefaultAsync(p => p.Id == id);

                if (patient is null)
                    return NotFound();

                _db.Patients.Remove(patient);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return BadRequest();
        }

    }
}
