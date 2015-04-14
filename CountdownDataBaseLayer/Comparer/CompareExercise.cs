namespace CountdownDataBaseLayer.Comparer
{
	using System.Collections.Generic;

	/// <summary>
	/// The instance of compare exercises for compare exercise.
	/// </summary>
	public class CompareExercise : IEqualityComparer<Exercises>
	{
		#region Public Methods

		/// <summary>
		/// Determines whether the exercises are equal.
		/// </summary>
		/// <param name="x">The first object of type Exercise to compare.</param>
		/// <param name="y">The second object of type Exercise to compare.</param>
		/// <returns>
		/// true if the specified objects are equal; otherwise, false.
		/// </returns>
		public bool Equals(Exercises x, Exercises y)
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
		/// Returns a hash code for this the exercises.
		/// </summary>
		/// <param name="obj">The exercises.</param>
		/// <returns>
		/// A hash code for this exercises, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public int GetHashCode(Exercises obj)
		{
			return obj.Id.GetHashCode();
		}

		#endregion
	}
}
