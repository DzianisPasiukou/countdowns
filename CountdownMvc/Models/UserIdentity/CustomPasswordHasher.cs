namespace CountdownMvc.Models.UserIdentity
{
	using System;

	using CountdownBusinessLogic.Security;

	using Microsoft.AspNet.Identity;

	/// <summary>
	/// The instance of custom password hasher.
	/// </summary>
	public class CustomPasswordHasher : PasswordHasher
	{
		#region Private Fields
		/// <summary>
		/// The hash calculator
		/// </summary>
		private IHashCalculator hashCalculator;

		#endregion

		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomPasswordHasher"/> class.
		/// </summary>
		/// <param name="hashCalculator">The hash calculator.</param>
		/// <exception cref="System.ArgumentNullException">IHashCalculator is null.</exception>
		public CustomPasswordHasher(IHashCalculator hashCalculator)
		{
			if (hashCalculator == null)
			{
				throw new ArgumentNullException("hashCalculator", "IHashCalculator is null.");
			}

			this.hashCalculator = hashCalculator;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Hash a password
		/// </summary>
		/// <param name="password">The password.</param>
		/// <returns>The hash password.</returns>
		public override string HashPassword(string password)
		{
			return this.hashCalculator.Calculate(password);
		}

		/// <summary>
		/// Verify that a password matches the hashedPassword
		/// </summary>
		/// <param name="hashedPassword">The hashed password.</param>
		/// <param name="providedPassword">The provided password.</param>
		/// <returns>The result of verification password.</returns>
		public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
		{
			bool isSuccess = this.hashCalculator.Check(providedPassword, hashedPassword);

			if (isSuccess)
			{
				return PasswordVerificationResult.Success;
			}
			else
			{
				return PasswordVerificationResult.Failed;
			}
		}

		#endregion
	}
}