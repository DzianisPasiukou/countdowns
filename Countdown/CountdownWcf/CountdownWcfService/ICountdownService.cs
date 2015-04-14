namespace CountdownWcfService
{
	using System;
	using System.Collections.Generic;
	using System.ServiceModel;
	using System.Threading.Tasks;

	using CountdownBusinessLogic;

	using Transfer.SmallTransfer;

	/// <summary>
	/// The interface for WCF service with service contract.
	/// </summary>
	[ServiceContract(CallbackContract = typeof(ICountdownCallback))]
	public interface ICountdownService
	{
		#region Methods

		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <param name="userName">The user name.</param>
		/// <returns>The collection of reminder part data transfer objects.</returns>
		[OperationContract]
		IEnumerable<ReminderPartDto> GetData(string userName);

		/// <summary>
		/// Updates the data.
		/// </summary>
		/// <param name="userName">The user name.</param>
		/// <param name="id">The identifier reminder.</param>
		/// <param name="state">The state.</param>
		[OperationContract]
		void UpdateData(string userName, int id, State state);

		/// <summary>
		/// Called when user is connected.
		/// </summary>
		/// <param name="userName">The user name.</param>
		/// <param name="password">The password.</param>
		/// <returns>Is connected.</returns>
		[OperationContract]
		bool OnConnectUser(string userName, string password);

		/// <summary>
		/// Called when user id disconnected.
		/// </summary>
		/// <param name="userName">The user name.</param>
		[OperationContract]
		void OnDisconnectUser(string userName);

		#endregion
	}
}
