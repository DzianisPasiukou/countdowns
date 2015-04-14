namespace CountdownDataBaseLayer.Repo
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;

	/// <summary>
	/// Repository for reminder.
	/// </summary>
	public class ReminderRepo : MyDbContainer<Reminder>, IDisposable
	{
		#region Public Methods

		/// <summary>
		/// Gets all Reminders.
		/// </summary>
		/// <returns>
		/// IEnumerable of entity object
		/// </returns>
		public override IEnumerable<Reminder> GetAll()
		{
			IEnumerable<Reminder> reminders = this.Container.Reminders.OfType<Reminder>()
				.Include(rs => rs.ReminderSettings)
				.Include(e => e.Exercises)
				.Include(t => t.TypeOfReminder)
				.Include(pr => pr.ProgressSetting)
				.Include(c => c.CountdownSetting).ToArray();

			return reminders;
		}

		/// <summary>
		/// Gets the by identifier Reminder.
		/// </summary>
		/// <param name="id">The identifier of reminder.</param>
		/// <returns>
		/// Reminder object
		/// </returns>
		public override Reminder GetById(int id)
		{
			return this.Container.Reminders.OfType<Reminder>()
				.Include(rs => rs.ReminderSettings)
				.Include(e => e.Exercises)
				.Include(t => t.TypeOfReminder)
				.Include(pr => pr.ProgressSetting)
				.Include(c => c.CountdownSetting)
				.FirstOrDefault(r => r.Id == id);
		}

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public override void Delete(Reminder entity)
		{
			this.Container.Reminders.Remove(entity);
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
