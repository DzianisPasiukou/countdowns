namespace CountdownDataBaseLayer.Comparer
{
	using System.Collections.Generic;

	/// <summary>
	/// The instance for compare two images.
	/// </summary>
	public class CompareImages : IEqualityComparer<Images>
	{
		#region Public Methods

		/// <summary>
		/// Determines whether the specified images are equal.
		/// </summary>
		/// <param name="x">The first image of type <paramref name="Images" /> to compare.</param>
		/// <param name="y">The second image of type <paramref name="Images" /> to compare.</param>
		/// <returns>
		/// true if the specified images are equal; otherwise, false.
		/// </returns>
		public bool Equals(Images x, Images y)
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
		/// Returns a hash code for this images.
		/// </summary>
		/// <param name="obj">The image.</param>
		/// <returns>
		/// A hash code for this image, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public int GetHashCode(Images obj)
		{
			return obj.Id.GetHashCode();
		}

		#endregion
	}
}
