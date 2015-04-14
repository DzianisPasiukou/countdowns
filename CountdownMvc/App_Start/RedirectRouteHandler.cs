namespace CountdownMvc.App_Start
{
	using System.Web;
	using System.Web.Routing;

	/// <summary>
	/// Redirect Route Handler.
	/// </summary>
	public class RedirectRouteHandler : IRouteHandler
	{
		#region Private Fields

		/// <summary>
		/// The new URL
		/// </summary>
		private string newUrl;

		#endregion

		#region Public Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="RedirectRouteHandler"/> class.
		/// </summary>
		/// <param name="newUrl">The new URL.</param>
		public RedirectRouteHandler(string newUrl)
		{
			this.newUrl = newUrl;
		}

		/// <summary>
		/// Provides the object that processes the request.
		/// </summary>
		/// <param name="requestContext">An object that encapsulates information about the request.</param>
		/// <returns>
		/// An object that processes the request.
		/// </returns>
		public IHttpHandler GetHttpHandler(RequestContext requestContext)
		{
			return new RedirectHandler(this.newUrl);
		}

		#endregion
	}
}
