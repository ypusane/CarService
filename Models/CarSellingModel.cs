using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Carzz.Models
{
        public class CarSellingModel
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ServiceId { get; set; }

            [Required(ErrorMessage = "RC Book is required")]
            [Display(Name = "RC Book")]
            public string RCBookPath { get; set; }

            [Required(ErrorMessage = "Insurance is required")]
            [Display(Name = "Insurance")]
            public string InsurancePath { get; set; }

            [Required(ErrorMessage = "Front View Photo is required")]
            [Display(Name = "Front View Photo")]
            public string FrontPhotoPath { get; set; }

            [Required(ErrorMessage = "Rear View Photo is required")]
            [Display(Name = "Rear View Photo")]
            public string RearPhotoPath { get; set; }

            [Required(ErrorMessage = "Left View Photo is required")]
            [Display(Name = "Left View Photo")]
            public string LeftPhotoPath { get; set; }

            [Required(ErrorMessage = "Right View Photo is required")]
            [Display(Name = "Right View Photo")]
            public string RightPhotoPath { get; set; }

            public string ServiceName { get; set; } = "Car Selling";

            public DateTime SubmittedOn { get; set; } = DateTime.Now;
        }
    }
