using Carzz.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;
using Carzz.ViewModels;
using Microsoft.AspNet.Identity.Owin;

namespace Carzz.Controllers
{
	public class ManageRolesController : Controller
	{
		private ApplicationRoleManager _roleManager;
		private UserManager<ApplicationUser> _userManager;

		public ManageRolesController()
		{

		}

		public ManageRolesController(ApplicationRoleManager roleManager,ApplicationUserManager userManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
		}

		public ApplicationRoleManager RoleManager
		{
			get
			{
				return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
			}
			private set
			{
				_roleManager = value;
			}
		}
		public ApplicationUserManager UserManager
		{
			get
			{
				return (ApplicationUserManager)(_userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>());
			}
			private set
			{
				_userManager = value;
			}
		}

		[HttpGet]
		public ActionResult CreateRole()
		{
			return View();
		}

		[HttpPost]
		public async Task<ActionResult> CreateRole(CreateRoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				var applicationRole = new ApplicationRole
				{
					Name = model.RoleName
				};

				var result = await RoleManager.CreateAsync(applicationRole);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}

				foreach (var identityError in result.Errors)
				{
					ModelState.AddModelError("", identityError);
				}
			}

			return View(model);
		}

		[HttpGet]
		public ActionResult ListRoles()
		{
			var roles = RoleManager.Roles.ToList();
			return View(roles);
		}

		[HttpGet]
		public async Task<ActionResult> EditRole(string id)
		{
			var role = await RoleManager.FindByIdAsync(id);
			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
				return View("NotFound");
			}

			var model = new EditRolesViewModel
			{
				Id = role.Id,
				RoleName = role.Name,
				Users = new List<string>()
			};

			// Iterate through all users to check if they belong to this role
			foreach (var user in _userManager.Users.ToList())
			{
				if (await _userManager.IsInRoleAsync(user.Id, role.Name))
				{
					model.Users.Add(user.UserName);
				}
			}

			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> EditRole(EditRolesViewModel model)
		{
			var role = await RoleManager.FindByIdAsync(model.Id);
			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
				return View("NotFound");
			}

			role.Name = model.RoleName;
			var result = await RoleManager.UpdateAsync(role);

			if (result.Succeeded)
			{
				return RedirectToAction("ListRoles");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error);
			}

			return View(model);
		}

		[HttpGet]
		public async Task<ActionResult> EditUserRole(string roleId)
		{
			ViewBag.RoleId = roleId;
			var role = await RoleManager.FindByIdAsync(roleId);
			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
				return View("NotFound");
			}

			var model = new List<UserRoleViewModel>();
			foreach (var user in _userManager.Users.ToList())
			{
				var userRoleViewModel = new UserRoleViewModel
				{
					UserId = user.Id,
					UserName = user.UserName,
					IsSelected = await _userManager.IsInRoleAsync(user.Id, role.Name)
				};
				model.Add(userRoleViewModel);
			}

			return View(model);
		}

		[HttpPost]
		public async Task<ActionResult> EditUserRole(List<UserRoleViewModel> model, string roleId)
		{
			var role = await RoleManager.FindByIdAsync(roleId);
			if (role == null)
			{
				ViewBag.ErrorMessage = $"Role with Id = {roleId} Not Found";
				return View("NotFound");
			}

			foreach (var userRole in model)
			{
				var user = await _userManager.FindByIdAsync(userRole.UserId);

				if (userRole.IsSelected && !(await _userManager.IsInRoleAsync(user.Id, role.Name)))
				{
					await _userManager.AddToRoleAsync(user.Id, role.Name);
				}
				else if (!userRole.IsSelected && await _userManager.IsInRoleAsync(user.Id, role.Name))
				{
					await _userManager.RemoveFromRoleAsync(user.Id, role.Name);
				}
			}

			return RedirectToAction("EditRole", new { Id = roleId });
		}
	}
}
