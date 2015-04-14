namespace CountdownDataBaseLayer.Comparer
{
	using System.Collections.Generic;

	/// <summary>
	/// The instance of compare Files for compare Files.
	/// </summary>
	public class CompareFiles : IEqualityComparer<Files>
	{
		#region Public Methods

		/// <summary>
		/// Determines whether the specified Files are equal.
		/// </summary>
		/// <param name="x">The first object of type Files to compare.</param>
		/// <param name="y">The second object of type Files to compare.</param>
		/// <returns>
		/// true if the specified objects are equal; otherwise, false.
		/// </returns>
		public bool Equals(Files x, Files y)
		{
			if (x.Id == 0)
			{
				return false;
			}

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
		/// Returns a hash code for this Files.
		/// </summary>
		/// <param name="obj">The Files.</param>
		/// <returns>
		/// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public int GetHashCode(Files obj)
		{
			return obj.Id.GetHashCode();
		}

		#endregion
	}
}
