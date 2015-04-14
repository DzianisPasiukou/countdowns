namespace CountdownWpf.ServiceClient
{
	using System;
	using System.Globalization;
	using System.ServiceModel;
	using System.Threading.Tasks;

	using CountdownWpf.CountdownsServiceReference;

	using LoggingManager;

	using Microsoft.Practices.Unity;

	using NLog;

	using Transfer.SmallTransfer;

	/// <summary>
	/// The instance for callback by WCF service.
	/// </summary>
	[CallbackBehavior(
	ConcurrencyMode = ConcurrencyMode.Multiple,
	UseSynchronizationContext = true)]
	public class Callback : ICountdownServiceCallback
	{
		#region Private Fields

		/// <summary>
		/// The logger.
		/// </summary>
		private ILogger<ICountdownServiceCallback> logger;

		#endregion

		#region Public Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="Callback"/> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <exception cref="System.ArgumentNullException">ILogger is null.</exception>
		public Callback(ILogger<ICountdownServiceCallback> logger)
		{
			if (logger == null)
			{
				throw new ArgumentNullException("logger", "ILogger is null.");
			}

			this.logger = logger;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Notifies the about refresh.
		/// </summary>
		public void NotifyAboutRefresh()
		{
			this.logger.Write(
				string.Format(CultureInfo.InvariantCulture, "Callback for notify about refresh is complete."),
				TypeMessage.Info);
		}

		/// <summary>
		/// Notifies the about refresh reminder.
		/// </summary>
		/// <param name="reminder">The reminder.</param>
		public void NotifyAboutRefreshReminder(ReminderPartDto reminder)
		{
			this.logger.Write(
				string.Format(
				CultureInfo.InvariantCulture, 
				"Callback for notify about refresh reminder is complete. Reminder: {0}",
				MySerialization.SerializeToJSON(reminder)),
				TypeMessage.Info);
			ServiceRepository.OnRefreshed(reminder);
		}

		#endregion
	}
}
