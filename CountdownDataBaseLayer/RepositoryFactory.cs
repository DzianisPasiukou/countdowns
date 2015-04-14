namespace CountdownDataBaseLayer
{
	using CountdownDataBaseLayer.Repo;

	using Microsoft.Practices.Unity;

	/// <summary>
	/// The instance for factory from repositories.
	/// </summary>
	public class RepositoryFactory : IRepoFactory
	{
		#region Repositories

		/// <summary>
		/// The reminder repository.
		/// </summary>
		private IRepo<Reminder> reminderRepo;

		/// <summary>
		/// The days repository.
		/// </summary>
		private IRepo<Days> daysRepo;

		/// <summary>
		/// The weeks repository.
		/// </summary>
		private IRepo<Weeks> weeksRepo;

		/// <summary>
		/// The months repository.
		/// </summary>
		private IRepo<Monthes> monthsRepo;

		/// <summary>
		/// The settings repository.
		/// </summary>
		private IRepo<ReminderSettings> settingsRepo;

		/// <summary>
		/// The files repository.
		/// </summary>
		private IRepo<Files> filesRepo;

		/// <summary>
		/// The exercise repository.
		/// </summary>
		private IRepo<Exercises> exerciseRepo;

		/// <summary>
		/// The images repository.
		/// </summary>
		private IRepo<Images> imagesRepo;

		/// <summary>
		/// The progress settings repository.
		/// </summary>
		private IRepo<ProgressSettings> progressSettingsRepo;

		#endregion

		/// <summary>
		/// Gets or sets the get reminder repo.
		/// </summary>
		/// <value>
		/// The get reminder repo.
		/// </value>
		[Dependency]
		public IRepo<Reminder> GetReminderRepo
		{
			get
			{
				return this.reminderRepo;
			}

			set
			{
				this.reminderRepo = value;
			}
		}

		/// <summary>
		/// Gets or sets the get days repo.
		/// </summary>
		/// <value>
		/// The get days repo.
		/// </value>
		[Dependency]
		public IRepo<Days> GetDaysRepo
		{
			get
			{
				return this.daysRepo;
			}

			set
			{
				this.daysRepo = value;
			}
		}

		/// <summary>
		/// Gets or sets the get weeks repo.
		/// </summary>
		/// <value>
		/// The get weeks repo.
		/// </value>
		[Dependency]
		public IRepo<Weeks> GetWeeksRepo
		{
			get
			{
				return this.weeksRepo;
			}

			set
			{
				this.weeksRepo = value;
			}
		}

		/// <summary>
		/// Gets or sets the get month repo.
		/// </summary>
		/// <value>
		/// The get month repo.
		/// </value>
		[Dependency]
		public IRepo<Monthes> GetMonthRepo
		{
			get
			{
				return this.monthsRepo;
			}

			set
			{
				this.monthsRepo = value;
			}
		}

		/// <summary>
		/// Gets or sets the get reminder settings repo.
		/// </summary>
		/// <value>
		/// The get reminder settings repo.
		/// </value>
		[Dependency]
		public IRepo<ReminderSettings> GetReminderSettingsRepo
		{
			get
			{
				return this.settingsRepo;
			}

			set
			{
				this.settingsRepo = value;
			}
		}

		/// <summary>
		/// Gets or sets the get files repo.
		/// </summary>
		/// <value>
		/// The get files repo.
		/// </value>
		[Dependency]
		public IRepo<Files> GetFilesRepo
		{
			get
			{
				return this.filesRepo;
			}

			set
			{
				this.filesRepo = value;
			}
		}

		/// <summary>
		/// Gets or sets the get exercise repo.
		/// </summary>
		/// <value>
		/// The get exercise repo.
		/// </value>
		[Dependency]
		public IRepo<Exercises> GetExerciseRepo
		{
			get
			{
				return this.exerciseRepo;
			}

			set
			{
				this.exerciseRepo = value;
			}
		}

		/// <summary>
		/// Gets or sets the get images repo.
		/// </summary>
		/// <value>
		/// The get images repo.
		/// </value>
		[Dependency]
		public IRepo<Images> GetImagesRepo
		{
			get
			{
				return this.imagesRepo;
			}

			set
			{
				this.imagesRepo = value;
			}
		}

		/// <summary>
		/// Gets or sets the get progress settings repo.
		/// </summary>
		/// <value>
		/// The get progress settings repo.
		/// </value>
		[Dependency]
		public IRepo<ProgressSettings> GetProgressSettingsRepo
		{
			get
			{
				return this.progressSettingsRepo;
			}

			set
			{
				this.progressSettingsRepo = value;
			}
		}
	}
}
