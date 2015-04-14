using System.Web.Http;

using CountdownMvc.Models.UserIdentity;

using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;

using Owin;

[assembly: OwinStartupAttribute(typeof(CountdownMvc.Startup))]

namespace CountdownMvc
{
	/// <summary>
	/// The instance to the startup by the OWIN.
	/// </summary>
	public partial class Startup
	{
		#region Public Methods

		/// <summary>
		/// Configurations the specified application.
		/// </summary>
		/// <param name="app">The application.</param>
		public void Configuration(IAppBuilder app)
		{
			// Configure the db context, user manager and role 
			// manager to use a single instance per request
			app.CreatePerOwinContext<CustomUserManager>(CreateUserManager);

			// Enable the application to use a cookie to store information for the 
			// signed in user and to use a cookie to temporarily store information 
			// about a user logging in with a third party login provider 
			// Configure the sign in cookie
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Account/Login"),
			});

			//// Use a cookie to temporarily store information about a user logging in with a third party login provider
			////app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

			//// Uncomment the following lines to enable logging in with third party login providers
			////app.UseMicrosoftAccountAuthentication(
			////	clientId: "",
			////	clientSecret: "");

			////app.UseTwitterAuthentication(
			////   consumerKey: "",
			////   consumerSecret: "");

			////app.UseFacebookAuthentication(
			////   appId: "",
			////   appSecret: "");

			////app.UseGoogleAuthentication();
		}

		#endregion

		#region Private Static Methods

		/// <summary>
		/// Creates the user manager.
		/// </summary>
		/// <returns>The custom user manager.</returns>
		private static CustomUserManager CreateUserManager()
		{
			return GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(CustomUserManager)) as CustomUserManager;
		}

		#endregion
	}
}
