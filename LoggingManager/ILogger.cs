namespace LoggingManager
{
	using System;

	/// <summary>
	/// The logger.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	public interface ILogger<TEntity>
	{
		#region Properties

		/// <summary>
		/// Gets or sets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		string UserName { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Writes the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="type">The type.</param>
		void Write(string message, TypeMessage type);

		/// <summary>
		/// Writes log with the exception.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception.</param>
		void WriteException(string message, Exception exception);

		#endregion
	}
}
