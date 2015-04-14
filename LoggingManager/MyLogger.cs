namespace LoggingManager
{
	using System;
	using System.Globalization;

	using NLog;

	/// <summary>
	/// The enumerable for logging level.
	/// </summary>
	public enum TypeMessage
	{
		/// <summary>
		/// The trace
		/// </summary>
		Trace,

		/// <summary>
		/// The information
		/// </summary>
		Info,

		/// <summary>
		/// The debug
		/// </summary>
		Debug,

		/// <summary>
		/// The warning
		/// </summary>
		Warning,

		/// <summary>
		/// The error
		/// </summary>
		Error
	}

	/// <summary>
	/// Realization of logging by nLog.
	/// </summary>
	/// <typeparam name="TEntity">The type of the entity.</typeparam>
	public class MyLogger<TEntity> : ILogger<TEntity>
	{
		#region Private Fields

		/// <summary>
		/// The logger by nLog
		/// </summary>
		private Logger log;

		/// <summary>
		/// The logger name.
		/// </summary>
		private string loggerName;

		/// <summary>
		/// The locker object.
		/// </summary>
		private object locker = new object();

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the name of the user.
		/// </summary>
		/// <value>
		/// The name of the user.
		/// </value>
		public string UserName
		{
			get
			{
				return this.loggerName;
			}

			set
			{
				this.loggerName = string.Format(CultureInfo.InvariantCulture, @"{0}/{1}", value, typeof(TEntity).ToString());
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Writes the specified message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="type">The type.</param>
		public void Write(string message, TypeMessage type)
		{
			if (this.log == null)
			{
				lock (this.locker)
				{
					if (this.log == null)
					{
						this.SetLogger();
					}
				}
			}

			switch (type)
			{
				case TypeMessage.Trace:
					this.log.Trace(new LogMessageGenerator(() => message));
					break;
				case TypeMessage.Info:
					this.log.Info(new LogMessageGenerator(() => message));
					break;
				case TypeMessage.Debug:
					this.log.Debug(new LogMessageGenerator(() => message));
					break;
				case TypeMessage.Warning:
					this.log.Warn(new LogMessageGenerator(() => message));
					break;
				case TypeMessage.Error:
					this.log.Error(new LogMessageGenerator(() => message));
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Writes log with the exception.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception.</param>
		public void WriteException(string message, Exception exception)
		{
			if (this.log == null)
			{
				lock (this.locker)
				{
					if (this.log == null)
					{
						this.SetLogger();
					}
				}
			}

			this.log.ErrorException(message, exception);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Sets the logger.
		/// </summary>
		private void SetLogger()
		{
			if (string.IsNullOrEmpty(this.loggerName))
			{
				this.log = LogManager.GetLogger(typeof(TEntity).ToString());
			}
			else
			{
				this.log = LogManager.GetLogger(this.loggerName);
			}
		}

		#endregion
	}
}
