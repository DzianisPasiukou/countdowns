namespace CountdownBusinessLogic.Manager
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using CountdownDataBaseLayer;

	using Transfer;

	/// <summary>
	/// The static class for Initialize reminder and reminder properties.
	/// </summary>
	public static class Initializer
	{
		#region Public Methods

		#region Starting

		/// <summary>
		/// Starts the progress.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="interval">The interval.</param>
		/// <param name="duration">The duration.</param>
		/// <returns>
		/// New start of DateTime.
		/// </returns>
		public static DateTime StartProgress(DateTime start, int interval, int duration)
		{
			DateTime now = DateTime.Now;

			TimeSpan diff = now - start;

			if (diff.Ticks <= 0)
			{
				return start;
			}

			double totalMinutes = diff.TotalMinutes;

			double valueWithRest = totalMinutes / (interval + duration);
			double whole = (int)valueWithRest;
			double durationTime = (totalMinutes - ((interval + duration) * whole));

			if (durationTime > interval)
			{
				valueWithRest = (interval + duration) * ((int)valueWithRest + 1);
			}
			else
			{
				valueWithRest = (interval + duration) * (int)valueWithRest;
			}

			start = start.AddMinutes(valueWithRest);

			return start;
		}

		/// <summary>
		/// Starts the countdown.
		/// </summary>
		/// <param name="start">The start.</param>
		/// <param name="days">The days.</param>
		/// <param name="weeks">The weeks.</param>
		/// <param name="months">The months.</param>
		/// <returns>
		/// New start of DateTime.
		/// </returns>
		public static DateTime StartCountdown(DateTime start, ICollection<DaysDto> days, ICollection<WeeksDto> weeks, ICollection<MonthsDto> months)
		{
			TimeSpan diff = DateTime.Now - start;

			if (diff.Ticks > 0)
			{
				int nextDay = 100;

				int currentDay = 0;

				GetDayOfWeek(DateTime.Now, ref currentDay);

				if ((days.Count > 0) && (days.Max(d => d.Number) <= currentDay))
				{
					nextDay = days.Min(d => d.Number);
				}

				for (int i = 0; i < days.Count; i++)
				{
					if ((currentDay < days.ElementAt(i).Number) && (days.ElementAt(i).Number < nextDay))
					{
						nextDay = days.ElementAt(i).Number;
					}

					if (days.ElementAt(i).Number == 0)
					{
						if ((int)diff.TotalDays < diff.TotalDays)
						{
							start = start.AddDays((int)diff.TotalDays + 1);
							nextDay = currentDay + 2;
						}
						else
						{
							start = start.AddDays((int)diff.TotalDays);
							nextDay = currentDay + 1;
						}

						return start;
					}

					if (i == days.Count - 1)
					{
						if (nextDay > currentDay)
						{
							start = start.AddDays((int)diff.TotalDays).AddDays(nextDay - currentDay);
						}
						else
						{
							start = start.AddDays((int)diff.TotalDays).AddDays(7 - currentDay + nextDay);
						}

						return start;
					}
				}

				if (weeks.FirstOrDefault(w => w.Number == 0) != null)
				{
					start = start.AddDays((((int)(diff.TotalDays / 7)) * 7));

					if ((DateTime.Now - start).Ticks > 0)
					{
						start = start.AddDays(7);
					}
				}
				else if (months.FirstOrDefault(m => m.Number == 0) != null)
				{
					start = start.AddDays((int)diff.TotalDays + start.Day - DateTime.Now.Day);

					if ((start - DateTime.Now).Ticks <= 0)
					{
						start = start.AddMonths(1);
					}
				}
			}

			return start;
		}
		#endregion

		/// <summary>
		/// Initializes the reminder.
		/// </summary>
		/// <param name="reminder">The reminder.</param>
		/// <returns>Reminder data transfer object</returns>
		public static ReminderDto InitReminder(Reminder reminder)
		{
			ReminderDto reminderDto = new ReminderDto
								{
									Id = reminder.Id,
									Name = reminder.Name,
									Description = reminder.Description,
									UserName = reminder.UserName,
									TypeOfReminder = new TypeOfReminderDto()
									{
										Description = reminder.TypeOfReminder.Description,
										Name = reminder.TypeOfReminder.Name,
										Id = reminder.TypeOfReminder.Id
									},
									Exercises = InitExercise(reminder.Exercises),
									ReminderSettings = InitReminderSetting(reminder.ReminderSettings),
									CountdownSettings = InitCountdownSettings(reminder.CountdownSetting),
									ProgressSettings = InitProgressSettings(reminder.ProgressSetting)
								};
			return reminderDto;
		}

		/// <summary>
		/// Initializes the countdown settings.
		/// </summary>
		/// <param name="countdownSettings">The countdown settings.</param>
		/// <returns>The countdowns setting data transfer object.</returns>
		public static CountdownSettingsDto InitCountdownSettings(CountdownSettings countdownSettings)
		{
			if (countdownSettings != null)
			{
				CountdownSettingsDto setting = new CountdownSettingsDto()
				{
					Days = InitDay(countdownSettings.Days),
					Duration = countdownSettings.Duration,
					Months = InitMonths(countdownSettings.Monthes),
					Start = countdownSettings.Start,
					Weeks = InitWeek(countdownSettings.Weeks),
					Id = countdownSettings.Id
				};

				setting.Start = StartCountdown(setting.Start, setting.Days, setting.Weeks, setting.Months).ToUniversalTime();
				setting.End = setting.Start.AddMinutes(setting.Duration).ToUniversalTime();

				return setting;
			}

			return null;
		}

		/// <summary>
		/// Initializes the progress settings.
		/// </summary>
		/// <param name="countdownSettings">The countdown settings.</param>
		/// <returns>Progress setting data transfer object.</returns>
		public static ProgressSettingsDto InitProgressSettings(ProgressSettings countdownSettings)
		{
			if (countdownSettings != null)
			{
				ProgressSettingsDto setting = new ProgressSettingsDto()
				{
					Duration = countdownSettings.Duration,
					Start = countdownSettings.Start,
					Id = countdownSettings.Id,
					Interval = countdownSettings.Interval,
				};

				setting.Start = StartProgress(setting.Start, setting.Interval, setting.Duration).ToUniversalTime();
				setting.End = setting.Start.AddMinutes(setting.Interval + setting.Duration).ToUniversalTime();

				return setting;
			}

			return null;
		}

		/// <summary>
		/// Initializes the week.
		/// </summary>
		/// <param name="collection">The collection of weeks.</param>
		/// <returns>Weeks data transfer objects.</returns>
		public static ICollection<WeeksDto> InitWeek(ICollection<Weeks> collection)
		{
			ICollection<WeeksDto> weeks = new List<WeeksDto>();
			for (int i = 0; i < collection.Count; i++)
			{
				weeks.Add(new WeeksDto()
				{
					Number = collection.ElementAt(i).Number,
					Id = collection.ElementAt(i).Id
				});
			}

			return weeks;
		}

		/// <summary>
		/// Initializes the months.
		/// </summary>
		/// <param name="collection">The collection of month.</param>
		/// <returns>Months data transfer objects</returns>
		public static ICollection<MonthsDto> InitMonths(ICollection<Monthes> collection)
		{
			ICollection<MonthsDto> months = new List<MonthsDto>();
			for (int i = 0; i < collection.Count; i++)
			{
				months.Add(new MonthsDto()
				{
					Name = collection.ElementAt(i).Name,
					Number = collection.ElementAt(i).Number,
					Id = collection.ElementAt(i).Id
				});
			}

			return months;
		}

		/// <summary>
		/// Initializes the days.
		/// </summary>
		/// <param name="collection">The collection of days.</param>
		/// <returns>Days data transfer objects</returns>
		public static ICollection<DaysDto> InitDay(ICollection<Days> collection)
		{
			ICollection<DaysDto> days = new List<DaysDto>();
			for (int i = 0; i < collection.Count; i++)
			{
				days.Add(new DaysDto()
				{
					Name = collection.ElementAt(i).Name,
					Number = collection.ElementAt(i).Number.Value,
					Id = collection.ElementAt(i).Id
				});
			}

			return days;
		}

		/// <summary>
		/// Initializes the reminder setting.
		/// </summary>
		/// <param name="collection">The collection of reminder settings.</param>
		/// <returns>Reminder settings data transfer objects</returns>
		public static ICollection<ReminderSettingsDto> InitReminderSetting(ICollection<ReminderSettings> collection)
		{
			ICollection<ReminderSettingsDto> reminderSettings = new List<ReminderSettingsDto>();
			for (int i = 0; i < collection.Count; i++)
			{
				reminderSettings.Add(new ReminderSettingsDto()
					{
						Name = collection.ElementAt(i).Setting.Name,
						Description = collection.ElementAt(i).Setting.Description,
						Id = collection.ElementAt(i).Setting.Id,
						Value = collection.ElementAt(i).ValueSetting,
                        NameElement = collection.ElementAt(i).Setting.NameElement,
                        NameProperty = collection.ElementAt(i).Setting.NameProperty
					});
			}

			return reminderSettings;
		}

		/// <summary>
		/// Initializes the exercises.
		/// </summary>
		/// <param name="exercise">The collection of exercises.</param>
		/// <returns>Exercises data transfer objects</returns>
		public static ICollection<ExercisesDto> InitExercise(ICollection<Exercises> exercise)
		{
			ICollection<ExercisesDto> exercises = new List<ExercisesDto>();

			for (int i = 0; i < exercise.Count; i++)
			{
				exercises.Add(new ExercisesDto()
					{
						Name = exercise.ElementAt(i).Name,
						Description = exercise.ElementAt(i).Description,
						Files = InitFile(exercise.ElementAt(i).Files),
						Id = exercise.ElementAt(i).Id
					});
			}

			return exercises;
		}

		/// <summary>
		/// Initializes the file.
		/// </summary>
		/// <param name="files">The collection of files.</param>
		/// <returns>Files data transfer objects</returns>
		public static ICollection<FilesDto> InitFile(ICollection<Files> files)
		{
			ICollection<FilesDto> filesDto = new List<FilesDto>();

			for (int i = 0; i < files.Count; i++)
			{
				ICollection<ImageDto> images = new List<ImageDto>();
				for (int j = 0; j < files.ElementAt(i).Images.Count; j++)
				{
					images.Add(new ImageDto()
						{
							Data = Convert.ToBase64String(files.ElementAt(i).Images.ElementAt(j).Data),
							Id = files.ElementAt(i).Images.ElementAt(j).Id,
							Name = files.ElementAt(i).Images.ElementAt(j).Name,
							Type = files.ElementAt(i).Images.ElementAt(j).Type
						});
				}

				filesDto.Add(new FilesDto()
				{
					Name = files.ElementAt(i).Name,
					Description = files.ElementAt(i).Description,
					Images = images.ToArray(),
					Id = files.ElementAt(i).Id
				});
			}

			return filesDto;
		}

		/// <summary>
		/// Initializes the settings.
		/// </summary>
		/// <param name="settings">The settings.</param>
		/// <returns>The collection of settings transfer objects.</returns>
		public static IEnumerable<SettingsDto> InitSettings(IEnumerable<Settings> settings)
		{
			var transferSettings = new List<SettingsDto>();

			for (int i = 0; i < settings.Count(); i++)
			{
				transferSettings.Add(new SettingsDto()
				{
					Id = settings.ElementAt(i).Id,
					Name = settings.ElementAt(i).Name,
					Description = settings.ElementAt(i).Description,
					TypeOfReminderId = settings.ElementAt(i).TypeOfReminderId,
                    NameProperty = settings.ElementAt(i).NameProperty,
                    NameElement = settings.ElementAt(i).NameElement
				});
			}

			return transferSettings;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Checks the current day of the week.
		/// </summary>
		/// <param name="start">The start date.</param>
		/// <param name="currentDay">The current day.</param>
		private static void GetDayOfWeek(DateTime start, ref int currentDay)
		{
			if (start.DayOfWeek == 0)
			{
				currentDay = 7;
			}
			else
			{
				currentDay = (int)start.DayOfWeek;
			}
		}

		#endregion
	}
}
