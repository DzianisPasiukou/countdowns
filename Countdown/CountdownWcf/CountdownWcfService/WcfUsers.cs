namespace CountdownWcfService
{
	/// <summary>
	/// The instance of user.
	/// </summary>
	public class WcfUsers
	{
		#region Public Properties

		/// <summary>
		/// Gets or sets the user name.
		/// </summary>
		/// <value>
		/// The user name.
		/// </value>
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets the callback.
		/// </summary>
		/// <value>
		/// The callback.
		/// </value>
		public ICountdownCallback Callback { get; set; }

		#endregion
	}
}