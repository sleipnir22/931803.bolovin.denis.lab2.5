using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lab5.Models
{
    public class Hospital
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
        public string Phones { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
