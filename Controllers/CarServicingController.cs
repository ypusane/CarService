using Carzz.Models;
using Carzz.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carzz.Controllers
{
    public class CarServicingController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarServicingModel carServicingModel)
        {
            if (ModelState.IsValid)
            {
                CarServicingModel carServicing = new CarServicingModel()
                {
                    ServiceId = carServicingModel.ServiceId,
                    ServiceCategory = carServicingModel.ServiceCategory,
                    AppointmentTime = carServicingModel.AppointmentTime,
                    BookingTime = carServicingModel.BookingTime,
                    ProblemDescription = carServicingModel.ProblemDescription,
                };
                db.CarServingServices.Add(carServicing);
                db.SaveChanges();
                TempData["Acknowledgement"] = "Your service has been booked successfully!";
                TempData["ServiceId"] = carServicing.ServiceId;
                return RedirectToAction("Acknowledgement");
            }
            else
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine($"Property: {state.Key} Error: {error.ErrorMessage}");
                    }
                }
                return View();
            }
        }

        public ActionResult Acknowledgement(CarServicingModel carServicingModel)
        {
            ViewBag.Message = TempData["Acknowledgement"] ?? "No Acknowledgement available.";
            ViewBag.ServiceId= TempData["ServiceId"];
            return View();
        }
    }
    

}