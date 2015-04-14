namespace CountdownMvc.Models.UserIdentity
{
	using System;
	using System.Threading.Tasks;

	using CountdownBusinessLogic.Security;

	using Microsoft.AspNet.Identity;

	/// <summary>
	/// The instance of custom user manager.
	/// </summary>
	public class CustomUserManager : UserManager<ApplicationUser>
	{
		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomUserManager"/> class.
		/// </summary>
		/// <param name="store">The store.</param>
		/// <param name="hashCalculator">The hash calculator.</param>
		public CustomUserManager(CustomUserStore store, IHashCalculator hashCalculator)
			: base(store)
		{
			this.PasswordHasher = new CustomPasswordHasher(hashCalculator);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Finds the asynchronous.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <param name="password">The password.</param>
		/// <returns>The task of the application user.</returns>
		public override Task<ApplicationUser> FindAsync(string userName, string password)
		{
			Task<ApplicationUser> taskInvoke = Task<ApplicationUser>.Factory.StartNew(() =>
			{
				try
				{
					ApplicationUser user = Store.FindByNameAsync(userName).Result;

					PasswordVerificationResult result = this.PasswordHasher.VerifyHashedPassword(user.Password, password);

					if (result == PasswordVerificationResult.Success)
					{
						return user;
					}
				}
				catch (Exception)
				{
					return null;
				}

				return null;
			});
			return taskInvoke;
		}

		/// <summary>
		/// Finds the specified user.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <param name="password">The password.</param>
		/// <returns>The user.</returns>
		public ApplicationUser Find(string userName, string password)
		{
			try
			{
				ApplicationUser user = Store.FindByNameAsync(userName).Result;

				PasswordVerificationResult result = this.PasswordHasher.VerifyHashedPassword(user.Password, password);

				if (result == PasswordVerificationResult.Success)
				{
					return user;
				}
			}
			catch (Exception)
			{
				return null;
			}

			return null;
		}

		/// <summary>
		/// Creates the asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>The task of creation asynchronous.</returns>
		public new Task CreateAsync(ApplicationUser user)
		{
			user.Password = this.PasswordHasher.HashPassword(user.Password);

			return Store.CreateAsync(user);
		}

		/// <summary>
		/// Determines whether the specified user name is exist.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <returns>Is exist user.</returns>
		public bool IsExist(string userName)
		{
			try
			{
				ApplicationUser user = Store.FindByNameAsync(userName).Result;

				return user != null;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Updates the asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>The task of updating asynchronous.</returns>
		public new Task UpdateAsync(ApplicationUser user)
		{
			user.Password = this.PasswordHasher.HashPassword(user.Password);

			return Store.UpdateAsync(user);
		}

		#endregion
	}
}
