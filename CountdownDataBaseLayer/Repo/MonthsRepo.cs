namespace CountdownDataBaseLayer.Repo
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;

	using CountdownDataBaseLayer.Comparer;

	/// <summary>
	/// Repository for months.
	/// </summary>
	public class MonthsRepo : MyDbContainer<Monthes>, IDisposable
	{
		#region Public Methods

		/// <summary>
		/// Gets all months.
		/// </summary>
		/// <returns>
		/// IEnumerable of months.
		/// </returns>
		public override IEnumerable<Monthes> GetAll()
		{
			IEnumerable<Monthes> months = this.Container.Monthes;
			return months;
		}

		/// <summary>
		/// Gets the by identifier month.
		/// </summary>
		/// <param name="id">The identifier of month.</param>
		/// <returns>
		/// The month.
		/// </returns>
		public override Monthes GetById(int id)
		{
			Monthes months = this.Container.Monthes.Find(id);
			return months;
		}

		/// <summary>
		/// Updates the many months.
		/// </summary>
		/// <param name="existingEntities">The existing entities.</param>
		/// <param name="updatedEntities">The updated entities.</param>
		public override void UpdateMany(IEnumerable<Monthes> existingEntities, IEnumerable<Monthes> updatedEntities)
		{
			var addedMonth = updatedEntities.Except(existingEntities, new CompareMonth());
			var deletedMonth = existingEntities.Except(updatedEntities, new CompareMonth());
			var modifiedMonth = updatedEntities.Except(addedMonth, new CompareMonth());

			addedMonth.ToList<Monthes>().ForEach(addMonth => this.Container.Entry(addMonth).State = EntityState.Added);

			deletedMonth.ToList<Monthes>().ForEach(delMonth => this.Container.Monthes.Remove(this.Container.Monthes.Find(delMonth.Id)));

			foreach (Monthes month in modifiedMonth)
			{
				var existingMonth = this.Container.Monthes.Find(month.Id);

				if (existingMonth != null)
				{
					var monthEntry = this.Container.Entry(existingMonth);
					monthEntry.CurrentValues.SetValues(month);
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
