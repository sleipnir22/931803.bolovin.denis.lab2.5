using System;
using System.ComponentModel.DataAnnotations;

namespace Lab5.ViewModels.Hospitals
{
    public class HospitalEditingViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        public string Phones { get; set; }
    }
}
