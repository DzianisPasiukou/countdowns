namespace CountdownMvc.Models.UserIdentity
{
	using System;

	using Microsoft.AspNet.Identity;

	/// <summary>
	/// The instance of application user.
	/// </summary>
	public class ApplicationUser : IUser
	{
		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ApplicationUser"/> class.
		/// </summary>
		public ApplicationUser()
		{
			this.Id = Guid.NewGuid().ToString();
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public string Id { get; private set; }

		/// <summary>
		/// Gets or sets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		public string UserName { get; set; }

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>
		/// The password.
		/// </value>
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		public string Email { get; set; }

		#endregion
	}
}
