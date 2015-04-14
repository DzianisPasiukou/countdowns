namespace Transfer
{
	/// <summary>
	/// The instance of settings data transfer objects.
	/// </summary>
	public class SettingsDto
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the identifier of setting data transfer object.
		/// </summary>
		/// <value>
		/// The identifier of setting data transfer object.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the name of setting.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description of setting.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the type of reminder identifier.
		/// </summary>
		/// <value>
		/// The type of reminder identifier.
		/// </value>
		public int TypeOfReminderId { get; set; }

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
