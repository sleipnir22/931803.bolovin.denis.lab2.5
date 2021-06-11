using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Lab5.Models;
using Microsoft.AspNetCore.Mvc;
using Lab5.ViewModels.Doctors;
using Microsoft.EntityFrameworkCore;

namespace Lab5.Controllers
{
    public class DoctorsController : Controller
    {
        private ApplicationDbContext _db;

        public DoctorsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _db.Doctors.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var hospitals = await _db.Hospitals
                .Select(h => new {h.Id, h.Name})
                .ToListAsync();

            var model = new DoctorCreatingViewModel
            {
                Hospitals = hospitals.Select(h => (h.Id, h.Name))
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DoctorCreatingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.HospitalId != null)
                {
                    var hospital = await _db.Hospitals
                        .FirstOrDefaultAsync(h => h.Id == model.HospitalId);

                    if (hospital == null)
                    {
                        ModelState.AddModelError("HospitalId", "Hospital was not found");
                        goto Err;
                    }
                }

                _db.Doctors.Add(new Doctor()
                {
                    Name = model.Name,
                    Speciality = model.Speciality,
                    HospitalId = model.HospitalId
                });
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            Err:
            var hospitals = await _db.Hospitals
                .Select(h => new { h.Id, h.Name })
                .ToListAsync();

            model.Hospitals = hospitals.Select(h => (h.Id, h.Name));
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (ModelState.IsValid)
            {
                var doctor = await _db.Doctors.FirstOrDefaultAsync(d => d.Id == id);

                if (doctor is null)
                    return NotFound();

                var hospitals = await _db.Hospitals
                    .Select(h => new { h.Id, h.Name })
                    .ToListAsync();

                var model = new DoctorEditingViewModel
                {
                    Id = id,
                    Name = doctor.Name,
                    Speciality = doctor.Speciality,
                    HospitalId = doctor.HospitalId,
                    Hospitals = hospitals.Select(h => (h.Id, h.Name))
                };

                return View(model);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DoctorEditingViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.HospitalId != null)
                {
                    var hospital = await _db.Hospitals.FirstOrDefaultAsync(h => h.Id == model.HospitalId);
                    if (hospital is null)
                        return BadRequest("Hospital is not exist");
                }

                _db.Doctors.Update(new Doctor
                {
                    Id = model.Id,
                    Name = model.Name,
                    Speciality = model.Speciality,
                    HospitalId = model.HospitalId
                });
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            var hospitals = await _db.Hospitals
                .Select(h => new { h.Id, h.Name })
                .ToListAsync();

            model.Hospitals = hospitals.Select(h => (h.Id, h.Name));
            return View(model);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (ModelState.IsValid)
            {
                var doctor = await _db.Doctors.FirstOrDefaultAsync(d => d.Id == id);

                if (doctor is null)
                    return NotFound();

                _db.Doctors.Remove(doctor);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return BadRequest();
        }

        public async Task<IActionResult> Details([Required] Guid id)
        {
            if (ModelState.IsValid)
            {
                var doctor = await _db.Doctors.Include(d => d.Hospital)
                    .FirstOrDefaultAsync(d => d.Id == id);

                if (doctor is null)
                {
                    return NotFound();
                }

                return View(doctor);
            }

            return BadRequest();
        }
    }
}
