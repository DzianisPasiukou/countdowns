namespace CountdownWpf.ServiceClient
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Security.Principal;
	using System.ServiceModel;
	using System.Threading.Tasks;

	using CountdownWpf.CountdownsServiceReference;

	using LoggingManager;

	using Transfer.SmallTransfer;

	/// <summary>
	/// The instance for service repository which manipulate service functions.
	/// </summary>
	public class ServiceRepository : IRepository
	{
		#region Private Fields

		/// <summary>
		/// The service client.
		/// </summary>
		private ICountdownService service;

		/// <summary>
		/// The logger.
		/// </summary>
		private ILogger<IRepository> logger;

		/// <summary>
		/// The is connected boolean value.
		/// </summary>
		private bool isConnected = false;

		/// <summary>
		/// The user name.
		/// </summary>
		private string userName;

		#endregion

		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceRepository" /> class.
		/// </summary>
		/// <param name="logger">The logger repository.</param>
		/// <param name="service">The service.</param>
		/// <exception cref="System.ArgumentNullException">ICountdownServiceCallback is null.</exception>
		public ServiceRepository(ILogger<IRepository> logger, ICountdownService service)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger", "ILogger of ServiceRepository is null.");
			}

			this.logger = logger;

			if (service == null)
			{
				throw new ArgumentNullException("service", "ICountdownService is null.");
			}

			this.service = service;
		}

		#endregion

		#region Destructor

		/// <summary>
		/// Finalizes an instance of the <see cref="ServiceRepository"/> class.
		/// </summary>
		~ServiceRepository()
		{
			this.Dispose(false);
		}

		#endregion

		#region Public Events

		/// <summary>
		/// Occurs when some reminder refreshed on server.
		/// </summary>
		public static event Action<ReminderPartDto> Refreshed;

		/// <summary>
		/// Occurs when call to WCF methods ended with error.
		/// </summary>
		public static event Action<string> Error;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		#endregion

		#region Public Static Methods

		/// <summary>
		/// Called when server is refreshed.
		/// </summary>
		/// <param name="reminder">The reminder.</param>
		public static void OnRefreshed(ReminderPartDto reminder)
		{
			if (Refreshed != null)
			{
				Refreshed(reminder);
			}
		}

		/// <summary>
		/// Called when call from WCF service ended with error.
		/// </summary>
		/// <param name="text">The text.</param>
		public static void OnError(string text)
		{
			if (Error != null)
			{
				Error(text);
			}
		}

		#endregion

		#region Public Non-static Methods

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <returns>
		/// The collection of reminder part data transfer objects.
		/// </returns>
		public ICollection<ReminderPartDto> GetData()
		{
			if (!this.isConnected)
			{
				return new List<ReminderPartDto>();
			}

			List<ReminderPartDto> countdowns;
			try
			{

				countdowns = this.service.GetData(this.userName).ToList();
			}
			catch (CommunicationException e)
			{
				countdowns = new List<ReminderPartDto>();
				this.logger.WriteException("Failed get data from wcf.", e);

				OnError("Sory, getting data ended with error.");
			}

			return countdowns;
		}

		/// <summary>
		/// Updates the data.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <param name="id">The identifier of reminder.</param>
		/// <param name="state">The state of the reminder.</param>
		public void UpdateData(string userName, int id, State state)
		{
			try
			{
				this.service.UpdateData(userName, id, state);

				this.logger.Write(
					string.Format(CultureInfo.InvariantCulture, "User is updated reminder with id {0}. User name - {1}.", id, this.userName),
					TypeMessage.Info);
			}
			catch (CommunicationException e)
			{
				this.logger.WriteException("Failed to update data.", e);

				OnError("Sory, updating data ended with error.");
			}
		}

		/// <summary>
		/// Disconnects this instance.
		/// </summary>
		public void Disconnect()
		{
			try
			{
				this.service.OnDisconnectUser(this.userName);

				this.logger.Write(
							string.Format(CultureInfo.InvariantCulture, "User is disconnected: {0}", this.userName),
							TypeMessage.Info);

				this.userName = string.Empty;
				this.isConnected = false;
			}
			catch (CommunicationException e)
			{
				this.logger.WriteException(
					string.Format(CultureInfo.InvariantCulture, "Failed with disconnect user {0}", this.userName), e);

				OnError("Sory, disconnecting from server ended with error.");
			}
		}

		/// <summary>
		/// Connects this user to WCF server.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <param name="password">The password.</param>
		/// <returns>Is success to connect.</returns>
		public Task<bool> Connect(string userName, string password)
		{
			Task<bool> task = Task.Factory.StartNew(() =>
			{
				try
				{
					this.isConnected = this.service.OnConnectUser(userName, password);

					if (this.isConnected)
					{
						this.userName = userName;

						this.logger.Write(
						string.Format(CultureInfo.InvariantCulture, "User is connected: {0}", this.userName),
						TypeMessage.Info);
					}
				}
				catch (CommunicationException e)
				{
					this.isConnected = false;
					this.logger.WriteException(
						string.Format(CultureInfo.InvariantCulture, "Failed with connected user {0}", this.userName), e);

					OnError("Sory, connecting to server ended with error.");
				}

				return this.isConnected;
			});

			return task;
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing">Dispose state.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ReleaseManagedResources();
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Releases the managed resources.
		/// </summary>
		private void ReleaseManagedResources()
		{
			if (this.service != null)
			{
				try
				{
					this.Disconnect();

					if (this.service is CountdownsClient)
					{
						CountdownServiceClient client = this.service as CountdownServiceClient;

						client.Close();
					}
				}
				catch (CommunicationException e)
				{
					this.logger.WriteException(
						string.Format(CultureInfo.InvariantCulture, "Failed with disconnect user {0}", this.userName), e);

					OnError("Sory, disconnecting from server ended with error.");
				}
			}
		}

		#endregion
	}
}
