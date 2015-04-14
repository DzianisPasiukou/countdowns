namespace CountdownWpf
{
	using System;
	using System.Globalization;
	using System.Linq;
	using System.Windows;
	using System.Windows.Navigation;
	using System.Windows.Threading;

	using CountdownWpf.Icon;
	using CountdownWpf.ServiceClient;

	using Transfer.SmallTransfer;

	/// <summary>
	/// Interaction logic for MainWindow.
	/// </summary>
	/// DoubleAnimation anim;
	public sealed partial class MainWindow : Window, IVisible
	{
		#region Private Fields

		/// <summary>
		/// The notify icon instance.
		/// </summary>
		private static NICon icon;

		/// <summary>
		/// The dispatcher timer.
		/// </summary>
		private DispatcherTimer dispatcherTimer;

		/// <summary>
		/// The countdowns.
		/// </summary>
		private CountdownsClient countdowns;

		#endregion

		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow" /> class.
		/// </summary>
		/// <param name="repo">The repository.</param>
		public MainWindow(IRepository repo)
		{
			this.InitializeComponent();

			icon = new NICon(this);
			icon.Closing += this.Exit_Click;

			ServiceRepository.Error += icon.Show;

			this.countdowns = new CountdownsClient(repo);
			ServiceRepository.Refreshed += this.OnRefreshed;
			ServiceRepository.Refreshed += this.countdowns.RefreshReminder;

			this.Show();

			this.DataContext = new ViewModel();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Called when reminder is refreshed.
		/// </summary>
		/// <param name="obj">The object.</param>
		private void OnRefreshed(ReminderPartDto obj)
		{
			if (obj.State == State.Deleted)
			{
				try
				{
					obj = this.countdowns.Values.First(r => r.Id == obj.Id);
				}
				catch (InvalidOperationException)
				{
					return;
				}

				obj.State = State.Deleted;
			}

			icon.Show(
				string.Format(CultureInfo.InvariantCulture, "Refresh: {0}\n Type of refresh {1}", obj.Name, obj.State.ToString()));
		}

		/// <summary>
		/// Handles the Click event of the Exit control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void Exit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Handles the Closed event of the Window control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void Window_Closed(object sender, EventArgs e)
		{
			this.countdowns.Close();
		}

		/// <summary>
		/// Ticks the specified sender.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void Ticks(object sender, EventArgs e)
		{
			foreach (var countdown in this.countdowns.Values)
			{
				if (countdown.CountdownsSettings != null)
				{
					TimeSpan differenceCountdown = countdown.CountdownsSettings.Start - DateTime.Now;

					if (differenceCountdown.Ticks <= 0)
					{
						icon.Show(
							string.Format(CultureInfo.InvariantCulture, "Start {0}", countdown.Name));

						countdown.CountdownsSettings.Start = countdown.CountdownsSettings.End;

						if (countdown.CountdownsSettings.Duration > 0)
						{
							TimeSpan differenceDuration = countdown.CountdownsSettings.End - DateTime.Now;

							if (differenceDuration.Ticks <= 0)
							{
								icon.Show(
									string.Format(CultureInfo.InvariantCulture, "End {0}", countdown.Name));

								if (CountdownsClient.CheckRepeats(countdown))
								{
									this.countdowns.UpdateReminder(countdown);
								}
								else
								{
									this.countdowns.Values.Remove(countdown);
									break;
								}
							}
						}
						else if (CountdownsClient.CheckRepeats(countdown))
						{
							this.countdowns.UpdateReminder(countdown);
						}
						else
						{
							this.countdowns.Values.Remove(countdown);
							break;
						}
					}
				}

				if (countdown.ProgressSettings != null)
				{
					TimeSpan differenceProgress = countdown.ProgressSettings.Start.AddMinutes(
						countdown.ProgressSettings.Interval) - DateTime.Now;

					if (differenceProgress.Ticks <= 0)
					{
						icon.Show(
							string.Format(CultureInfo.InvariantCulture, "Start {0}", countdown.Name));

						countdown.ProgressSettings.Start = countdown.ProgressSettings.Start.AddMinutes(
							countdown.ProgressSettings.Duration + countdown.ProgressSettings.Interval);
					}

					TimeSpan differenceDuration = countdown.ProgressSettings.End - DateTime.Now;

					if (differenceDuration.Ticks <= 0)
					{
						icon.Show(
							string.Format(CultureInfo.InvariantCulture, "End {0}", countdown.Name));

						countdown.ProgressSettings.End = countdown.ProgressSettings.End.AddMinutes(
							countdown.ProgressSettings.Duration + countdown.ProgressSettings.Interval);
					}
				}
			}
		}

		/// <summary>
		/// Logins the specified sender.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		private async void Login(object sender, RoutedEventArgs e)
		{
			if (await this.countdowns.Service.Connect(this.txtLogin.Text, this.pbPassword.Password))
			{
				icon.IsVisible = false;

				this.dispatcherTimer = new DispatcherTimer();
				this.dispatcherTimer.Tick += new EventHandler(this.Ticks);
				this.dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
				this.dispatcherTimer.Start();

				this.loginGrid.Visibility = System.Windows.Visibility.Hidden;
				this.accountGrid.Visibility = System.Windows.Visibility.Visible;
				this.lblInfo.Content = string.Format("Hello, {0}.", this.countdowns.Service.UserName);
			}
			else
			{
				(this.DataContext as ViewModel).GroupFadeOut = !(this.DataContext as ViewModel).GroupFadeOut;
			}
		}

		/// <summary>
		/// Handles the StateChanged event of the Window control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void Window_StateChanged(object sender, EventArgs e)
		{
			switch (this.WindowState)
			{
				case WindowState.Minimized:
					icon.IsVisible = false;
					break;
			}
		}

		/// <summary>
		/// Handles the RequestNavigate event of the Hyperlink control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RequestNavigateEventArgs" /> instance containing the event data.</param>
		private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			System.Diagnostics.Process.Start(e.Uri.ToString());
		}

		/// <summary>
		/// Handles the Click event of the SignOut control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
		private void SignOut_Click(object sender, RoutedEventArgs e)
		{
			this.loginGrid.Visibility = System.Windows.Visibility.Visible;
			this.accountGrid.Visibility = System.Windows.Visibility.Hidden;

			this.countdowns.Service.Disconnect();
		}

		#endregion
	}
}
