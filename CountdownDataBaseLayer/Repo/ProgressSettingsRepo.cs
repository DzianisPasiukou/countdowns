namespace CountdownDataBaseLayer.Repo
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// The instance of Progress Settings Repository.
	/// </summary>
	public class ProgressSettingsRepo : MyDbContainer<ProgressSettings>, IDisposable
	{
		#region Public Methods

		/// <summary>
		/// Gets all entities from progress settings.
		/// </summary>
		/// <returns>
		/// IEnumerable of progress settings object
		/// </returns>
		public override IEnumerable<ProgressSettings> GetAll()
		{
			return this.Container.ProgressSettings.ToArray();
		}

		/// <summary>
		/// Gets the by identifier progress settings.
		/// </summary>
		/// <param name="id">The identifier of progress settings.</param>
		/// <returns>
		/// Progress settings object
		/// </returns>
		public override ProgressSettings GetById(int id)
		{
			return this.Container.ProgressSettings.Find(id);
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
