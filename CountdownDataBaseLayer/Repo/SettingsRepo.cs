namespace CountdownDataBaseLayer.Repo
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;

	using CountdownDataBaseLayer.Comparer;

	/// <summary>
	/// The instance of the settings repository.
	/// </summary>
	public class SettingsRepo : MyDbContainer<Settings>, IDisposable
	{
		#region Public Methods

		/// <summary>
		/// Gets all entities of the settings.
		/// </summary>
		/// <returns>
		/// IEnumerable of entity object of the settings.
		/// </returns>
		public override IEnumerable<Settings> GetAll()
		{
			IEnumerable<Settings> settings = this.Container.Settings;
			return settings;
		}

		/// <summary>
		/// Gets the by identifier entity of the setting.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>
		/// The setting.
		/// </returns>
		public override Settings GetById(int id)
		{
			Settings setting = this.Container.Settings.Find(id);
			return setting;
		}

		/// <summary>
		/// Updates the many of the settings.
		/// </summary>
		/// <param name="existingEntities">The existing entities.</param>
		/// <param name="updatedEntities">The updated entities.</param>
		public override void UpdateMany(IEnumerable<Settings> existingEntities, IEnumerable<Settings> updatedEntities)
		{
			var addedSettings = updatedEntities.Except(existingEntities, new CompareSettings());
			var deletedSettings = existingEntities.Except(updatedEntities, new CompareSettings());
			var modifiedSettings = updatedEntities.Except(addedSettings, new CompareSettings());

			addedSettings.ToList<Settings>().ForEach(addSett => this.Container.Entry(addSett).State = EntityState.Added);

			deletedSettings.ToList<Settings>().ForEach(delSett => this.Container.Settings.Remove(this.Container.Settings.Find(delSett.Id)));

			foreach (Settings setting in modifiedSettings)
			{
				var existingSettings = this.Container.Settings.Find(setting.Id);

				if (existingSettings != null)
				{
					var settingsEntry = this.Container.Entry(existingSettings);
					settingsEntry.CurrentValues.SetValues(setting);
				}
			}

			this.Container.SaveChanges();
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
