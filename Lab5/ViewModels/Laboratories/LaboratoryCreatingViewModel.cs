
using System;
using System.ComponentModel.DataAnnotations;

namespace Lab5.ViewModels.Laboratories
{
    public class LaboratoryCreatingViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
