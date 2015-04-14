namespace CountdownBusinessLogic
{
	using System.Collections.Generic;

	/// <summary>
	/// Get countdowns from user and get countdown by id.
	/// </summary>
	public interface ICountdownCollection : IActions, IUpdater
	{
		#region Properties

		/// <summary>
		/// Gets or sets the countdowns.
		/// </summary>
		/// <value>
		/// The countdowns.
		/// </value>
		IEnumerable<Countdown> Countdowns { get; set; }

		/// <summary>
		/// Gets or sets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		string UserName { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Gets the countdown by identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>Countdown object</returns>
		Countdown GetCountdownById(int id);

		#endregion
	}
}
