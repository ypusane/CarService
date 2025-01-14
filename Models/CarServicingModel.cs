using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Carzz.Models
{
    public class CarServicingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }

        [Required]
        public Service ServiceCategory { get; set; }

        [Required]
        public DateTime AppointmentTime{ get; set; }

        public DateTime BookingTime { get; set; } = DateTime.Now;

        public string ProblemDescription{ get; set; }

    }
}