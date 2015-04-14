namespace CountdownBusinessLogic
{
	/// <summary>
	/// Interface for update reminder.
	/// </summary>
	public interface IUpdater
	{
		#region Methods

		/// <summary>
		/// Full the update of the reminder.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		void FullUpdate(Countdown countdown);

		/// <summary>
		/// Updates the settings of countdown.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		void UpdateProgressSettings(Countdown countdown);

		#endregion
	}
}
