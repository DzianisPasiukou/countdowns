namespace CountdownDataBaseLayer.Repo
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;

	using CountdownDataBaseLayer.Comparer;

	/// <summary>
	/// The instance of the weeks repository.
	/// </summary>
	public class WeeksRepo : MyDbContainer<Weeks>, IDisposable
	{
		#region Public Methods

		/// <summary>
		/// Gets all entities of the week.
		/// </summary>
		/// <returns>
		/// IEnumerable of entity object
		/// </returns>
		public override IEnumerable<Weeks> GetAll()
		{
			IEnumerable<Weeks> weeks = this.Container.Weeks;
			return weeks;
		}

		/// <summary>
		/// Gets the by identifier entity of the weeks.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>
		/// The week.
		/// </returns>
		public override Weeks GetById(int id)
		{
			Weeks week = this.Container.Weeks.Find(id);
			return week;
		}

		/// <summary>
		/// Updates the many of the weeks.
		/// </summary>
		/// <param name="existingEntities">The existing entities.</param>
		/// <param name="updatedEntities">The updated entities.</param>
		public override void UpdateMany(IEnumerable<Weeks> existingEntities, IEnumerable<Weeks> updatedEntities)
		{
			var addedWeeks = updatedEntities.Except(existingEntities, new CompareWeeks());
			var deletedWeeks = existingEntities.Except(updatedEntities, new CompareWeeks());
			var modifiedWeeks = updatedEntities.Except(addedWeeks, new CompareWeeks());

			addedWeeks.ToList<Weeks>().ForEach(addWeek => this.Container.Entry(addWeek).State = EntityState.Added);

			deletedWeeks.ToList<Weeks>().ForEach(delWeek => this.Container.Weeks.Remove(this.Container.Weeks.Find(delWeek.Id)));

			foreach (Weeks week in modifiedWeeks)
			{
				var existingWeek = this.Container.Weeks.Find(week.Id);

				if (existingWeek != null)
				{
					var weekEntry = this.Container.Entry(existingWeek);
					weekEntry.CurrentValues.SetValues(week);
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
