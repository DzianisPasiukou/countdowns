namespace CountdownDataBaseLayer.Comparer
{
	using System.Collections.Generic;

	/// <summary>
	/// The instance to compare days.
	/// </summary>
	public class CompareDays : IEqualityComparer<Days>
	{
		#region Public Methods

		/// <summary>
		/// Determines whether the specified objects are equal.
		/// </summary>
		/// <param name="x">The first object of type Days to compare.</param>
		/// <param name="y">The second object of type Days to compare.</param>
		/// <returns>
		/// true if the specified objects are equal; otherwise, false.
		/// </returns>
		public bool Equals(Days x, Days y)
		{
			if (x.Number == y.Number)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Returns a hash code for this instance of the day.
		/// </summary>
		/// <param name="obj">The day.</param>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public int GetHashCode(Days obj)
		{
			return obj.Id.GetHashCode();
		}

		#endregion
	}
}
