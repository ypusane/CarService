using Carzz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carzz.ViewModels
{
	public class UserRoleViewModel
	{
		public UserRoleViewModel() { }

		public UserRoleViewModel(ApplicationRole role) { 	
			UserId = role.Id;
			UserName = role.Name;
		}

		public string UserId { get; set; }
		public string UserName { get; set; }
		public bool IsSelected { get; set; }
	}
}