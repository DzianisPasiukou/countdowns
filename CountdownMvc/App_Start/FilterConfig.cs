namespace CountdownMvc.App_Start
{
	using System.Web.Mvc;

	/// <summary>
	/// The filter config for register global filters
	/// </summary>
	public static class FilterConfig
	{
		/// <summary>
		/// Registers the global filters.
		/// </summary>
		/// <param name="filters">The filters.</param>
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
