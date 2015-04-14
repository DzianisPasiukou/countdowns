namespace CountdownDataBaseLayer.Repo
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// The instance of countdown settings repo.
	/// </summary>
	public class CountdownSettingsRepo : MyDbContainer<CountdownSettings>, IDisposable
	{
		#region Public Methods

		/// <summary>
		/// Gets all entities.
		/// </summary>
		/// <returns>
		/// IEnumerable of entities.
		/// </returns>
		public override IEnumerable<CountdownSettings> GetAll()
		{
			return this.Container.CountdownSettings.ToArray();
		}

		/// <summary>
		/// Gets the by identifier of entity.
		/// </summary>
		/// <param name="id">The identifier of entity.</param>
		/// <returns>
		/// The entity.
		/// </returns>
		public override CountdownSettings GetById(int id)
		{
			return this.Container.CountdownSettings.Find(id);
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
