namespace CountdownDataBaseLayer.Repo
{
	using System;
	using System.Collections;
	using System.Collections.Generic;

	/// <summary>
	/// Interface for repository.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	public interface IRepo<TEntity> : IDisposable
		where TEntity : new()
	{
		#region Methods

		/// <summary>
		/// Gets all entities.
		/// </summary>
		/// <returns>
		/// IEnumerable of entity object
		/// </returns>
		IEnumerable<TEntity> GetAll();

		/// <summary>
		/// Gets the by identifier entity.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>
		/// Entity object
		/// </returns>
		TEntity GetById(int id);

		/// <summary>
		/// Gets the name of the entity.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>
		/// The entity.
		/// </returns>
		TEntity GetByName(string name);

		/// <summary>
		/// Adds the specified entity.
		/// </summary>
		/// <param name="entity">The t.</param>
		void Add(TEntity entity);

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The t.</param>
		void Delete(TEntity entity);

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The t.</param>
		void Update(TEntity entity);

		/// <summary>
		/// Updates the many entities.
		/// </summary>
		/// <param name="existingEntities">The existing entities.</param>
		/// <param name="updatedEntities">The updated entities.</param>
		void UpdateMany(IEnumerable<TEntity> existingEntities, IEnumerable<TEntity> updatedEntities);

		/// <summary>
		/// Saves this instance.
		/// </summary>
		void Save();

		#endregion
	}
}
