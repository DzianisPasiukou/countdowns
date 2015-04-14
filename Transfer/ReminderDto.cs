namespace Transfer
{
	using System.Collections.Generic;

	/// <summary>
	/// The instance of reminder data transfer object.
	/// </summary>
	public class ReminderDto
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the identifier of reminder.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of reminder.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description of reminder.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets the type of reminder.
		/// </summary>
		/// <value>
		/// The type of reminder.
		/// </value>
		public TypeOfReminderDto TypeOfReminder { get; set; }

		/// <summary>
		/// Gets or sets the type of reminder setting.
		/// </summary>
		/// <value>
		/// The type of reminder setting.
		/// </value>
		public CountdownSettingsDto CountdownSettings { get; set; }

		/// <summary>
		/// Gets or sets the progress settings.
		/// </summary>
		/// <value>
		/// The progress settings.
		/// </value>
		public ProgressSettingsDto ProgressSettings { get; set; }

		/// <summary>
		/// Gets or sets the reminder settings.
		/// </summary>
		/// <value>
		/// The reminder settings.
		/// </value>
		public ICollection<ReminderSettingsDto> ReminderSettings { get; set; }

		/// <summary>
		/// Gets or sets the exercises.
		/// </summary>
		/// <value>
		/// The exercises.
		/// </value>
		public ICollection<ExercisesDto> Exercises { get; set; }

		#endregion
	}
}
