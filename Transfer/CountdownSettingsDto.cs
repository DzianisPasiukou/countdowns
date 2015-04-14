namespace Transfer
{
	using System.Collections.Generic;

	/// <summary>
	/// The instance of countdown settings data transfer object.
	/// </summary>
	public class CountdownSettingsDto
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the duration.
		/// </summary>
		/// <value>
		/// The duration.
		/// </value>
		public int Duration { get; set; }

		/// <summary>
		/// Gets or sets the days.
		/// </summary>
		/// <value>
		/// The days.
		/// </value>
		public ICollection<DaysDto> Days { get; set; }

		/// <summary>
		/// Gets or sets the weeks.
		/// </summary>
		/// <value>
		/// The weeks.
		/// </value>
		public ICollection<WeeksDto> Weeks { get; set; }

		/// <summary>
		/// Gets or sets the months.
		/// </summary>
		/// <value>
		/// The months.
		/// </value>
		public ICollection<MonthsDto> Months { get; set; }

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
