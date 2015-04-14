namespace CountdownDataBaseLayer
{
	using CountdownDataBaseLayer.Repo;

	/// <summary>
	/// The repository factory interface.
	/// </summary>
	public interface IRepoFactory
	{
		#region Properties

		/// <summary>
		/// Gets or sets the get reminder repo.
		/// </summary>
		/// <value>
		/// The get reminder repo.
		/// </value>
		IRepo<Reminder> GetReminderRepo { get; set; }

		/// <summary>
		/// Gets or sets the get days repo.
		/// </summary>
		/// <value>
		/// The get days repo.
		/// </value>
		IRepo<Days> GetDaysRepo { get; set; }

		/// <summary>
		/// Gets or sets the get weeks repo.
		/// </summary>
		/// <value>
		/// The get weeks repo.
		/// </value>
		IRepo<Weeks> GetWeeksRepo { get; set; }

		/// <summary>
		/// Gets or sets the get month repo.
		/// </summary>
		/// <value>
		/// The get month repo.
		/// </value>
		IRepo<Monthes> GetMonthRepo { get; set; }

		/// <summary>
		/// Gets or sets the get reminder settings repo.
		/// </summary>
		/// <value>
		/// The get reminder settings repo.
		/// </value>
		IRepo<ReminderSettings> GetReminderSettingsRepo { get; set; }

		/// <summary>
		/// Gets or sets the get files repo.
		/// </summary>
		/// <value>
		/// The get files repo.
		/// </value>
		IRepo<Files> GetFilesRepo { get; set; }

		/// <summary>
		/// Gets or sets the get exercise repo.
		/// </summary>
		/// <value>
		/// The get exercise repo.
		/// </value>
		IRepo<Exercises> GetExerciseRepo { get; set; }

		/// <summary>
		/// Gets or sets the get images repo.
		/// </summary>
		/// <value>
		/// The get images repo.
		/// </value>
		IRepo<Images> GetImagesRepo { get; set; }

		/// <summary>
		/// Gets or sets the get progress settings repo.
		/// </summary>
		/// <value>
		/// The get progress settings repo.
		/// </value>
		IRepo<ProgressSettings> GetProgressSettingsRepo { get; set; }

		#endregion
	}
}
