namespace CountdownWcfService
{
	using System.ServiceModel;

	using Transfer.SmallTransfer;

	/// <summary>
	/// This is callback of countdown for notify user about changes.
	/// </summary>
	public interface ICountdownCallback
	{
		#region Methods

		/// <summary>
		/// Notifies the about refresh.
		/// </summary>
		[OperationContract(IsOneWay = true)]
		void NotifyAboutRefresh();

		/// <summary>
		/// Notifies the about refresh reminder.
		/// </summary>
		/// <param name="reminder">The reminder.</param>
		[OperationContract(IsOneWay = true)]
		void NotifyAboutRefreshReminder(ReminderPartDto reminder);

		#endregion
	}
}
