namespace CountdownMvc.App_Start
{
	using System;
	using System.ServiceModel;

	using CountdownBusinessLogic;
	using CountdownBusinessLogic.CountdownServiceReference;
	using CountdownBusinessLogic.Security;
	using CountdownBusinessLogic.Service;
	using CountdownBusinessLogic.SettingsInfo;

	using CountdownDataBaseLayer;
	using CountdownDataBaseLayer.Repo;

	using CountdownMvc.Controllers;
	using CountdownMvc.Models.UserIdentity;

	using LoggingManager;

	using Microsoft.Practices.Unity;

	/// <summary>
	/// Specifies the Unity configuration for the main container.
	/// </summary>
	public static class UnityConfig
	{
		#region Unity Container

		/// <summary>
		/// The container
		/// </summary>
		private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
		{
			var container = new UnityContainer();
			RegisterTypes(container);
			return container;
		});

		/// <summary>
		/// Gets the get configured container.
		/// </summary>
		/// <value>
		/// The get configured container.
		/// </value>
		public static IUnityContainer GetConfiguredContainer
		{
			get
			{
				return container.Value;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>Registers the type mappings with the Unity container.</summary>
		/// <param name="container">The unity container to configure.</param>
		/// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
		/// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
		public static void RegisterTypes(IUnityContainer container)
		{
			container.RegisterType<IRepo<Reminder>, ReminderRepo>()
			.RegisterType<IRepo<Days>, DaysRepo>()
			.RegisterType<IRepo<Weeks>, WeeksRepo>()
			.RegisterType<IRepo<Monthes>, MonthsRepo>()
			.RegisterType<IRepo<ReminderSettings>, ReminderSettingsRepo>()
			.RegisterType<IRepo<Files>, FilesRepo>()
			.RegisterType<IRepo<Exercises>, ExercisesRepo>()
			.RegisterType<IRepo<Images>, ImageRepo>()
			.RegisterType<IRepo<ProgressSettings>, ProgressSettingsRepo>()
			.RegisterType<ICountdownServiceCallback, Callback>()
			.RegisterType<ICountdownService, CountdownServiceClient>(new InjectionConstructor(new InstanceContext(container.Resolve<ICountdownServiceCallback>())))
			.RegisterType<IClient, ServiceFromWeb>()
			.RegisterType<ICountdownCollection, CountdownCollection>()
			.RegisterType<ISettings, SettingsCollection>()
			.RegisterType<ILogger<SettingsApiController>, MyLogger<SettingsApiController>>()
			.RegisterType<ILogger<CountdownApiController>, MyLogger<CountdownApiController>>()
			.RegisterType<IRepo<Users>, UsersRepo>()
			.RegisterType<IHashCalculator, Sha256HashCalculator>()
			.RegisterType<CustomUserStore>()
			.RegisterType<CustomUserManager>()
			.RegisterType<IRepoFactory, RepositoryFactory>();
		}

		#endregion
	}
}
