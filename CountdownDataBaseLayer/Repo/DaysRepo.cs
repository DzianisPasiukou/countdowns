namespace CountdownDataBaseLayer.Repo
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using CountdownDataBaseLayer.Comparer;

	/// <summary>
	/// The instance of days repository.
	/// </summary>
	public class DaysRepo : MyDbContainer<Days>, IDisposable
	{
		#region Public Methods

		/// <summary>
		/// Gets all entities of the days.
		/// </summary>
		/// <returns>
		/// IEnumerable of entity object of the days.
		/// </returns>
		public override IEnumerable<Days> GetAll()
		{
			IEnumerable<Days> days = this.Container.Days;
			return days;
		}

		/// <summary>
		/// Gets the by identifier entity of the days.
		/// </summary>
		/// <param name="id">The identifier of day.</param>
		/// <returns>
		/// The day.
		/// </returns>
		public override Days GetById(int id)
		{
			Days day = this.Container.Days.Find(id);
			return day;
		}

		/// <summary>
		/// Updates the many of the days.
		/// </summary>
		/// <param name="existingEntities">The existing entities.</param>
		/// <param name="updatedEntities">The updated entities.</param>
		public override void UpdateMany(IEnumerable<Days> existingEntities, IEnumerable<Days> updatedEntities)
		{
			var addedDays = updatedEntities.Except(existingEntities, new CompareDays());
			var deletedDays = existingEntities.Except(updatedEntities, new CompareDays());
			var modifiedDays = updatedEntities.Except(addedDays, new CompareDays());

			addedDays.ToList<Days>().ForEach(addDay => this.Container.Days.Add(addDay));

			deletedDays.ToList<Days>().ForEach(delDay => this.Container.Days.Remove(this.Container.Days.Find(delDay.Id)));

			foreach (Days day in modifiedDays)
			{
				var existingDay = this.Container.Days.Find(day.Id);

				if (existingDay != null)
				{
					var dayEntry = this.Container.Entry(existingDay);
					dayEntry.CurrentValues.SetValues(day);
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
