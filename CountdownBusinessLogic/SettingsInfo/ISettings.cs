namespace CountdownBusinessLogic.SettingsInfo
{
	using System.Collections.Generic;

	using Transfer;

	/// <summary>
	/// Existing settings.
	/// </summary>
	public interface ISettings
	{
		#region Properties

		/// <summary>
		/// Gets or sets the settings.
		/// </summary>
		/// <value>
		/// The settings.
		/// </value>
		IEnumerable<SettingsDto> Settings { get; set; }

		#endregion
	}
}
