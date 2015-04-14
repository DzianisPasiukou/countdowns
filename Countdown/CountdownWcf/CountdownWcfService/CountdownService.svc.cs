namespace CountdownWcfService
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.ServiceModel;
	using System.Threading.Tasks;

	using CountdownBusinessLogic;

	using CountdownMvc.Models.UserIdentity;
	using LoggingManager;

	using Microsoft.Practices.Unity;

	using Transfer.SmallTransfer;

	/// <summary>
	/// The countdown WCF service.
	/// </summary>
	[ServiceBehavior(
	   ConcurrencyMode = ConcurrencyMode.Multiple,
	   InstanceContextMode = InstanceContextMode.PerCall)]
	public class CountdownService : ICountdownService
	{
		#region Private Fields

		/// <summary>
		/// The users.
		/// </summary>
		private static List<WcfUsers> users = new List<WcfUsers>();

		/// <summary>
		/// The small countdown factory.
		/// </summary>
		private ICountdownsCollectionPart countdowns;

		/// <summary>
		/// The logger by nLog.
		/// </summary>
		private ILogger<CountdownService> logger;

		/// <summary>
		/// The user manager.
		/// </summary>
		private CustomUserManager userManager;

		#endregion

		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CountdownService"/> class.
		/// </summary>
		/// <param name="countdowns">The countdowns.</param>
		/// <param name="logger">The logger.</param>
		/// <exception cref="System.ArgumentNullException">
		/// ICountdownsCollectionPart is null.
		/// or
		/// ILogger is null.
		/// </exception>
		[InjectionConstructor]
		public CountdownService(ICountdownsCollectionPart countdowns, ILogger<CountdownService> logger)
		{
			if (countdowns == null)
			{
				throw new ArgumentNullException("countdowns", "ICountdownsCollectionPart is null.");
			}

			this.countdowns = countdowns;

			if (logger == null)
			{
				throw new ArgumentNullException("logger", "ILogger is null.");
			}

			this.logger = logger;

			////TODO: implement for account name.
			this.logger.UserName = ServiceSecurityContext.Current.WindowsIdentity.Name;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the user manager.
		/// </summary>
		/// <value>
		/// The user manager.
		/// </value>
		[Dependency]
		public CustomUserManager UserManager
		{
			get
			{
				return this.userManager;
			}

			set
			{
				this.userManager = value;
			}
		}

		#endregion

		#region public Methods

		/// <summary>
		/// Gets the data.
		/// </summary>
		/// <param name="userName">The user name.</param>
		/// <returns>The collection of reminder part data transfer objects.</returns>
		public IEnumerable<ReminderPartDto> GetData(string userName)
		{
			this.countdowns.UserName = userName;

			IEnumerable<ReminderPartDto> reminders = this.countdowns.Countdowns;

			return reminders;
		}

		/// <summary>
		/// Updates the data.
		/// </summary>
		/// <param name="userName">The user name.</param>
		/// <param name="id">The identifier.</param>
		/// <param name="state">The state.</param>
		public void UpdateData(string userName, int id, State state)
		{
			WcfUsers user = users.Find(u => u.UserName == userName);
			this.logger.Write(
				string.Format(CultureInfo.InvariantCulture, "User is updated data: {0}", MySerialization.SerializeToJSON(user)),
				TypeMessage.Info);

			if (user != null)
			{
				this.logger.Write(
					string.Format(CultureInfo.InvariantCulture, "Updated data by user: {0}", MySerialization.SerializeToJSON(user)),
					TypeMessage.Info);

				ReminderPartDto reminder;

				if (state != State.Deleted)
				{
					reminder = this.countdowns.GetCountdownById(id);
					reminder.State = state;
				}
				else
				{
					reminder = new ReminderPartDto()
				   {
					   Id = id,
					   State = state
				   };
				}

				try
				{
					user.Callback.NotifyAboutRefreshReminder(reminder);
				}
				catch (CommunicationException e)
				{
					this.logger.WriteException(string.Format(CultureInfo.InvariantCulture, "User {0} is droped.", userName), e);

					WcfUsers removeUser = users.Find(us => us.UserName == userName);

					if (removeUser != null)
					{
						users.Remove(removeUser);
					}
				}
			}
		}

		/// <summary>
		/// Called when user is connected.
		/// </summary>
		/// <param name="userName">The user name.</param>
		/// <param name="password">The password of user.</param>
		/// <returns>Is success to connect.</returns>
		public bool OnConnectUser(string userName, string password)
		{
			ApplicationUser user;

			try
			{
				user = this.UserManager.FindAsync(userName, password).Result;
			}
			catch (Exception)
			{
				return false;
			}

			if (user != null)
			{
				ICountdownCallback userCallback =
	 OperationContext.Current.GetCallbackChannel<ICountdownCallback>();

				if (users.Find(us => us.UserName == userName) == null)
				{
					users.Add(new WcfUsers()
					{
						UserName = userName,
						Callback = userCallback
					});
				}

				this.logger.Write(
					string.Format(
					CultureInfo.InvariantCulture,
					"User is connected: {0}",
					MySerialization.SerializeToJSON(users.Find(u => u.UserName == userName))),
					TypeMessage.Info);
			}

			return user != null;
		}

		/// <summary>
		/// Called when user is disconnected.
		/// </summary>
		/// <param name="userName">The user name.</param>
		public void OnDisconnectUser(string userName)
		{
			WcfUsers removeUser = users.Find(us => us.UserName == userName);

			if (removeUser != null)
			{
				users.Remove(removeUser);
			}

			this.logger.Write(
				string.Format(CultureInfo.InvariantCulture, "User is disconnected: {0}", MySerialization.SerializeToJSON(removeUser)),
				TypeMessage.Info);
		}

		#endregion
	}
}
