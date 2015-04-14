namespace CountdownBusinessLogic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using CountdownBusinessLogic.Manager;

	using CountdownDataBaseLayer;
	using CountdownDataBaseLayer.Repo;

	using Transfer;
	using Transfer.SmallTransfer;

	/// <summary>
	/// The instance of small countdown factory which realize small functional from countdowns.
	/// </summary>
	public class CountdownCollectionPart : ICountdownsCollectionPart
	{
		#region Private Fields

		/// <summary>
		/// The reminder repository.
		/// </summary>
		private IRepo<Reminder> reminderRepo;

		/// <summary>
		/// The countdowns of the small reminder data transfer object.
		/// </summary>
		private IEnumerable<ReminderPartDto> countdowns;

		/// <summary>
		/// The user name.
		/// </summary>
		private string userName;

		#endregion

		#region Public Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="CountdownCollectionPart"/> class.
		/// </summary>
		/// <param name="reminderRepo">The reminder repository.</param>
		/// <exception cref="System.ArgumentNullException">IRepo of reminder is null.</exception>
		public CountdownCollectionPart(IRepo<Reminder> reminderRepo)
		{
			if (reminderRepo == null)
			{
				throw new ArgumentNullException("reminderRepo", "IRepo of reminder is null.");
			}

			this.reminderRepo = reminderRepo;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the user login.
		/// </summary>
		/// <value>
		/// The user login.
		/// </value>
		public string UserName
		{
			get
			{
				return this.userName;
			}

			set
			{
				this.userName = value;
			}
		}

		/// <summary>
		/// Gets or sets the countdowns of the small reminder data transfer object.
		/// </summary>
		/// <value>
		/// The countdowns of the small reminder data transfer object.
		/// </value>
		public IEnumerable<ReminderPartDto> Countdowns
		{
			get
			{
				if (this.countdowns == null)
				{
					this.countdowns = this.InitCountdowns();
				}

				return this.countdowns;
			}

			set
			{
				this.countdowns = value;
			}
		}

		/// <summary>
		/// Gets the countdown by identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>
		/// The reminder part data transfer object.
		/// </returns>
		public ReminderPartDto GetCountdownById(int id)
		{
			Reminder reminder;

			reminder = this.reminderRepo.GetById(id);

			ReminderPartDto outRem = new ReminderPartDto()
			{
				Id = reminder.Id,
				Description = reminder.Description,
				Name = reminder.Name
			};

			InitSettings(reminder, outRem);

			return outRem;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Initializes the settings.
		/// </summary>
		/// <param name="reminder">The reminder.</param>
		/// <param name="outReminder">The out small reminder.</param>
		/// <exception cref="System.ArgumentException">
		/// exist progress and countdown settings!
		/// or
		/// not exist progress and countdown settings!
		/// </exception>
		private static void InitSettings(Reminder reminder, ReminderPartDto outReminder)
		{
			if ((reminder.ProgressSetting != null) && (reminder.CountdownSetting != null))
			{
				throw new ArgumentException("Progress and countdowns settings can not be set simultaneously.");
			}

			if (reminder.ProgressSetting != null)
			{
				outReminder.ProgressSettings = new ProgressSettingsDto()
				{
					Duration = reminder.ProgressSetting.Duration,
					Id = reminder.ProgressSetting.Id,
					Interval = reminder.ProgressSetting.Interval,
					Start = Initializer.StartProgress(reminder.ProgressSetting.Start, reminder.ProgressSetting.Interval, reminder.ProgressSetting.Duration),
				};

				outReminder.ProgressSettings.End = outReminder.ProgressSettings.Start.AddMinutes(
					outReminder.ProgressSettings.Interval + outReminder.ProgressSettings.Duration);
			}
			else if (reminder.CountdownSetting != null)
			{
				outReminder.CountdownsSettings = new CountdownSettingsDto()
				{
					Duration = reminder.CountdownSetting.Duration,
					Id = reminder.CountdownSetting.Id,
					Days = Initializer.InitDay(reminder.CountdownSetting.Days),
					Months = Initializer.InitMonths(reminder.CountdownSetting.Monthes),
					Weeks = Initializer.InitWeek(reminder.CountdownSetting.Weeks)
				};

				outReminder.CountdownsSettings.Start = Initializer.StartCountdown(
					reminder.CountdownSetting.Start,
					outReminder.CountdownsSettings.Days,
					outReminder.CountdownsSettings.Weeks,
					outReminder.CountdownsSettings.Months);

				outReminder.CountdownsSettings.End = outReminder.CountdownsSettings.Start.AddMinutes(outReminder.CountdownsSettings.Duration);
			}
			else
			{
				throw new ArgumentException("Progress and countdowns settings are null.");
			}
		}

		/// <summary>
		/// Initializes the countdowns.
		/// </summary>
		/// <returns>Collection of the small reminder data transfer objects.</returns>
		private List<ReminderPartDto> InitCountdowns()
		{
			IEnumerable<Reminder> reminders;

			reminders = this.reminderRepo.GetAll();
			List<ReminderPartDto> countdowns = new List<ReminderPartDto>();

			foreach (var reminder in reminders)
			{
				if (reminder.UserName == this.userName)
				{
					ReminderPartDto outRem = new ReminderPartDto()
						{
							Id = reminder.Id,
							Description = reminder.Description,
							Name = reminder.Name
						};

					InitSettings(reminder, outRem);
					countdowns.Add(outRem);
				}
			}

			return countdowns;
		}

		#endregion
	}
}
