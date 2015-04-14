namespace CountdownDataBaseLayer.Repo
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;

	/// <summary>
	/// The instance of users repository.
	/// </summary>
	public class UsersRepo : MyDbContainer<Users>, IDisposable
	{
		#region Public Methods

		/// <summary>
		/// Gets all entities.
		/// </summary>
		/// <returns>
		/// IEnumerable of entity object
		/// </returns>
		public override IEnumerable<Users> GetAll()
		{
			IEnumerable<Users> users = this.Container.Users;
			return users;
		}

		/// <summary>
		/// Gets the name of the entity.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>
		/// The entity.
		/// </returns>
		public override Users GetByName(string name)
		{
			Users user = this.Container.Users.First(u => u.Name == name);
			return user;
		}

		/// <summary>
		/// Updates the entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Update(Users entity)
		{
			Users old = this.Container.Users.Find(entity.Name);

			old.Password = entity.Password;
			old.Email = entity.Email;

			base.Update(old);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing">Dispose state.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		#endregion
	}
}
