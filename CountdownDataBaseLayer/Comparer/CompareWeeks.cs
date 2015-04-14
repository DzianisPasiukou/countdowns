namespace CountdownDataBaseLayer.Comparer
{
	using System.Collections.Generic;

	/// <summary>
	/// The instance of compare Weeks for compare Weeks.
	/// </summary>
	public class CompareWeeks : IEqualityComparer<Weeks>
	{
		#region Public Methods

		/// <summary>
		/// Determines whether the specified Weeks are equal.
		/// </summary>
		/// <param name="x">The first object of type Weeks to compare.</param>
		/// <param name="y">The second object of type Weeks to compare.</param>
		/// <returns>
		/// true if the specified Weeks are equal; otherwise, false.
		/// </returns>
		public bool Equals(Weeks x, Weeks y)
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
		/// Returns a hash code for this Weeks.
		/// </summary>
		/// <param name="obj">The Weeks.</param>
		/// <returns>
		/// A hash code for this Weeks, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public int GetHashCode(Weeks obj)
		{
			return obj.Id.GetHashCode();
		}

		#endregion
	}
}
