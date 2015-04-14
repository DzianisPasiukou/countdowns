namespace CountdownDataBaseLayer.Repo
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Runtime.InteropServices;

	/// <summary>
	/// This is class for container with data base context from entity framework.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	public abstract class MyDbContainer<TEntity> : IRepo<TEntity>, IDisposable
		where TEntity : class, new()
	{
		#region Private Fields

		/// <summary>
		/// The data base container of Countdowns.
		/// </summary>
		private CountdownDBContainer db;

		/// <summary>
		/// The native resource.
		/// </summary>
		private IntPtr nativeResource = Marshal.AllocHGlobal(100);

		#endregion

		#region Public Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="MyDbContainer{TEntity}"/> class.
		/// </summary>
		public MyDbContainer()
		{
			this.db = new CountdownDBContainer();
		}

		#endregion

		#region Desctructor

		/// <summary>
		/// Finalizes an instance of the <see cref="MyDbContainer{TEntity}"/> class.
		/// </summary>
		~MyDbContainer()
		{
			this.Dispose(false);
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the container.
		/// </summary>
		/// <value>
		/// The container.
		/// </value>
		public CountdownDBContainer Container
		{
			get
			{
				return this.db;
			}

			set
			{
				this.db = value;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets all entities.
		/// </summary>
		/// <returns>
		/// IEnumerable of entities.
		/// </returns>
		public virtual IEnumerable<TEntity> GetAll()
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Gets the by identifier of entity.
		/// </summary>
		/// <param name="id">The identifier of entity.</param>
		/// <returns>
		/// The entity.
		/// </returns>
		public virtual TEntity GetById(int id)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Adds the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Add(TEntity entity)
		{
			this.Container.Entry(entity).State = EntityState.Added;
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Delete(TEntity entity)
		{
			this.Container.Entry(entity).State = EntityState.Detached;
		}

		/// <summary>
		/// Updates the entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public virtual void Update(TEntity entity)
		{
			this.Container.Entry(entity).State = EntityState.Modified;
		}

		/// <summary>
		/// Updates the many entities.
		/// </summary>
		/// <param name="existingEntities">The existing entities.</param>
		/// <param name="updatedEntities">The updated entities.</param>
		/// <exception cref="System.NotImplementedException">Not implemented method!</exception>
		public virtual void UpdateMany(IEnumerable<TEntity> existingEntities, IEnumerable<TEntity> updatedEntities)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Gets the name of the entity.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>
		/// The entity.
		/// </returns>
		/// <exception cref="System.NotImplementedException">Not implemented.</exception>
		public virtual TEntity GetByName(string name)
		{
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Saves this instance.
		/// </summary>
		public virtual void Save()
		{
			this.Container.SaveChanges();
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
		/// <param name="disposing">Dispose state.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.db != null)
				{
					this.ReleaseManagedResources();
				}
			}

			if (this.nativeResource != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(this.nativeResource);
				this.nativeResource = IntPtr.Zero;
			}
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Releases the managed resources.
		/// </summary>
		private void ReleaseManagedResources()
		{
			if (this.db != null)
			{
				this.db.Dispose();
				this.db = null;
			}
		}

		#endregion
	}
}
