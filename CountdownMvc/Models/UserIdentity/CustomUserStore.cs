namespace CountdownMvc.Models.UserIdentity
{
	using System;
	using System.Runtime.InteropServices;
	using System.Threading.Tasks;

	using CountdownDataBaseLayer;
	using CountdownDataBaseLayer.Repo;

	using Microsoft.AspNet.Identity;

	/// <summary>
	/// The instance of custom user store.
	/// </summary>
	public class CustomUserStore : IUserStore<ApplicationUser>
	{
		#region Private Fields

		/// <summary>
		/// The users repo
		/// </summary>
		private IRepo<Users> usersRepo;

		/// <summary>
		/// The native resource
		/// </summary>
		private IntPtr nativeResource = Marshal.AllocHGlobal(100);

		#endregion

		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomUserStore"/> class.
		/// </summary>
		/// <param name="usersRepo">The users repo.</param>
		/// <exception cref="System.ArgumentNullException">IRepo users is null.</exception>
		public CustomUserStore(IRepo<Users> usersRepo)
		{
			if (usersRepo == null)
			{
				throw new ArgumentNullException("usersRepo", "IRepo users is null.");
			}

			this.usersRepo = usersRepo;
		}

		#endregion

		#region Destructor

		/// <summary>
		/// Finalizes an instance of the <see cref="CustomUserStore"/> class.
		/// </summary>
		~CustomUserStore()
		{
			// Finalizer calls Dispose(false)
			this.Dispose(false);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Creates the asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>The task of creating user asynchronous.</returns>
		public Task CreateAsync(ApplicationUser user)
		{
			return Task.Factory.StartNew(() =>
				{
					this.usersRepo.Add(new Users()
				{
					Email = user.Email,
					Name = user.UserName,
					Password = user.Password,
				});
					this.usersRepo.Save();
				});
		}

		/// <summary>
		/// Deletes the asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>The task of deleting user asynchronous.</returns>
		public Task DeleteAsync(ApplicationUser user)
		{
			return Task.Factory.StartNew(() =>
			{
				this.usersRepo.Delete(new Users()
			   {
				   Name = user.UserName
			   });
				this.usersRepo.Save();
			});
		}

		/// <summary>
		/// Finds the by identifier asynchronous.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns>
		/// The task of application user.
		/// </returns>
		/// <exception cref="System.NotImplementedException">Not implemented.</exception>
		public Task<ApplicationUser> FindByIdAsync(string userId)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Finds the by name asynchronous.
		/// </summary>
		/// <param name="userName">Name of the user.</param>
		/// <returns>The task of find user by name.</returns>
		public Task<ApplicationUser> FindByNameAsync(string userName)
		{
			Users user = this.usersRepo.GetByName(userName);

			return Task.Factory.StartNew(() => new ApplicationUser()
			{
				Email = user.Email,
				Password = user.Password,
				UserName = user.Name
			});
		}

		/// <summary>
		/// Updates the asynchronous.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>The task of updating user.</returns>
		public Task UpdateAsync(ApplicationUser user)
		{
			return Task.Factory.StartNew(() =>
				{
					this.usersRepo.Update(new Users()
				{
					Email = user.Email,
					Name = user.UserName,
					Password = user.Password
				});
					this.usersRepo.Save();
				});
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
				if (this.usersRepo != null)
				{
					this.usersRepo.Dispose();
					this.usersRepo = null;
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
	}
}
