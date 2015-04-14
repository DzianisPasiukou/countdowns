namespace CountdownMvc.Controllers
{
	using System.Web.Mvc;

	/// <summary>
	/// The instance for logs controller.
	/// </summary>
	public class LogsController : Controller
	{
		/// <summary>
		/// Indexes this instance.
		/// </summary>
		/// <returns>The index view.</returns>
		[Authorize(Users = "admin")]
		public ActionResult Index()
		{
			return this.View();
		}
	}
}