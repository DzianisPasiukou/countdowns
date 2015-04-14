namespace CountdownBusinessLogic.Service
{
	using Transfer.SmallTransfer;

	/// <summary>
	/// The instance for client which must notify user about changes.
	/// </summary>
	public interface IClient
	{
		/// <summary>
		/// Notifies the about changes.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <param name="id">The identifier.</param>
		/// <param name="state">The state.</param>
		void NotifyAboutChanges(string userName, int id, State state);
	}
}
