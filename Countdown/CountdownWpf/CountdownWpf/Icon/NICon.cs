namespace CountdownWpf.Icon
{
	using System;
	using System.Globalization;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Windows.Forms;

	/// <summary>
	/// The instance for notify icon.
	/// </summary>
	public class NICon : IDisposable
	{
		#region Private Fields

		/// <summary>
		/// The notify icon.
		/// </summary>
		private NotifyIcon notifyIcon = new NotifyIcon();

		/// <summary>
		/// The locker.
		/// </summary>
		private object locker = new object();

		/// <summary>
		/// The native resource
		/// </summary>
		private IntPtr nativeResource = Marshal.AllocHGlobal(100);

		/// <summary>
		/// The is visible.
		/// </summary>
		private bool isVisible;

		/// <summary>
		/// The visible.
		/// </summary>
		private IVisible visible;

		#endregion

		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="NICon" /> class.
		/// </summary>
		/// <param name="visible">The visible.</param>
		/// <exception cref="ArgumentNullException">visible;IVisible is null.</exception>
		public NICon(IVisible visible)
		{
			this.notifyIcon.Icon = ContentResource.Hopstarter_Book_Reminders_Mac_Book;
			this.notifyIcon.Visible = true;
			this.notifyIcon.DoubleClick += this.DoubleClick;

			ContextMenu m_menu = new ContextMenu();

			m_menu.MenuItems.Add(0, new MenuItem("Open", new EventHandler(this.OnOpen)));
			m_menu.MenuItems.Add(1, new MenuItem("Exit", new EventHandler(this.OnClosing)));

			this.notifyIcon.ContextMenu = m_menu;

			if (visible == null)
			{
				throw new ArgumentNullException("visible", "IVisible is null.");
			}

			this.visible = visible;
		}
		
		#endregion

		#region Destructor

		/// <summary>
		/// Finalizes an instance of the <see cref="NICon"/> class.
		/// </summary>
		~NICon()
		{
			// Finalizer calls Dispose(false)
			this.Dispose(false);
		}

		#endregion

		#region Public Events

		/// <summary>
		/// Occurs when window is closing.
		/// </summary>
		public event EventHandler Closing;

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>
		/// The instance.
		/// </value>
		public NotifyIcon Instance
		{
			get
			{
				return this.notifyIcon;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is visible.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is visible; otherwise, <c>false</c>.
		/// </value>
		public bool IsVisible
		{
			get
			{
				return this.isVisible;
			}

			set
			{
				this.isVisible = value;

				if (this.isVisible)
				{
					this.visible.Show();
				}
				else
				{
					this.visible.Hide();
				}
			}
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Shows the specified message.
		/// </summary>
		/// <param name="text">The text.</param>
		public void Show(string text)
		{
			Thread t = new Thread(() => this.ShowTread(text));

			t.IsBackground = true;
			t.Priority = ThreadPriority.Lowest;
			t.Start();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// free managed resources
				if (this.notifyIcon != null)
				{
					this.notifyIcon.Dispose();
					this.notifyIcon = null;
				}
			}

			// free native resources if there are any.
			if (this.nativeResource != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.nativeResource);
				this.nativeResource = IntPtr.Zero;
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Called when windows is closing.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void OnClosing(object sender, EventArgs e)
		{
			this.notifyIcon.Visible = false;

			if (this.Closing != null)
			{
				this.Closing(sender, e);
			}
		}

		/// <summary>
		/// Called when open button is clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void OnOpen(object sender, EventArgs e)
		{
			this.IsVisible = true;
		}

		/// <summary>
		/// Handles the DoubleClick event of the notifyIcon control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void DoubleClick(object sender, EventArgs e)
		{
			this.IsVisible = !this.IsVisible;
		}

		/// <summary>
		/// Shows the message in background.
		/// </summary>
		/// <param name="text">The text.</param>
		private void ShowTread(string text)
		{
			lock (this.locker)
			{
				this.notifyIcon.BalloonTipTitle = "Notification";
				this.notifyIcon.BalloonTipText += string.Format(CultureInfo.InvariantCulture, "{0} \n", text);

				this.notifyIcon.ShowBalloonTip(1000);
			}

			Thread.Sleep(5000);

			this.notifyIcon.BalloonTipTitle = string.Empty;
			this.notifyIcon.BalloonTipText = string.Empty;
		}

		#endregion
	}
}
