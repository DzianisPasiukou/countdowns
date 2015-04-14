namespace CountdownBusinessLogic.Service
{
	using System;
	using System.ServiceModel;

	using CountdownBusinessLogic.CountdownServiceReference;

	using Transfer.SmallTransfer;

	/// <summary>
	/// The instance for callback function by WCF.
	/// </summary>
	[CallbackBehavior(
	ConcurrencyMode = ConcurrencyMode.Multiple,
	UseSynchronizationContext = false)]
	public class Callback : ICountdownServiceCallback
	{
		#region Public Methods

		/// <summary>
		/// Notifies the about refresh.
		/// </summary>
		/// <exception cref="System.NotImplementedException">Not implemented refresh</exception>
		public void NotifyAboutRefresh()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Begins the notify about refresh.
		/// </summary>
		/// <param name="callback">The callback.</param>
		/// <param name="asyncState">State of the asynchronous.</param>
		/// <returns>Async result.</returns>
		/// <exception cref="System.NotImplementedException">Not implemented.</exception>
		public IAsyncResult BeginNotifyAboutRefresh(AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Ends the notify about refresh.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <exception cref="System.NotImplementedException">Not implemented.</exception>
		public void EndNotifyAboutRefresh(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Notifies the about refresh reminder.
		/// </summary>
		/// <param name="reminder">The reminder.</param>
		/// <exception cref="System.NotImplementedException">Not implemented.</exception>
		public void NotifyAboutRefreshReminder(ReminderPartDto reminder)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Begins the notify about refresh reminder.
		/// </summary>
		/// <param name="reminder">The reminder.</param>
		/// <param name="callback">The callback.</param>
		/// <param name="asyncState">State of the asynchronous.</param>
		/// <returns>The async result.</returns>
		/// <exception cref="System.NotImplementedException">Not implemented.</exception>
		public IAsyncResult BeginNotifyAboutRefreshReminder(Transfer.SmallTransfer.ReminderPartDto reminder, AsyncCallback callback, object asyncState)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Ends the notify about refresh reminder.
		/// </summary>
		/// <param name="result">The result.</param>
		/// <exception cref="System.NotImplementedException">Not implemented.</exception>
		public void EndNotifyAboutRefreshReminder(IAsyncResult result)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
