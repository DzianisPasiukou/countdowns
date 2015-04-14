namespace UnitTestsOfCountdown.Tests.BLL
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using CountdownBusinessLogic;
	using CountdownBusinessLogic.Manager;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Transfer;

	/// <summary>
	/// The summary for unit test from initialize.
	/// </summary>
	[TestClass]
	public class InitializerTest
	{
		#region Public Methods

		/// <summary>
		/// Tests the start progress.
		/// </summary>
		[TestMethod]
		public void StartProgress_Test1()
		{
			DateTime start = new DateTime(2000, 1, 1, 14, 1, 30);
			int interval = 60;
			int duration = 0;

			DateTime newStart = Initializer.StartProgress(start, interval, duration);

			DateTime expectedStart = start;

			while (true)
			{
				if (expectedStart.Ticks >= DateTime.Now.Ticks)
				{
					break;
				}
				else
				{
					if (expectedStart.AddMinutes(interval).Ticks >= DateTime.Now.Ticks)
					{
						break;
					}
					else
					{
						expectedStart = expectedStart.AddMinutes(interval);
					}

					if (expectedStart.Ticks >= DateTime.Now.Ticks)
					{
						break;
					}
					else
					{
						expectedStart = expectedStart.AddMinutes(duration);
					}
				}
			}

			Assert.AreEqual(expectedStart, newStart);
		}

		/// <summary>
		/// Tests the start countdown.
		/// </summary>
		[TestMethod]
		public void StartCountdown_DayTest()
		{
			DateTime start = new DateTime(2000, 2, 10);

			List<DaysDto> days = new List<DaysDto>()
			{
				new DaysDto()
				{
					Number = 3
				},
				new DaysDto()
				{
					Number = 1
				},
				new DaysDto()
				{
					Number = 2
				}
			};

			DateTime newStartWithDays = Initializer.StartCountdown(start, days, new List<WeeksDto>(), new List<MonthsDto>());
			DateTime expectedDateWithDays = start;

			if ((start - DateTime.Now).Ticks <= 0)
			{
				expectedDateWithDays = DateTime.Now;

				int nextDay = 10;
				int currentDay = GetDayOfWeek(DateTime.Now);

				if ((days.Count > 0) && (days.Max(d => d.Number) <= currentDay))
				{
					nextDay = days.Min(d => d.Number);
				}

				for (int i = 0; i < days.Count; i++)
				{
					if ((currentDay < days.ElementAt(i).Number) && (days.ElementAt(i).Number < nextDay))
					{
						nextDay = days.ElementAt(i).Number;
					}

					if (i == days.Count - 1)
					{
						if (nextDay > currentDay)
						{
							expectedDateWithDays = expectedDateWithDays.AddDays(nextDay - currentDay);
						}
						else
						{
							expectedDateWithDays = expectedDateWithDays.AddDays(7 - currentDay + nextDay);
						}
					}
				}
			}

			Assert.AreEqual(expectedDateWithDays.ToShortDateString(), newStartWithDays.ToShortDateString());
		}

		/// <summary>
		/// Testing start countdown with repeat on weekly.
		/// </summary>
		[TestMethod]
		public void StartCountdown_WeekTest()
		{
			DateTime start = new DateTime(2000, 2, 10);

			List<WeeksDto> weeks = new List<WeeksDto>()
			{
				new WeeksDto()
				{
					Number = 0
				}
			};

			DateTime newStartWithweeks = Initializer.StartCountdown(start, new List<DaysDto>(), weeks, new List<MonthsDto>());
			DateTime expectedDateWithWeeks = start;

			if ((start - DateTime.Now).Ticks <= 0)
			{
				int nowDay = GetDayOfWeek(DateTime.Now);
				int startDay = GetDayOfWeek(start);

				expectedDateWithWeeks = DateTime.Now.AddDays((startDay - nowDay) < 0 ? (7 - nowDay + startDay) : (nowDay + startDay - 1));
			}

			Assert.AreEqual(expectedDateWithWeeks.ToShortDateString(), newStartWithweeks.ToShortDateString());
		}

		/// <summary>
		/// Testing start countdown with repeat on monthly.
		/// </summary>
		[TestMethod]
		public void StartCountdown_MonthTest()
		{
			DateTime start = new DateTime(2000, 2, 10);

			List<MonthsDto> months = new List<MonthsDto>()
			{
				new MonthsDto()
				{
					Number = 0
				}
			};

			DateTime newStartWithMonts = Initializer.StartCountdown(start, new List<DaysDto>(), new List<WeeksDto>(), months);
			DateTime expectedDateWithMonths = start;

			if ((start - DateTime.Now).Ticks <= 0)
			{
				expectedDateWithMonths = new DateTime(DateTime.Now.Year, DateTime.Now.Month, start.Day);

				while ((DateTime.Now - expectedDateWithMonths).Ticks >= 0)
				{
					expectedDateWithMonths = expectedDateWithMonths.AddMonths(1);
				}
			}

			Assert.AreEqual(expectedDateWithMonths.ToShortDateString(), newStartWithMonts.ToShortDateString());
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Checks the current day of the week.
		/// </summary>
		/// <param name="start">The start date.</param>
		/// <returns>The number of the day</returns>
		private static int GetDayOfWeek(DateTime start)
		{
			if (start.DayOfWeek == 0)
			{
				return 7;
			}
			else
			{
				return (int)start.DayOfWeek;
			}
		}

		/// <summary>
		/// Combinations the of days.
		/// </summary>
		/// <returns>The collection of days data transfer objects.</returns>
		private IEnumerable<ICollection<DaysDto>> CombinationOfDays()
		{
			List<DaysDto> days = new List<DaysDto>();

			for (int i = 0; i < 8; i++)
			{
				days.Add(new DaysDto()
					{
						Number = i
					});
			}

			List<DaysDto> combination = new List<DaysDto>();

			for (int k = 0; k < days.Count; k++)
			{
				combination = new List<DaysDto>();

				for (int i = k; i < days.Count; i++)
				{
					combination.Add(days[i]);

					for (int j = i; j < days.Count; j++)
					{
						combination[i] = days[j];

						yield return combination;
					}

					combination[i] = days[i];
				}
			}
		}

		#endregion
	}
}
