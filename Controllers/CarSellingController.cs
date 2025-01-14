using Carzz.Models;
using Carzz.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YourNamespace.Controllers
{
    public class CarSellingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CarSelling/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CarSellingCreateViewModel carSellingCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                string RCBookpath = ProcessUploadedFile(carSellingCreateViewModel.RCBook);
                string Insurancepath = ProcessUploadedFile(carSellingCreateViewModel.Insurance);
                string FrontPhotopath = ProcessUploadedFile(carSellingCreateViewModel.FrontPhoto);
                string RearPhotopath = ProcessUploadedFile(carSellingCreateViewModel.RearPhoto);
                string LeftPhotopath = ProcessUploadedFile(carSellingCreateViewModel.LeftPhoto);
                string RightPhotopath = ProcessUploadedFile(carSellingCreateViewModel.RightPhoto);


                CarSellingModel carSellingModel = new CarSellingModel()
                {
                    RCBookPath = RCBookpath,
                    InsurancePath = Insurancepath,
                    FrontPhotoPath = FrontPhotopath,
                    RearPhotoPath = RearPhotopath,
                    LeftPhotoPath = LeftPhotopath,
                    RightPhotoPath = RightPhotopath,
                    //ServiceId = carSellingCreateViewModel.ServiceId,
                    ServiceName = carSellingCreateViewModel.ServiceName,
                    SubmittedOn = carSellingCreateViewModel.SubmittedOn,
                };
                db.CarSellingServices.Add(carSellingModel);
                db.SaveChanges();
                TempData["Acknowledgement"] = "Your service has been booked successfully!";
                TempData["ServiceId"] = carSellingModel.ServiceId;
                return RedirectToAction("Acknowledgement");

            }
            else {
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

        public ActionResult Acknowledgement(CarSellingCreateViewModel carSellingCreateViewModel)
        {
            ViewBag.Message= TempData["Acknowledgement"] ?? "No Acknowledgement available.";
            ViewBag.ServiceId= TempData["ServiceId"];
            return View();
        }

        private string ProcessUploadedFile(HttpPostedFileBase httpPostedFileBase)
        {
            string uniqueFileName = null;
            if (httpPostedFileBase != null && httpPostedFileBase.ContentLength > 0)
            {
                // Define the folder where files will be uploaded
                string uploadsFolder = Server.MapPath("~/Images");

                // Ensure the folder exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Create a unique file name
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(httpPostedFileBase.FileName);

                // Get the full file path
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the file
                httpPostedFileBase.SaveAs(filePath);
            }

            return uniqueFileName;
        }
    }

    }
 
