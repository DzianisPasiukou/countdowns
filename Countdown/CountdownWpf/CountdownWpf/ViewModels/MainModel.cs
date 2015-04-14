namespace CountdownWpf
{
	using System.ComponentModel;

	/// <summary>
	/// The view model for main window.
	/// </summary>
	public class ViewModel : INotifyPropertyChanged
	{
		#region Private Fields

		/// <summary>
		/// The group fade out
		/// </summary>
		private bool groupFadeOut;

		#endregion

		#region Public Events

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets a value indicating whether [group fade out].
		/// </summary>
		/// <value>
		///   <c>true</c> if [group fade out]; otherwise, <c>false</c>.
		/// </value>
		public bool GroupFadeOut
		{
			get
			{
				return this.groupFadeOut;
			}

			set
			{
				this.groupFadeOut = value;

				this.OnPropertyChanged("GroupFadeOut");
			}
		}

		#endregion

		#region Protected Methods

		/// <summary>
		/// Called when [property changed].
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
	}
}
