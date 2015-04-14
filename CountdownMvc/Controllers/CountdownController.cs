namespace CountdownMvc.Controllers
{
	using System.Web.Mvc;

	/// <summary>
	/// Countdown MVC controller.
	/// </summary>
	[Authorize]
	public class CountdownController : Controller
	{
		#region Public Methods

		/// <summary>
		///  GET: /Countdown/
		/// </summary>
		/// <returns>View page Index.</returns>
		public ActionResult Index()
		{
			return this.View();
		}

		#endregion
	}
}