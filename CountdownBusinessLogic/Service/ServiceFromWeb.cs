namespace CountdownBusinessLogic.Service
{
	using System;
	using System.ServiceModel;
	using System.Threading.Tasks;

	using CountdownBusinessLogic.CountdownServiceReference;

	using LoggingManager;

	using NLog;

	using Transfer.SmallTransfer;

	/// <summary>
	/// The static class for using WCF service methods on client.
	/// </summary>
	public class ServiceFromWeb : IClient
	{
		#region Private Fields

		/// <summary>
		/// The client to WCF service.
		/// </summary>
		private ICountdownService client;

		#endregion

		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceFromWeb" /> class.
		/// </summary>
		/// <param name="client">The client.</param>
		/// <exception cref="System.ArgumentNullException">ICountdownService is null.
		/// </exception>
		public ServiceFromWeb(ICountdownService client)
		{
			if (client == null)
			{
				throw new ArgumentNullException("client", "ICountdownService is null.");
			}

			this.client = client;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Notifies the about changes.
		/// </summary>
		/// <param name="userName">The user name.</param>
		/// <param name="id">The identifier.</param>
		/// <param name="state">The state.</param>
		/// <exception cref="System.CommunicationException">Error in connection to WCF service for notify about changes to client.</exception>
		public void NotifyAboutChanges(string userName, int id, State state)
		{
			try
			{
				this.client.BeginUpdateData(userName, id, state, this.Notify_Callback, null);
			}
			catch (Exception)
			{
				//TODO: Logging exceptiuons.
				//throw new Exception("Error in connection to WCF service for notify about changes to client.", e);
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Notify the callback.
		/// </summary>
		/// <param name="ar">The async result.</param>
		private void Notify_Callback(IAsyncResult ar)
		{
			if (this.client is CountdownServiceClient)
			{
				CountdownServiceClient closing = this.client as CountdownServiceClient;

				try
				{
					closing.Close();
				}
				catch (Exception)
				{
				}
			}
		}

		#endregion
	}
}
