namespace CountdownMvc.App_Start
{
	using System.Web.Http;

	/// <summary>
	/// The Web API config for register Web API routes.
	/// </summary>
	public static class WebApiConfig
	{
		/// <summary>
		/// Registers the specified configuration.
		/// </summary>
		/// <param name="config">The configuration.</param>
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional });

			config.Routes.MapHttpRoute(
				name: "FixedApi",
				routeTemplate: "Countdown/Index/api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional });
		}
	}
}
