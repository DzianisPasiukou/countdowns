namespace CountdownMvc
{
	using System.Web.Http;
	using System.Web.Mvc;
	using System.Web.Optimization;
	using System.Web.Routing;

	using CountdownMvc.App_Start;

	/// <summary>
	/// The instance of application.
	/// </summary>
    public class MvcApplication : System.Web.HttpApplication
	{
		#region Protected Methods

		/// <summary>
		/// Application the start.
		/// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
		}

		#endregion
	}
}
