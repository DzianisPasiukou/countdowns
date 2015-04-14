namespace CountdownMvc.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Web.Http;

	using CountdownBusinessLogic.SettingsInfo;

	using LoggingManager;

	using Transfer;

	/// <summary>
	/// Web API controller for settings.
	/// </summary>
	[Authorize]
	public class SettingsApiController : ApiController
	{
		#region Private Fields

		/// <summary>
		/// The settings.
		/// </summary>
		private ISettings settings;

		/// <summary>
		/// The logger.
		/// </summary>
		private ILogger<SettingsApiController> logger;

		#endregion

		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsApiController" /> class.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <param name="logger">The logger.</param>
		/// <exception cref="System.ArgumentNullException">ISettings is null!</exception>
		public SettingsApiController(ISettings settings, ILogger<SettingsApiController> logger)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings", "ISettings is null.");
			}

			this.settings = settings;

			if (logger == null)
			{
				throw new ArgumentNullException("logger", "ILogger is null.");
			}

			this.logger = logger;

			this.logger.UserName = User.Identity.Name;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets this settings.
		/// </summary>
		/// <returns>The collection of settings transfer objects.</returns>
		public IEnumerable<SettingsDto> Get()
		{
			IEnumerable<SettingsDto> settings = this.settings.Settings;

			this.logger.Write(
				string.Format(
				CultureInfo.InvariantCulture,
				"Get the full settings.\n Return value is:\n {0}",
				MySerialization.SerializeToJSON(settings)),
				TypeMessage.Info);

			return settings;
		}

		#endregion
	}
}