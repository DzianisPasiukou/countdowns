namespace CountdownBusinessLogic
{
	/// <summary>
	/// Get actions of countdowns.
	/// </summary>
	public interface IActions
	{
		#region Methods

		/// <summary>
		/// Activates this reminder.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		void Activate(Countdown countdown);

		/// <summary>
		/// Postpones reminder by the specified time.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		/// <param name="time">The time of postpone.</param>
		void Postpone(Countdown countdown, int time);

		/// <summary>
		/// Closes this countdown.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		void Close(Countdown countdown);

		/// <summary>
		/// Creates this instance of countdown.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		void Create(Countdown countdown);

		#endregion
	}
}
