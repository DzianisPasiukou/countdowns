namespace CountdownWpf.ServiceClient
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Transfer.SmallTransfer;

	/// <summary>
	/// The interface for manipulation with data by WCF service.
	/// </summary>
	public interface IRepository : IDisposable
	{
		#region Public Methods

		/// <summary>
		/// Gets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		string UserName { get; }

		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <returns>The collection of reminder part data transfer objects.</returns>
		ICollection<ReminderPartDto> GetData();

		/// <summary>
		/// Updates the data.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <param name="id">The identifier of reminder.</param>
		/// <param name="state">The state of the reminder.</param>
		void UpdateData(string userName, int id, State state);

		/// <summary>
		/// Connects the specified username.
		/// </summary>
		/// <param name="userName">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns>Is access to connect.</returns>
		Task<bool> Connect(string userName, string password);

		/// <summary>
		/// Disconnects this instance.
		/// </summary>
		void Disconnect();

		#endregion
	}
}
