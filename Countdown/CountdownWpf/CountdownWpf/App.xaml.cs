namespace CountdownWpf
{
	using System.Windows;

	using CountdownWpf.Infrastructure;

	using Microsoft.Practices.Unity;

	/// <summary>
	/// Interaction logic for App.
	/// </summary>
	public partial class App : Application
	{
		/// <summary>
		/// Handles the Startup event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="StartupEventArgs"/> instance containing the event data.</param>
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			IUnityContainer container = UnityConfig.GetConfiguredContainer;

			container.Resolve<MainWindow>();
		}
	}
}
