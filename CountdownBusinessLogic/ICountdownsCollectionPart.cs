namespace CountdownBusinessLogic
{
	using System.Collections.Generic;

	using Transfer.SmallTransfer;

	/// <summary>
	/// The small countdown factory interface
	/// </summary>
	public interface ICountdownsCollectionPart
	{
		#region Properties

		/// <summary>
		/// Gets or sets the countdowns of small reminder data transfer objects.
		/// </summary>
		/// <value>
		/// The countdowns.
		/// </value>
		IEnumerable<ReminderPartDto> Countdowns { get; set; }

		/// <summary>
		/// Gets or sets the user name.
		/// </summary>
		/// <value>
		/// The user name.
		/// </value>
		string UserName { get; set; }
		#endregion

		#region Methods

		/// <summary>
		/// Gets the countdown by identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>The reminder part data transfer object.</returns>
		ReminderPartDto GetCountdownById(int id);

		#endregion
	}
}
