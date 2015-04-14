namespace CountdownBusinessLogic.Security
{
	/// <summary>
	/// The hash calculator.
	/// </summary>
	public interface IHashCalculator
	{
		#region Methods

		/// <summary>
		/// Calculates the specified raw.
		/// </summary>
		/// <param name="raw">The raw.</param>
		/// <param name="saltValue">The salt bytes.</param>
		/// <returns>The hash of raw.</returns>
		string Calculate(string raw, byte[] saltValue = null);

		/// <summary>
		/// Checks the specified raw.
		/// </summary>
		/// <param name="raw">The raw.</param>
		/// <param name="hash">The hash.</param>
		/// <returns>Is true string.</returns>
		bool Check(string raw, string hash);

		#endregion
	}
}
