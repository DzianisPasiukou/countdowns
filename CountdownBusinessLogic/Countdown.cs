namespace CountdownBusinessLogic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using CountdownBusinessLogic.Service;
	using CountdownDataBaseLayer;
	using CountdownDataBaseLayer.Repo;

	using LoggingManager;

	using NLog;

	using Transfer;

	/// <summary>
	/// The instance of countdown consist of Reminder Data Transfer Object.
	/// </summary>
	public class Countdown
	{
		#region Private Fields

		/// <summary>
		/// The reminder.
		/// </summary>
		private ReminderDto reminder;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the reminder.
		/// </summary>
		/// <value>
		/// The reminder.
		/// </value>
		public ReminderDto Reminder
		{
			get
			{
				return this.reminder;
			}

			set
			{
				this.reminder = value;
			}
		}

		#endregion
	}
}
