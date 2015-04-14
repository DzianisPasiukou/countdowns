namespace Transfer
{
	/// <summary>
	/// The instance of month data transfer object.
	/// </summary>
	public class MonthsDto
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the identifier of months.
		/// </summary>
		/// <value>
		/// The identifier of months.
		/// </value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the number of month.
		/// </summary>
		/// <value>
		/// The number.
		/// </value>
		public int Number { get; set; }

		/// <summary>
		/// Gets or sets the name of month.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; set; }

		#endregion
	}
}
