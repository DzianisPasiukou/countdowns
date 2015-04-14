namespace Transfer
{
	/// <summary>
	/// The instance of days data transfer object.
	/// </summary>
	public class DaysDto
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the identifier of days.
		/// </summary>
		/// <value>
		/// The identifier of days.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the number of the day.
		/// </summary>
		/// <value>
		/// The number.
		/// </value>
		public int Number { get; set; }

		/// <summary>
		/// Gets or sets the name of the day.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		#endregion
	}
}
