namespace CountdownBusinessLogic.Manager
{
	using System;
	using System.Linq;

	using Transfer;

	/// <summary>
	/// The static class for validate date on server.
	/// </summary>
	public static class Validator
	{
		#region Public Methods

		/// <summary>
		/// Validates the creation of reminder.
		/// </summary>
		/// <param name="reminder">The reminder.</param>
		/// <returns>Returns the validity of reminder.</returns>
		public static bool ValidateCreateReminder(ReminderDto reminder)
		{
			if ((reminder == null)
				|| (string.IsNullOrEmpty(reminder.Name))
				|| (reminder.TypeOfReminder.Id < 1)
				|| ((reminder.ProgressSettings == null) && (reminder.CountdownSettings == null))
				|| ((reminder.ProgressSettings != null) && (reminder.CountdownSettings != null))
				|| ((reminder.ProgressSettings != null) && (reminder.ProgressSettings.Start == null)))
			{
				return false;
			}

			if ((reminder.CountdownSettings != null))
			{
				if (reminder.CountdownSettings.Start == null)
				{
					return false;
				}

				foreach (var day in reminder.CountdownSettings.Days)
				{
					if (string.IsNullOrEmpty(day.Name))
					{
						return false;
					}
				}

				foreach (var month in reminder.CountdownSettings.Months)
				{
					if (string.IsNullOrEmpty(month.Name))
					{
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Validates the update reminder.
		/// </summary>
		/// <param name="reminder">The reminder.</param>
		/// <returns>Returns the validity of reminder.</returns>
		public static bool ValidateUpdateReminder(ReminderDto reminder)
		{
			foreach (var exercise in reminder.Exercises)
			{
				if (string.IsNullOrEmpty(exercise.Name))
				{
					return false;
				}

				if (exercise.Files != null)
				{
					foreach (var file in exercise.Files)
					{
						if (string.IsNullOrEmpty(file.Name))
						{
							return false;
						}
					}
				}
			}

			return ValidateCreateReminder(reminder);
		}

		#endregion
	}
}
