namespace Transfer
{
	/// <summary>
	/// The instance of type of reminder data transfer object.
	/// </summary>
	public class TypeOfReminderDto
	{
		#region PublicProperties

		/// <summary>
		/// Gets or sets the identifier of type of reminder.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of type of reminder.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description of type of reminder.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		#endregion
	}
}
