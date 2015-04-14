namespace CountdownWcfService
{
	using CountdownBusinessLogic;
	using CountdownBusinessLogic.Security;
	using CountdownDataBaseLayer;
	using CountdownDataBaseLayer.Repo;
	using CountdownMvc.Models.UserIdentity;
	using LoggingManager;
	using Microsoft.Practices.Unity;
	using Unity.Wcf;

	/// <summary>
	/// The instance of WCF service factory.
	/// </summary>
	public class WcfServiceFactory : UnityServiceHostFactory
	{
		/// <summary>
		/// Configures the container.
		/// </summary>
		/// <param name="container">The container.</param>
		protected override void ConfigureContainer(IUnityContainer container)
		{
			container.RegisterType<ICountdownService, CountdownService>()
			.RegisterType<ICountdownsCollectionPart, CountdownCollectionPart>()
			.RegisterType<ILogger<CountdownService>, MyLogger<CountdownService>>()
			.RegisterType<IRepo<Reminder>, ReminderRepo>()
			.RegisterType<IHashCalculator, Sha256HashCalculator>()
			.RegisterType<CustomUserStore>()
			.RegisterType<CustomUserManager>()
			.RegisterType<IRepo<Users>, UsersRepo>();
		}
	}
}