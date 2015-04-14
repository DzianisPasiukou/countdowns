namespace CountdownWpf.ServiceClient
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Principal;

	using Transfer.SmallTransfer;

	/// <summary>
	/// The instance for get access to countdowns.
	/// </summary>
	public class CountdownsClient
	{
		#region Private Fields

		/// <summary>
		/// The countdowns
		/// </summary>
		private List<ReminderPartDto> countdowns;

		/// <summary>
		/// The repository of service for get access data from WCF service.
		/// </summary>
		private IRepository repo;

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="CountdownsClient"/> class.
		/// </summary>
		/// <param name="repo">The repository.</param>
		/// <exception cref="System.ArgumentNullException">IRepository is null.</exception>
		public CountdownsClient(IRepository repo)
		{
			if (repo == null)
			{
				throw new ArgumentNullException("repo", "IRepository is null.");
			}

			this.repo = repo;
		}

		#region Public Properties

		/// <summary>
		/// Gets the instance of countdowns.
		/// </summary>
		/// <value>
		/// The countdowns.
		/// </value>
		public ICollection<ReminderPartDto> Values
		{
			get
			{
				if (this.countdowns == null)
				{
					this.Init();
				}

				return this.countdowns;
			}
		}

		/// <summary>
		/// Gets the service.
		/// </summary>
		/// <value>
		/// The service.
		/// </value>
		public IRepository Service
		{
			get
			{
				return this.repo;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Checks the repeats.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		/// <returns>Is repeated.</returns>
		public static bool CheckRepeats(ReminderPartDto countdown)
		{
			return countdown.CountdownsSettings.Days.Count > 0 ||
				countdown.CountdownsSettings.Weeks.Count > 0 ||
				countdown.CountdownsSettings.Months.Count > 0;
		}

		/// <summary>
		/// Refreshes the reminder.
		/// </summary>
		/// <param name="reminder">The reminder.</param>
		public void RefreshReminder(ReminderPartDto reminder)
		{
			switch (reminder.State)
			{
				case State.Created:
					this.countdowns.Add(reminder);
					break;
				case State.Deleted:
					this.countdowns.RemoveAll(r => r.Id == reminder.Id);
					break;
				default:
					int index = 0;
					try
					{
						index = this.countdowns.FindIndex(r => r.Id == reminder.Id);
					}
					catch (ArgumentNullException)
					{
						this.countdowns.Add(reminder);
					}

					if (index == -1)
					{
						this.countdowns.Add(reminder);
					}
					else
					{
						this.countdowns[index] = reminder;
					}

					break;
			}
		}

		/// <summary>
		/// Closes this instance.
		/// </summary>
		public void Close()
		{
			this.repo.Dispose();
		}

		/// <summary>
		/// Updates the reminder.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		public void UpdateReminder(ReminderPartDto countdown)
		{
			this.repo.UpdateData(WindowsIdentity.GetCurrent().Name, countdown.Id, State.Updated);
		}
		
		#endregion

		#region Private Methods

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		private void Init()
		{
			this.countdowns = this.repo.GetData().ToList();
		}

		#endregion
	}
}
