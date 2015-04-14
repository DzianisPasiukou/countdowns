namespace CountdownDataBaseLayer.Repo
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;

	using CountdownDataBaseLayer.Comparer;

	/// <summary>
	/// Repository for reminder settings.
	/// </summary>
	public class ReminderSettingsRepo : MyDbContainer<ReminderSettings>, IDisposable
	{
		#region Public Methods

		/// <summary>
		/// Gets all reminder settings.
		/// </summary>
		/// <returns>
		/// IEnumerable of reminder settings.
		/// </returns>
		public override IEnumerable<ReminderSettings> GetAll()
		{
			IEnumerable<ReminderSettings> settings = this.Container.ReminderSettings.OfType<ReminderSettings>()
				.Include(s => s.Reminder)
				.Include(s => s.Setting)
				.ToArray();

			return settings;
		}

		/// <summary>
		/// Gets the by identifier of reminder setting.
		/// </summary>
		/// <param name="id">The identifier of reminder setting.</param>
		/// <returns>
		/// The reminder settings.
		/// </returns>
		public override ReminderSettings GetById(int id)
		{
			ReminderSettings setting = this.Container.ReminderSettings
				.Include(s => s.Reminder)
				.Include(s => s.Setting)
				.First(f => f.ReminderId == id);

			return setting;
		}

		/// <summary>
		/// Updates the many reminder settings.
		/// </summary>
		/// <param name="existingEntities">The existing entities.</param>
		/// <param name="updatedEntities">The updated entities.</param>
		public override void UpdateMany(IEnumerable<ReminderSettings> existingEntities, IEnumerable<ReminderSettings> updatedEntities)
		{
			var addedSettings = updatedEntities.Except(existingEntities, new CompareReminderSettings());
			var deletedSettings = existingEntities.Except(updatedEntities, new CompareReminderSettings());
			var modifiedSettings = updatedEntities.Except(addedSettings, new CompareReminderSettings());

			addedSettings.ToList<ReminderSettings>().ForEach(addSett => this.Container.ReminderSettings.Add(addSett));

			deletedSettings.ToList<ReminderSettings>().ForEach(delSett => this.Container.ReminderSettings.Remove(
				this.Container.ReminderSettings.First(rs => rs.ReminderId == delSett.ReminderId && rs.SettingsId == delSett.SettingsId)));

			foreach (ReminderSettings setting in modifiedSettings)
			{
				var existingSettings = this.Container.ReminderSettings.Find(setting.ReminderId, setting.SettingsId);

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
