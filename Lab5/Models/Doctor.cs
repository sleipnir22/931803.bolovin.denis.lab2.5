using System;
using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Speciality { get; set; }
        
        public Guid? HospitalId { get; set; }
        public Hospital Hospital { get; set; }
    }
}
