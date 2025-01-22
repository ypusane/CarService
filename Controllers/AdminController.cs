using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Carzz.Models;

namespace Carzz.Controllers
{
    public class AdminController : Controller
    {
		private ApplicationUserManager _userManager;
		private ApplicationDbContext _context;

		public AdminController()
        {
			_context = new ApplicationDbContext();
		}
		public AdminController(ApplicationUserManager userManager)
		{
			_userManager = userManager;
			_context = new ApplicationDbContext();
		}

		// GET: Admin
		public ActionResult Index()
        {
            return View();
        }

		//List all Customers
        public ActionResult UserManagement() 
        {
			var customers = _context.Users.ToList(); // Assuming Users table holds customer data
			return View(customers);
        }

        public ActionResult CarServiceManagement()
        {
            return View();
        }
		public ActionResult CarSellingManagement()
		{
			return View();
		}

	}
}