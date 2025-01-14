using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Carzz.ViewModels 
{
    public class CarSellingCreateViewModel
    {
        [Key]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "RC Book is required")]
        [Display(Name = "RC Book")]
        public HttpPostedFileBase RCBook { get; set; }

        [Required(ErrorMessage = "Insurance is required")]
        [Display(Name = "Insurance")]
        public HttpPostedFileBase Insurance { get; set; }

        [Required(ErrorMessage = "Front View Photo is required")]
        [Display(Name = "Front View Photo")]
        public HttpPostedFileBase FrontPhoto { get; set; }

        [Required(ErrorMessage = "Rear View Photo is required")]
        [Display(Name = "Rear View Photo")]
        public HttpPostedFileBase RearPhoto { get; set; }

        [Required(ErrorMessage = "Left View Photo is required")]
        [Display(Name = "Left View Photo")]
        public HttpPostedFileBase LeftPhoto { get; set; }

        [Required(ErrorMessage = "Right View Photo is required")]
        [Display(Name = "Right View Photo")]
        public HttpPostedFileBase RightPhoto { get; set; }

        public string ServiceName { get; set; } = "Car Selling";

        public DateTime SubmittedOn { get; set; } = DateTime.Now;
    }
}