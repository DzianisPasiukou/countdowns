namespace Transfer
{
	using System;

	/// <summary>
	/// The instance of progress setting data transfer object.
	/// </summary>
	public class ProgressSettingsDto
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the interval of progress.
		/// </summary>
		/// <value>
		/// The interval.
		/// </value>
		public int Interval { get; set; }

		/// <summary>
		/// Gets or sets the duration of progress.
		/// </summary>
		/// <value>
		/// The duration.
		/// </value>
		public int Duration { get; set; }

		/// <summary>
		/// Gets or sets the start of type of reminder setting.
		/// </summary>
		/// <value>
		/// The start.
		/// </value>
		public System.DateTime Start { get; set; }

		/// <summary>
		/// Gets or sets the end.
		/// </summary>
		/// <value>
		/// The end.
		/// </value>
		public System.DateTime End { get; set; }

		/// <summary>
		/// Gets or sets the identifier of Type of Reminder.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public int Id { get; set; }

		#endregion
	}
}
