namespace CountdownDataBaseLayer.Comparer
{
	using System.Collections.Generic;

	/// <summary>
	/// The instance of compare Months for compare Months.
	/// </summary>
	public class CompareMonth : IEqualityComparer<Monthes>
	{
		#region Public Methods

		/// <summary>
		/// Determines whether the specified Months are equal.
		/// </summary>
		/// <param name="x">The first object of type Months to compare.</param>
		/// <param name="y">The second object of type Months to compare.</param>
		/// <returns>
		/// true if the specified objects are equal; otherwise, false.
		/// </returns>
		public bool Equals(Monthes x, Monthes y)
		{
			if (x.Id == y.Id)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Returns a hash code for this Months.
		/// </summary>
		/// <param name="obj">The Months.</param>
		/// <returns>
		/// A hash code for this Months, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public int GetHashCode(Monthes obj)
		{
			return obj.Id.GetHashCode();
		}

		#endregion
	}
}
