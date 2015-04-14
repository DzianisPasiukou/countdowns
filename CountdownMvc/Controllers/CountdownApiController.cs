/// <summary>
/// Controllers for MVC application from countdown.
/// </summary>
namespace CountdownMvc.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Web.Http;

	using CountdownBusinessLogic;

	using LoggingManager;

	/// <summary>
	/// Web API controller for countdowns.
	/// </summary>
	[Authorize]
	public class CountdownApiController : ApiController
	{
		#region Private Fields

		/// <summary>
		/// Get countdowns from current user. Get countdown by id.
		/// </summary>
		private ICountdownCollection countdowns;

		/// <summary>
		/// The logger.
		/// </summary>
		private ILogger<CountdownApiController> logger;

		#endregion

		#region Public Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="CountdownApiController" /> class.
		/// Web API constructor for resolve interfaces from unity container.
		/// </summary>
		/// <param name="countdown">Resolve from CountdownFactory</param>
		/// <param name="logger">The logger.</param>
		/// <exception cref="System.ArgumentNullException">ICountdown is null!</exception>
		public CountdownApiController(ICountdownCollection countdown, ILogger<CountdownApiController> logger)
		{
			if (countdown == null)
			{
				throw new ArgumentNullException("countdown", "ICountdownCollection is null.");
			}

			this.countdowns = countdown;

			if (logger == null)
			{
				throw new ArgumentNullException("logger", "ILogger is null.");
			}

			this.logger = logger;

			this.logger.UserName = User.Identity.Name;

			this.countdowns.UserName = User.Identity.Name;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Get collection of countdowns.
		/// </summary>
		/// <returns>IEnumerable of Countdown</returns>
		public IEnumerable<Countdown> Get()
		{
			IEnumerable<Countdown> countdowns;

			try
			{
				countdowns = this.countdowns.Countdowns;
			}
			catch (Exception e)
			{
				this.logger.WriteException(
					string.Format(CultureInfo.InvariantCulture, "Error with getting countdowns."), e);
				throw;
			}

			this.logger.Write(
				string.Format(
				CultureInfo.InvariantCulture,
				"Gets the countdowns. \n Return value is \n {0}",
				MySerialization.SerializeToJSON(countdowns)),
				TypeMessage.Info);

			return countdowns;
		}

		/// <summary>
		/// Gets the specified Countdown.
		/// </summary>
		/// <param name="id">The id of countdown.</param>
		/// <returns>Countdown object</returns>
		public Countdown Get(int id)
		{
			Countdown current;

			try
			{
				current = this.countdowns.GetCountdownById(id);
			}
			catch (Exception e)
			{
				this.logger.WriteException(
					string.Format(CultureInfo.InvariantCulture, "Error with getting countdown of id: {0}.", id), e);
				throw;
			}

			this.logger.Write(
				string.Format(
				CultureInfo.InvariantCulture,
				"Get the specific countdown by id {0}. \n Return value is \n {1}",
				id,
				MySerialization.SerializeToJSON(current)),
				TypeMessage.Info);

			return current;
		}

		/// <summary>
		/// Posts the add countdown.
		/// </summary>
		/// <param name="countdown">The specific countdown.</param>
		/// <returns>
		/// The countdown.
		/// </returns>
		public Countdown PostAddCountdown(Countdown countdown)
		{
			this.logger.Write(
				string.Format(CultureInfo.InvariantCulture, "Adding countdown. \n Post value is \n {0}", MySerialization.SerializeToJSON(countdown)),
				TypeMessage.Info);

			if (countdown != null)
			{
				countdown.Reminder.UserName = User.Identity.Name;

				try
				{
					this.countdowns.Create(countdown);
				}
				catch (Exception e)
				{
					this.logger.WriteException(
						string.Format(CultureInfo.InvariantCulture, "Error with getting adding countdown: {0}.", MySerialization.SerializeToJSON(countdown)), e);
					throw;
				}
			}

			this.logger.Write(
				string.Format(CultureInfo.InvariantCulture, "Adding countdown. \n After adding value is \n {0}", MySerialization.SerializeToJSON(countdown)),
				TypeMessage.Info);

			return countdown;
		}

		/// <summary>
		/// Puts the update countdown.
		/// </summary>
		/// <param name="countdown">The countdown.</param>
		/// <returns>The reminder data transfer object.</returns>
		public Countdown PutUpdate(Countdown countdown)
		{
			this.logger.Write(
				string.Format(CultureInfo.InvariantCulture, "Update countdown. \n Put value is \n {0}", MySerialization.SerializeToJSON(countdown)),
				TypeMessage.Info);

			if (countdown != null)
			{
				try
				{
					this.countdowns.FullUpdate(countdown);
				}
				catch (Exception e)
				{
					this.logger.WriteException(
						string.Format(CultureInfo.InvariantCulture, "Error in full update. Countdown - {0}", MySerialization.SerializeToJSON(countdown)), e);
					throw;
				}
			}

			this.logger.Write(
				string.Format(CultureInfo.InvariantCulture, "Update countdown. \n After update value is \n {0}", MySerialization.SerializeToJSON(countdown)),
				TypeMessage.Info);

			return countdown;
		}

		/// <summary>
		/// Puts the activate of the reminder.
		/// </summary>
		/// <param name="id">The identifier of the reminder.</param>
		/// <returns>Reminder data transfer object.</returns>
		public Countdown PutActivate(int id)
		{
			Countdown countdown = this.countdowns.GetCountdownById(id);

			this.logger.Write(
				string.Format(
				CultureInfo.InvariantCulture,
				"Activate the countdown by id {0}. \n Put value is \n {1}",
				id,
				MySerialization.SerializeToJSON(countdown)),
				TypeMessage.Info);

			try
			{
				this.countdowns.Activate(countdown);
			}
			catch (Exception e)
			{
				this.logger.WriteException(
					string.Format(CultureInfo.InvariantCulture, "Error in activate countdown: {0}", MySerialization.SerializeToJSON(countdown)), e);
				throw;
			}

			this.logger.Write(
				string.Format(
				CultureInfo.InvariantCulture,
				"Activate the countdown by id {0}. \n After activate value is \n {1}",
				id,
				MySerialization.SerializeToJSON(countdown)),
				TypeMessage.Info);

			return countdown;
		}

		/// <summary>
		/// Puts the postpone of the reminder.
		/// </summary>
		/// <param name="id">The identifier reminder.</param>
		/// <param name="time">The time to postpone.</param>
		/// <returns>Reminder data transfer object.</returns>
		public Countdown PutPostpone(int id, int time)
		{
			Countdown countdown = this.countdowns.GetCountdownById(id);

			this.logger.Write(
				string.Format(
				CultureInfo.InvariantCulture,
				"Postpone the countdown by id {0} with time {1}. \n Put value is \n {2}",
				id,
				time,
				MySerialization.SerializeToJSON(countdown)),
				TypeMessage.Info);

			try
			{
				this.countdowns.Postpone(countdown, time);
			}
			catch (Exception e)
			{
				this.logger.WriteException(
					string.Format(CultureInfo.InvariantCulture, "Error in postpone countdown: {0}", MySerialization.SerializeToJSON(countdown)), e);
				throw;
			}

			this.logger.Write(
				string.Format(
				CultureInfo.InvariantCulture,
				"Postpone the countdown by id {0} with time {1}. \n After pospone value is \n {2}",
				id,
				time,
				MySerialization.SerializeToJSON(countdown)),
				TypeMessage.Info);

			return countdown;
		}

		/// <summary>
		/// Deletes the specified countdown.
		/// </summary>
		/// <param name="id">The identifier of countdown.</param>
		public void Delete(int id)
		{
			Countdown countdown = this.countdowns.GetCountdownById(id);

			this.logger.Write(
				string.Format(
				CultureInfo.InvariantCulture,
				"Delete the countdown by id {0}. \n Deleting value is \n {1}",
				id,
				MySerialization.SerializeToJSON(countdown)),
				TypeMessage.Info);

			try
			{
				this.countdowns.Close(countdown);
			}
			catch (Exception e)
			{
				this.logger.WriteException(
					string.Format(CultureInfo.InvariantCulture, "Error in deleting countdown: {0}", MySerialization.SerializeToJSON(countdown)), e);
				throw;
			}
		}

		#endregion
	}
}