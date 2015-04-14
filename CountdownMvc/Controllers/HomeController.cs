namespace CountdownMvc.Controllers
{
	using System.Web.Mvc;

	/// <summary>
	/// The instance of home controller.
	/// </summary>
    public class HomeController : Controller
	{
		#region Public Methods

		/// <summary>
		/// Indexes this instance.
		/// </summary>
		/// <returns>The view of index.</returns>
        public ActionResult Index()
        {
            return this.View();
		}

		#endregion
	}
}