
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab5.ViewModels.Doctors
{
    public class DoctorCreatingViewModel
    { 
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Speciality { get; set; }

        public Guid? HospitalId { get; set; }

        public IEnumerable<(Guid Id, string Name)> Hospitals { get; set; }
    }
}
