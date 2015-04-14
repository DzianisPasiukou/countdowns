namespace CountdownMvc.App_Start
{
	using System.Web;

	/// <summary>
	/// Redirecting MVC handler
	/// </summary>
	public class RedirectHandler : IHttpHandler
	{
		/// <summary>
		/// The new URL
		/// </summary>
		private string newUrl;

		/// <summary>
		/// Initializes a new instance of the <see cref="RedirectHandler"/> class.
		/// </summary>
		/// <param name="newUrl">The new URL.</param>
		public RedirectHandler(string newUrl)
		{
			this.newUrl = newUrl;
		}

		/// <summary>
		/// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
		/// </summary>
		public bool IsReusable
		{
			get { return true; }
		}

		/// <summary>
		/// Processes the request.
		/// </summary>
		/// <param name="httpContext">The HTTP context.</param>
		public void ProcessRequest(HttpContext httpContext)
		{
			httpContext.Response.Status = "301 Moved Permanently";
			httpContext.Response.StatusCode = 301;
			httpContext.Response.AppendHeader("Location", this.newUrl);
			return;
		}
	}
}