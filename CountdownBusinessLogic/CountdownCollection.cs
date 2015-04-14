namespace CountdownBusinessLogic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;

	using CountdownBusinessLogic;
	using CountdownBusinessLogic.Manager;
	using CountdownBusinessLogic.Service;

	using CountdownDataBaseLayer;
	using CountdownDataBaseLayer.Repo;

	using Microsoft.AspNet.Identity;

	using Transfer.SmallTransfer;

	/// <summary>
	/// This is instance is countdowns factory existing for store of countdowns.
	/// </summary>
	public partial class CountdownCollection : ICountdownCollection
	{
		#region Private Fields

		/// <summary>
		/// The countdowns.
		/// </summary>
		private IEnumerable<Countdown> countdowns;

		/// <summary>
		/// The client from WCF proxy.
		/// </summary>
		private IClient client;

		/// <summary>
		/// The repository factory.
		/// </summary>
		private IRepoFactory factory;

		/// <summary>
		/// The user name.
		/// </summary>
		private string userName;

		#endregion

		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CountdownCollection"/> class.
		/// </summary>
		/// <param name="factory">The factory.</param>
		/// <param name="client">The client.</param>
		/// <exception cref="System.ArgumentNullException">
		/// factory;IRepoFactory is null.
		/// or
		/// client;IClient is null.
		/// </exception>
		public CountdownCollection(IRepoFactory factory, IClient client)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory", "IRepoFactory is null.");
			}

			this.factory = factory;

			if (client == null)
			{
				throw new ArgumentNullException("client", "IClient is null.");
			}

			this.client = client;

			this.CountdownChanged += (userName, id, state) => this.client.NotifyAboutChanges(userName, id, state);
		}

		#endregion

		#region Private Events

		/// <summary>
		/// Occurs when countdown is changed.
		/// </summary>
		private event Action<string, int, State> CountdownChanged;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the countdowns.
		/// </summary>
		/// <value>
		/// The countdowns.
		/// </value>
		public IEnumerable<Countdown> Countdowns
		{
			get
			{
				if (this.countdowns == null)
				{
					this.countdowns = this.Init();
					return this.countdowns;
				}
				else
				{
					return this.countdowns;
				}
			}

			set
			{
				this.countdowns = value;
			}
		}

		/// <summary>
		/// Gets or sets the name of the user.
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

			set
			{
				this.userName = value;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets the countdown by identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>
		/// Countdown object
		/// </returns>
		public Countdown GetCountdownById(int id)
		{
			Reminder reminder = this.factory.GetReminderRepo.GetById(id);

			Countdown countdown = new Countdown()
						{
							Reminder = Initializer.InitReminder(reminder)
						};

			return countdown;
		}

		/// <summary>
		/// Activates this reminder.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		/// <exception cref="System.ArgumentNullException">I tried to activate null reminder.
		/// or
		/// I tried to activate not progress reminder.</exception>
		public void Activate(Countdown countdown)
		{
			if (countdown.Reminder == null)
			{
				throw new ArgumentNullException("countdown", "I tried to activate null reminder.");
			}

			try
			{
				countdown.Reminder.ProgressSettings.Start = DateTime.Now.AddMinutes(countdown.Reminder.ProgressSettings.Duration);
			}
			catch (ArgumentNullException e)
			{
				throw new ArgumentNullException("I tried to activate not progress reminder.", e);
			}

			this.UpdateProgressSettings(countdown);
			this.OnCountdownChanged(countdown, State.Activate);
		}

		/// <summary>
		/// Postpones reminder by the specified time.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		/// <param name="time">The time of postpone.</param>
		/// <exception cref="System.ArgumentNullException">I tried to postpone null reminder.
		/// or
		/// I tried to postpone not progress reminder.</exception>
		public void Postpone(Countdown countdown, int time)
		{
			if (countdown.Reminder == null)
			{
				throw new ArgumentNullException("countdown", "I tried to postpone null reminder.");
			}

			try
			{
				countdown.Reminder.ProgressSettings.Start = countdown.Reminder.ProgressSettings.Start.ToLocalTime();

				if ((countdown.Reminder.ProgressSettings.Start - DateTime.Now).Ticks < 0)
				{
					countdown.Reminder.ProgressSettings.Start = countdown.Reminder.ProgressSettings.Start.AddMinutes(time);
				}
				else
				{
					countdown.Reminder.ProgressSettings.Start = countdown.Reminder.ProgressSettings.Start.AddMinutes(
						time - countdown.Reminder.ProgressSettings.Duration - countdown.Reminder.ProgressSettings.Interval);
				}
			}
			catch (ArgumentNullException e)
			{
				throw new ArgumentNullException("I tried to postpone not progress reminder.", e);
			}

			this.UpdateProgressSettings(countdown);
			this.OnCountdownChanged(countdown, State.Postpone);
		}

		/// <summary>
		/// Closes this countdown.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		/// <exception cref="System.ArgumentNullException">I tried to close null reminder.</exception>
		public void Close(Countdown countdown)
		{
			if (countdown.Reminder == null)
			{
				throw new ArgumentNullException("countdown", "I tried to close null reminder.");
			}

			Reminder reminder = this.factory.GetReminderRepo.GetById(countdown.Reminder.Id);

			this.factory.GetReminderRepo.Delete(reminder);
			this.factory.GetReminderRepo.Save();
			this.OnCountdownChanged(countdown, State.Deleted);
		}

		/// <summary>
		/// Creates this instance of countdown.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		/// <exception cref="System.ArgumentException">Countdown is not valid.</exception>
		public void Create(Countdown countdown)
		{
			if (Validator.ValidateCreateReminder(countdown.Reminder))
			{
				Reminder newReminder = new Reminder();
				newReminder.Name = countdown.Reminder.Name;
				newReminder.Description = countdown.Reminder.Description;
				newReminder.UserName = countdown.Reminder.UserName;

				if (countdown.Reminder.ProgressSettings != null)
				{
					newReminder.ProgressSetting = new ProgressSettings()
					{
						Duration = countdown.Reminder.ProgressSettings.Duration,
						Interval = countdown.Reminder.ProgressSettings.Interval,
						Start = countdown.Reminder.ProgressSettings.Start.ToLocalTime()
					};
				}
				else
				{
					newReminder.CountdownSetting = new CountdownSettings()
					{
						Duration = countdown.Reminder.CountdownSettings.Duration,
						Start = countdown.Reminder.CountdownSettings.Start.ToLocalTime()
					};

					List<Days> newDays = new List<Days>();
					for (int i = 0; i < countdown.Reminder.CountdownSettings.Days.Count; i++)
					{
						newDays.Add(new Days()
						{
							Id = countdown.Reminder.CountdownSettings.Days.ElementAt(i).Id,
							Name = countdown.Reminder.CountdownSettings.Days.ElementAt(i).Name,
							Number = countdown.Reminder.CountdownSettings.Days.ElementAt(i).Number,
							CountdownSetting = newReminder.CountdownSetting
						});
					}

					newReminder.CountdownSetting.Days = newDays;

					List<Monthes> newMonth = new List<Monthes>();

					for (int i = 0; i < countdown.Reminder.CountdownSettings.Months.Count; i++)
					{
						newMonth.Add(new Monthes()
						{
							Id = countdown.Reminder.CountdownSettings.Months.ElementAt(i).Id,
							Name = countdown.Reminder.CountdownSettings.Months.ElementAt(i).Name,
							Number = countdown.Reminder.CountdownSettings.Months.ElementAt(i).Number,
							CountdownSetting = newReminder.CountdownSetting
						});
					}

					newReminder.CountdownSetting.Monthes = newMonth;

					List<Weeks> newWeeks = new List<Weeks>();

					for (int i = 0; i < countdown.Reminder.CountdownSettings.Weeks.Count; i++)
					{
						newWeeks.Add(new Weeks()
						{
							CountdownSetting = newReminder.CountdownSetting,
							Id = countdown.Reminder.CountdownSettings.Weeks.ElementAt(i).Id,
							Number = countdown.Reminder.CountdownSettings.Weeks.ElementAt(i).Number
						});
					}

					newReminder.CountdownSetting.Weeks = newWeeks;
				}

				newReminder.TypeOfReminderId = countdown.Reminder.TypeOfReminder.Id;

				this.factory.GetReminderRepo.Add(newReminder);
				this.factory.GetReminderRepo.Save();

				newReminder = this.factory.GetReminderRepo.GetById(newReminder.Id);
				Refresh(countdown, newReminder);

				this.OnCountdownChanged(countdown, State.Created);
			}
			else
			{
				throw new ArgumentException("Countdown is not valid.");
			}
		}

		/// <summary>
		/// Full the update of the reminder.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		/// <exception cref="System.ArgumentNullException">I tried to update null reminder.</exception>
		/// <exception cref="System.ArgumentException">Countdown is not valid.</exception>
		public void FullUpdate(Countdown countdown)
		{
			if (countdown.Reminder == null)
			{
				throw new ArgumentNullException("countdown", "I tried to update null reminder.");
			}

			if (Validator.ValidateUpdateReminder(countdown.Reminder))
			{
				Reminder oldReminder = this.factory.GetReminderRepo.GetById(countdown.Reminder.Id);

				oldReminder.Name = countdown.Reminder.Name;
				oldReminder.Description = countdown.Reminder.Description;

				if (oldReminder.ProgressSetting != null)
				{
					oldReminder.ProgressSetting.Duration = countdown.Reminder.ProgressSettings.Duration;
					oldReminder.ProgressSetting.Interval = countdown.Reminder.ProgressSettings.Interval;
					oldReminder.ProgressSetting.Start = Initializer.StartProgress(
						countdown.Reminder.ProgressSettings.Start.ToLocalTime(),
						countdown.Reminder.ProgressSettings.Interval,
						countdown.Reminder.ProgressSettings.Duration);

					countdown.Reminder.ProgressSettings.Start = oldReminder.ProgressSetting.Start;
				}
				else
				{
					oldReminder.CountdownSetting.Duration = countdown.Reminder.CountdownSettings.Duration;
					oldReminder.CountdownSetting.Start = Initializer.StartCountdown(
						countdown.Reminder.CountdownSettings.Start.ToLocalTime(),
						countdown.Reminder.CountdownSettings.Days,
						countdown.Reminder.CountdownSettings.Weeks,
						countdown.Reminder.CountdownSettings.Months);

					countdown.Reminder.CountdownSettings.Start = oldReminder.CountdownSetting.Start;

					var existingDays = oldReminder.CountdownSetting.Days.ToList<Days>();

					List<Days> updatedDays = new List<Days>();

					foreach (var day in countdown.Reminder.CountdownSettings.Days)
					{
						updatedDays.Add(new Days()
						{
							CountdownSettingsId = oldReminder.CountdownSetting.Id,
							Id = day.Id,
							Name = day.Name,
							Number = day.Number
						});
					}

					this.factory.GetDaysRepo.UpdateMany(existingDays, updatedDays);

					var existingMonth = oldReminder.CountdownSetting.Monthes.ToList<Monthes>();

					List<Monthes> updatedMonth = new List<Monthes>();

					foreach (var month in countdown.Reminder.CountdownSettings.Months)
					{
						updatedMonth.Add(new Monthes()
						{
							CountdownSettingsId = oldReminder.CountdownSetting.Id,
							Id = month.Id,
							Name = month.Name,
							Number = month.Number
						});
					}

					this.factory.GetMonthRepo.UpdateMany(existingMonth, updatedMonth);

					var existingWeek = oldReminder.CountdownSetting.Weeks.ToList<Weeks>();

					List<Weeks> updatedWeeks = new List<Weeks>();

					foreach (var week in countdown.Reminder.CountdownSettings.Weeks)
					{
						updatedWeeks.Add(new Weeks()
						{
							CountdownSettingsId = oldReminder.CountdownSetting.Id,
							Id = week.Id,
							Number = week.Number
						});
					}

					this.factory.GetWeeksRepo.UpdateMany(existingWeek, updatedWeeks);
				}

				var existingSettings = oldReminder.ReminderSettings.ToList<ReminderSettings>();

				List<ReminderSettings> updatedSetting = new List<ReminderSettings>();

				foreach (var setting in countdown.Reminder.ReminderSettings)
				{
					updatedSetting.Add(new ReminderSettings()
					{
						ReminderId = oldReminder.Id,
						SettingsId = setting.Id,
						ValueSetting = setting.Value
					});
				}

				this.factory.GetReminderSettingsRepo.UpdateMany(existingSettings, updatedSetting);

				var existingExercises = oldReminder.Exercises.ToList<Exercises>();

				List<Exercises> updatedExercises = new List<Exercises>();

				foreach (var exercise in countdown.Reminder.Exercises)
				{
					updatedExercises.Add(new Exercises()
					{
						Description = exercise.Description,
						Id = exercise.Id,
						Name = exercise.Name,
						ReminderId = oldReminder.Id
					});
				}

				this.factory.GetExerciseRepo.UpdateMany(existingExercises, updatedExercises);

				int indexExercise = 0;

				foreach (var exercise in countdown.Reminder.Exercises)
				{
					List<Files> existingFiles = new List<Files>();

					if (exercise.Id != 0)
					{
						existingFiles = this.factory.GetExerciseRepo.GetById(exercise.Id).Files.ToList<Files>();
					}

					List<Files> updatedFiles = new List<Files>();

					if (exercise.Files != null)
					{
						foreach (var file in exercise.Files)
						{
							updatedFiles.Add(new Files()
							{
								Description = file.Description,
								ExercisesId = updatedExercises[indexExercise].Id,
								Id = file.Id,
								Name = file.Name,
							});
						}

						this.factory.GetFilesRepo.UpdateMany(existingFiles, updatedFiles);

						int indexFile = 0;

						foreach (var file in exercise.Files)
						{
							List<Images> existingImages = new List<Images>();

							if (file.Id != 0)
							{
								existingImages = this.factory.GetFilesRepo.GetById(file.Id).Images.ToList<Images>();
							}

							List<Images> updatedImages = new List<Images>();

							foreach (var image in file.Images)
							{
								updatedImages.Add(new Images()
								{
									Data = Convert.FromBase64String(image.Data),
									FilesId = updatedFiles[indexFile].Id,
									Id = image.Id,
									Name = image.Name,
									Type = image.Type
								});
							}

							this.factory.GetImagesRepo.UpdateMany(existingImages, updatedImages);

							indexFile += 1;
						}
					}

					indexExercise += 1;
				}

				this.factory.GetReminderRepo.Update(oldReminder);
				this.factory.GetReminderRepo.Save();

				countdown = this.GetCountdownById(countdown.Reminder.Id);

				this.OnCountdownChanged(countdown, State.Updated);
			}
			else
			{
				throw new ArgumentException("Countdown is not valid.");
			}
		}

		/// <summary>
		/// Updates the settings of countdown.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		/// <exception cref="System.ArgumentNullException">I tried to update progress settings null reminder.</exception>
		public void UpdateProgressSettings(Countdown countdown)
		{
			if (countdown.Reminder == null)
			{
				throw new ArgumentNullException("countdown", "I tried to update progress settings null reminder.");
			}

			ProgressSettings progressSetting = new ProgressSettings()
			{
				Duration = countdown.Reminder.ProgressSettings.Duration,
				Id = countdown.Reminder.ProgressSettings.Id,
				Interval = countdown.Reminder.ProgressSettings.Interval,
				Start = countdown.Reminder.ProgressSettings.Start
			};

			this.factory.GetProgressSettingsRepo.Update(progressSetting);
			this.factory.GetProgressSettingsRepo.Save();

			countdown.Reminder.ProgressSettings.Start = countdown.Reminder.ProgressSettings.Start.ToUniversalTime();
		}

		/// <summary>
		/// Called when countdown is changed.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		/// <param name="state">The state of the changes the reminder.</param>
		public void OnCountdownChanged(Countdown countdown, State state)
		{
			if (this.CountdownChanged != null)
			{
				this.CountdownChanged(countdown.Reminder.UserName, countdown.Reminder.Id, state);
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Refreshes the specified reminder.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		/// <param name="reminder">The reminder.</param>
		private static void Refresh(Countdown countdown, Reminder reminder)
		{
			countdown.Reminder = Initializer.InitReminder(reminder);
		}

		/// <summary>
		/// Initializes this instance of countdowns.
		/// </summary>
		/// <returns>
		/// Countdowns objects
		/// </returns>
		private IEnumerable<Countdown> Init()
		{
			IEnumerable<Reminder> reminders;

			reminders = this.factory.GetReminderRepo.GetAll();

			List<Countdown> countdown = new List<Countdown>();

			foreach (var reminder in reminders)
			{
				if (reminder.UserName == this.userName)
				{
					countdown.Add(new Countdown()
						{
							Reminder = Initializer.InitReminder(reminder)
						});
				}
			}

			return countdown;
		}

		#endregion
	}
}
