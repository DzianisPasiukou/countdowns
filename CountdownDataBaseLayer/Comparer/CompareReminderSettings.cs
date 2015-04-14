namespace CountdownDataBaseLayer.Comparer
{
	using System.Collections.Generic;

	/// <summary>
	/// The instance of compare ReminderSettings for compare ReminderSettings.
	/// </summary>
	public class CompareReminderSettings : IEqualityComparer<ReminderSettings>
	{
		#region Public Methods

		/// <summary>
		/// Determines whether the specified ReminderSettings are equal.
		/// </summary>
		/// <param name="x">The first object of type ReminderSettings to compare.</param>
		/// <param name="y">The second object of type ReminderSettings to compare.</param>
		/// <returns>
		/// true if the specified ReminderSettings are equal; otherwise, false.
		/// </returns>
		public bool Equals(ReminderSettings x, ReminderSettings y)
		{
			if ((x.ReminderId == y.ReminderId) && (x.SettingsId == y.SettingsId))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Returns a hash code for this ReminderSettings.
		/// </summary>
		/// <param name="obj">The ReminderSettings.</param>
		/// <returns>
		/// A hash code for this ReminderSettings, suitable for use in hashing algorithms and data structures like a hash table. 
		/// </returns>
		public int GetHashCode(ReminderSettings obj)
		{
			return obj.SettingsId.GetHashCode() + obj.ReminderId.GetHashCode();
		}

		#endregion
	}
}
