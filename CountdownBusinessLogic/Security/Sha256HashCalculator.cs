namespace CountdownBusinessLogic.Security
{
	using System;
	using System.Linq;
	using System.Security.Cryptography;
	using System.Text;

	/// <summary>
	/// The instance of hash calculator of SHA256 algorithm.
	/// </summary>
	public class Sha256HashCalculator : IHashCalculator
	{
		#region Private Fields

		/// <summary>
		/// The minimum salt size.
		/// </summary>
		private const int MinSaltSize = 4;

		/// <summary>
		/// The maximum salt size.
		/// </summary>
		private const int MaxSaltSize = 8;

		/// <summary>
		/// The hash size.
		/// </summary>
		private const int HashSize = 32;

		#endregion

		#region Public Methods

		/// <summary>
		/// Calculates the specified raw.
		/// </summary>
		/// <param name="raw">The raw.</param>
		/// <param name="saltValue">The salt bytes.</param>
		/// <returns>
		/// The hash of raw.
		/// </returns>
		public string Calculate(string raw, byte[] saltValue = null)
		{
			if (saltValue == null || !saltValue.Any())
			{
				saltValue = new byte[new Random().Next(MinSaltSize, MaxSaltSize)];
				using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
				{
					rngCryptoServiceProvider.GetNonZeroBytes(saltValue);
				}
			}

			using (var sha = new SHA256Managed())
			{
				return Convert.ToBase64String(
					sha.ComputeHash(Encoding.UTF8.GetBytes(raw).Concat(saltValue).ToArray())
						.Concat(saltValue).ToArray());
			}
		}

		/// <summary>
		/// Checks the specified raw.
		/// </summary>
		/// <param name="raw">The raw.</param>
		/// <param name="hash">The hash.</param>
		/// <returns>
		/// Is true string.
		/// </returns>
		public bool Check(string raw, string hash)
		{
			return this.Calculate(raw, Convert.FromBase64String(hash).Skip(HashSize).ToArray()).Equals(hash);
		}

		#endregion
	}
}
