namespace CountdownMvc.App_Start
{
	using System.Web.Mvc;
	using System.Web.Routing;

	/// <summary>
	/// The route config for register routes.
	/// </summary>
	public static class RouteConfig
	{
		/// <summary>
		/// Registers the routes.
		/// </summary>
		/// <param name="routes">The routes.</param>
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.IgnoreRoute("Countdown/Index");

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Countdown", action = "Index", id = UrlParameter.Optional });
		}
	}
}
