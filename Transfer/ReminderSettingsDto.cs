namespace Transfer
{
	/// <summary>
	/// The instance of reminder setting data transfer object.
	/// </summary>
	public class ReminderSettingsDto
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the identifier of reminder setting data transfer object.
		/// </summary>
		/// <value>
		/// The identifier of reminder setting data transfer object.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of reminder setting.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description of reminder setting.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the value of the setting reminder.
		/// </summary>
		/// <value>
		/// The value of the setting reminder.
		/// </value>
		public string Value { get; set; }

		/// <summary>
		/// Gets or sets the name property.
		/// </summary>
		/// <value>
		/// The name property.
		/// </value>
		public string NameProperty { get; set; }

		/// <summary>
		/// Gets or sets the name element.
		/// </summary>
		/// <value>
		/// The name element.
		/// </value>
        public string NameElement { get; set; }

		#endregion
	}
}
