namespace Transfer.SmallTransfer
{
	/// <summary>
	/// The state of reminder part data transfer object.
	/// </summary>
	public enum State
	{
		/// <summary>
		/// The none
		/// </summary>
		None,

		/// <summary>
		/// The updated
		/// </summary>
		Updated,

		/// <summary>
		/// The created
		/// </summary>
		Created,

		/// <summary>
		/// The deleted
		/// </summary>
		Deleted,

		/// <summary>
		/// The activate
		/// </summary>
		Activate,

		/// <summary>
		/// The postpone.
		/// </summary>
		Postpone
	}

	/// <summary>
	/// The instance of the small reminder data transfer object.
	/// </summary>
	public class ReminderPartDto
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the progress settings.
		/// </summary>
		/// <value>
		/// The progress settings.
		/// </value>
		public ProgressSettingsDto ProgressSettings { get; set; }

		/// <summary>
		/// Gets or sets the countdowns settings.
		/// </summary>
		/// <value>
		/// The countdowns settings.
		/// </value>
		public CountdownSettingsDto CountdownsSettings { get; set; }

		/// <summary>
		/// Gets or sets the state.
		/// </summary>
		/// <value>
		/// The state.
		/// </value>
		public State State { get; set; }

		#endregion
	}
}
