using Carzz.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Carzz.Startup))]
namespace Carzz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
			// Register OWIN context for ApplicationDbContext, UserManager, and RoleManager
			app.CreatePerOwinContext<ApplicationDbContext>(ApplicationDbContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			app.CreatePerOwinContext<RoleManager<IdentityRole>>((options, context) =>
			{
				var roleStore = new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>());
				return new RoleManager<IdentityRole>(roleStore);
			});
		}

		//private static RoleManager<IdentityRole> RoleManagerSetup(IdentityFactoryOptions<RoleManager<IdentityRole>> options, IOwinContext context)
		//{
		//	var roleStore = new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>());
		//	return new RoleManager<IdentityRole>(roleStore);
		//}
	}
}

