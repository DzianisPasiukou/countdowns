namespace CountdownBusinessLogic
{
	using Transfer;

	/// <summary>
	/// Gets of sets properties of countdown
	/// </summary>
	public interface ICountdown
	{
		#region Properties

		/// <summary>
		/// Gets or sets the reminder.
		/// </summary>
		/// <value>
		/// The reminder.
		/// </value>
		ReminderDto Reminder { get; set; }

		#endregion
	}
}
