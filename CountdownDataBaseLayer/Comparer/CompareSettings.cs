namespace CountdownDataBaseLayer.Comparer
{
	using System.Collections.Generic;

	/// <summary>
	/// The instance of compare Settings for compare Settings.
	/// </summary>
	public class CompareSettings : IEqualityComparer<Settings>
	{
		#region Public Methods

		/// <summary>
		/// Determines whether the specified Settings are equal.
		/// </summary>
		/// <param name="x">The first object of type Settings to compare.</param>
		/// <param name="y">The second object of typeSettings to compare.</param>
		/// <returns>
		/// true if the specified objects are equal; otherwise, false.
		/// </returns>
		public bool Equals(Settings x, Settings y)
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
		/// Returns a hash code for this Settings.
		/// </summary>
		/// <param name="obj">The Settings.</param>
		/// <returns>
		/// A hash code for this Settings, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public int GetHashCode(Settings obj)
		{
			return obj.Id.GetHashCode();
		}

		#endregion
	}
}
