namespace CountdownBusinessLogic.SettingsInfo
{
	using System.Collections.Generic;

	using CountdownBusinessLogic.Manager;

	using CountdownDataBaseLayer;
	using CountdownDataBaseLayer.Repo;

	using Transfer;

	/// <summary>
	/// The instance of settings collection for provide success to settings.
	/// </summary>
	public class SettingsCollection : ISettings
	{
		#region Private Fields

		/// <summary>
		/// The settings.
		/// </summary>
		private IEnumerable<SettingsDto> settings;

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets or sets the settings.
		/// </summary>
		/// <value>
		/// The settings.
		/// </value>
		public IEnumerable<SettingsDto> Settings
		{
			get
			{
				if (this.settings == null)
				{
					using (IRepo<Settings> settingsRepo = new SettingsRepo())
					{
						this.settings = Initializer.InitSettings(settingsRepo.GetAll());
					}
				}

				return this.settings;
			}

			set
			{
				this.settings = value;
			}
		}

		#endregion
	}
}
