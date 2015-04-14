namespace UnitTestsOfCountdown.Tests.BLL
{
	using System;
	using System.Collections.Generic;

	using CountdownBusinessLogic;
	using CountdownBusinessLogic.Service;

	using CountdownDataBaseLayer;
	using CountdownDataBaseLayer.Repo;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Moq;

	using Transfer;
	using Transfer.SmallTransfer;

	/// <summary>
	/// The instance of UnitTestActions.
	/// </summary>
	[TestClass]
	public class CountdownCollectionTest
	{
		#region Private Fields

		/// <summary>
		/// The time for postpone
		/// </summary>
		private const int TimeForPostpone = 5;

		/// <summary>
		/// The countdown controller.
		/// </summary>
		private ICountdownCollection countdowns;

		/// <summary>
		/// The reminder.
		/// </summary>
		private Countdown countdown;

		/// <summary>
		/// The test context instance.
		/// </summary>
		private TestContext testContextInstance;

		#endregion

		#region Public Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CountdownCollectionTest"/> class.
		/// </summary>
		public CountdownCollectionTest()
		{
			this.countdown = new Countdown()
			{
				Reminder = new ReminderDto()
				{
					Id = 1,
					Name = "test reminder.",
					Description = "testing",
					TypeOfReminder = new TypeOfReminderDto()
					{
						Id = 1,
						Name = "Progress",
						Description = "Progress reminder"
					},
					UserName = "Test/User",
					ProgressSettings = new ProgressSettingsDto()
					{
						Duration = 5,
						Id = 1,
						Interval = 60,
						Start = new DateTime(2000, 1, 1, 1, 1, 1)
					}
				}
			};

			Mock<IClient> mockClient = new Mock<IClient>();
			mockClient.Setup(c => c.NotifyAboutChanges(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<State>()));

			Mock<IRepoFactory> mockRepo = new Mock<IRepoFactory>();

			IRepoFactory factory = new RepositoryFactory();
			FactoryInit(factory);

			this.countdowns = new CountdownCollection(factory, mockClient.Object);
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets or sets the test context which provides
		/// information about and functionality for the current test run.
		/// </summary>
		/// <value>
		/// The test context.
		/// </value>
		public TestContext TestContext
		{
			get
			{
				return this.testContextInstance;
			}

			set
			{
				this.testContextInstance = value;
			}
		}

		#endregion

		#region Additional test attributes

		//// You can use the following additional attributes as you write your tests:
		////
		//// Use ClassInitialize to run code before running the first test in the class
		//// [ClassInitialize()]
		//// public static void MyClassInitialize(TestContext testContext) { }
		////
		//// Use ClassCleanup to run code after all tests in a class have run
		//// [ClassCleanup()]
		//// public static void MyClassCleanup() { }
		////
		//// Use TestInitialize to run code before running each test 
		//// [TestInitialize()]
		//// public void MyTestInitialize() { }
		////
		//// Use TestCleanup to run code after each test has run
		//// [TestCleanup()]
		//// public void MyTestCleanup() { }

		#endregion

		#region Public Methods

		/// <summary>
		/// Tests the activate.
		/// </summary>
		[TestMethod]
		public void Activate_Test1()
		{
			this.countdowns.Activate(this.countdown);

			Assert.AreEqual(DateTime.Now.AddMinutes(this.countdown.Reminder.ProgressSettings.Duration).ToUniversalTime().ToShortTimeString(), this.countdown.Reminder.ProgressSettings.Start.ToShortTimeString());
		}

		/// <summary>
		/// Tests the postpone.
		/// </summary>
		[TestMethod]
		public void Postpone_Test1()
		{
			DateTime oldStart = this.countdown.Reminder.ProgressSettings.Start.ToLocalTime();

			this.countdowns.Postpone(this.countdown, TimeForPostpone);

			if ((oldStart - DateTime.Now).Ticks < 0)
			{
				oldStart = oldStart.AddMinutes(TimeForPostpone);
			}
			else
			{
				oldStart = oldStart.AddMinutes(TimeForPostpone - this.countdown.Reminder.ProgressSettings.Duration - this.countdown.Reminder.ProgressSettings.Interval);
			}

			Assert.AreEqual(oldStart.ToShortTimeString(), this.countdown.Reminder.ProgressSettings.Start.ToLocalTime().ToShortTimeString());
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Factories the initialize.
		/// </summary>
		/// <param name="factory">The factory.</param>
		private static void FactoryInit(IRepoFactory factory)
		{
			factory.GetDaysRepo = MocksRepo<Days>();
			factory.GetExerciseRepo = MocksRepo<Exercises>();
			factory.GetFilesRepo = MocksRepo<Files>();
			factory.GetImagesRepo = MocksRepo<Images>();
			factory.GetMonthRepo = MocksRepo<Monthes>();
			factory.GetProgressSettingsRepo = MocksRepo<ProgressSettings>();
			factory.GetReminderRepo = MocksRepo<Reminder>();
			factory.GetReminderSettingsRepo = MocksRepo<ReminderSettings>();
			factory.GetWeeksRepo = MocksRepo<Weeks>();
		}

		/// <summary>
		/// Mocks the repository.
		/// </summary>
		/// <typeparam name="T">The repository entity.</typeparam>
		/// <returns>The repository.</returns>
		private static IRepo<T> MocksRepo<T>()
			where T : new()
		{
			var mock = new Mock<IRepo<T>>();

			mock.Setup(r => r.Add(It.IsAny<T>()));
			mock.Setup(r => r.Delete(It.IsAny<T>()));
			mock.Setup(r => r.Update(It.IsAny<T>()));
			mock.Setup(r => r.UpdateMany(It.IsAny<IEnumerable<T>>(), It.IsAny<IEnumerable<T>>()));
			mock.Setup(r => r.GetAll()).Returns(new List<T>());
			mock.Setup(r => r.GetById(It.IsAny<int>())).Returns(new T());

			return mock.Object;
		}

		#endregion
	}
}
