myApp.controller('countdownController', ['$scope', '$interval', '$modal', '$location', 'countdownService', function ($scope, $interval, $modal, $location, countdownService) {

	//Themes.
	$scope.themes = {
		Blue: { name: 'Blue', src: $location.absUrl() + '/Content/blue.jpg' },
		Dark: { name: 'Dark', src: $location.absUrl() + '/Content/black-background_00313351.jpg' },
		White: { name: 'White', src: '' }
	};

	//Images.
	$scope.src = {
		Deleted: $location.absUrl() + '/Resources/deletered.ico',
		AddCountdown: $location.absUrl() + '/Resources/Screenshot.173977.100000.jpg',
		AddProgress: $location.absUrl() + '/Resources/semi_success.ico',
		Recycler: $location.absUrl() + '/Resources/Itzikgur-My-Seven-Recycle-Bin-full.ico',
		Settings: $location.absUrl() + '/Resources/advancedsettings.ico'
	};

	//Get theme from cookie.
	function getTheme() {

		if (!$.cookie('theme')) {
			$.cookie('theme', $scope.themes.Blue.src, { expires: 1000 });
			$scope.currentTheme = $scope.themes.src;
		}
		else {
			$scope.currentTheme = $.cookie('theme');
		}
	}

	//Change current theme.
	$scope.changeTheme = function (theme) {

		$.removeCookie('theme');
		$.cookie('theme', theme.src, { expires: 1000 });
		$scope.currentTheme = theme.src;
	}

	getTheme();

	//Object with all countdowns.
	$scope.countdowns = [];

	//Tiles.
	$scope.tiles = [["double", "triple"],
		["bg-olive"]];

	//Get countdowns function for initialize countdowns.
	function getCountdowns() {
		countdownService.getCountdowns().success(function (data) {

			$scope.countdowns = data;

			getProgresses($scope.countdowns);

			//Set up draggable.
			$scope.$on('dragg', function (ngRepeatFinishedEvent) {

				toDraggable($scope.countdowns);
			});

			//Set up positions.
			$scope.$on('positions', function (ngRepeatFinishedEvent) {

				getPositions($scope.countdowns);
			});

		}).error(function (data) {

			$('#errorModal').modal();
			$("#errorModalBody").text("Sorry, getting data ended with error.");
		});
	}

	getCountdowns();

	//Get different tiles.
	function getTiles(countdowns) {

		for (var i = 0; i < countdowns.length; i++) {

			var size = Math.floor((Math.random() * $scope.tiles[0].length));

			//	var transform = Math.floor((Math.random() * $scope.tiles[1].length));

			var color = Math.floor((Math.random() * $scope.tiles[1].length));

			countdowns[i].tile = "tile " + $scope.tiles[0][size] + " " + $scope.tiles[1][color];
		}
	}

	//Get info for countdown create cope of this countdown.
	$scope.info = function (countdown, size) {

		var modalInstance = $modal.open({
			templateUrl: 'countdownModal.html',
			controller: 'countdownModal',
			size: size,
			scope: $scope,
			resolve: {
				countdowns: function () {
					return $scope.countdowns;
				},
				countdown: function () {
					return countdown;
				}
			}
		});

		modalInstance.result.then(function (selectedItem) {
		}, function () {
			getPositions($scope.countdowns);
		});
	}

	//Temp countdown for create a new countdown.
	$scope.tempCountdown = {
		Reminder: {
			Name: '',
			Description: '',
			CountdownSettings: {
				Start: new Date(),
				Duration: 0,
				Days: [],
				Weeks: [],
				Months: []
			},
			TypeOfReminder: {
				Id: 1
			}
		},
		selectedDays: [],
		selectedWeekly: [],
		selectedMonthly: []
	};

	$scope.tempCountdown.Reminder.CountdownSettings.Start.setMinutes(0);
	$scope.tempCountdown.Reminder.CountdownSettings.Start.setHours($scope.tempCountdown.Reminder.CountdownSettings.Start.getHours() + 1);

	//Temp progress for create a new progress.
	$scope.tempProgress = {
		Reminder: {
			Name: '',
			Description: '',
			ProgressSettings: {
				Start: new Date(),
				Duration: 0,
				Interval: 0
			},
			TypeOfReminder: {
				Id: 2
			}
		}
	};

	//Create a new countdown.
	$scope.create = function (countdown) {

		if (countdown.Reminder.CountdownSettings) {

			countdown.Reminder.CountdownSettings.Start.setSeconds(new Date().getSeconds());
		}

		if (countdown.Reminder.ProgressSettings) {

			countdown.Reminder.ProgressSettings.Start.setSeconds(new Date().getSeconds());
		}


		selectedRepeat(countdown);

		countdownService.post(countdown).success(function (data) {

			$scope.countdowns.push(data);

			$scope.closeAdding(countdown);

			getPositions($scope.countdowns);

		}).error(function (data) {

			$('#errorModal').modal();
			$("#errorModalBody").text("Sorry, creating reminder ended with error.");
		});
	}

	//When user close adding modal.
	$scope.closeAdding = function (countdown) {

		if (countdown.Reminder.TypeOfReminder.Id == 1) {
			$scope.tempCountdown = {
				Reminder: {
					Name: '',
					Description: '',
					CountdownSettings: {
						Start: new Date(),
						Duration: 0,
						Days: [],
						Weeks: [],
						Months: []
					},
					TypeOfReminder: {
						Id: 1
					}
				},
				selectedDays: [],
				selectedWeekly: [],
				selectedMonthly: []
			};

			$scope.tempCountdown.Reminder.CountdownSettings.Start.setMinutes(0);
			$scope.tempCountdown.Reminder.CountdownSettings.Start.setHours($scope.tempCountdown.Reminder.CountdownSettings.Start.getHours() + 1);

			$scope.addCountdown.$setPristine();
		}
		else if (countdown.Reminder.TypeOfReminder.Id == 2) {

			$scope.tempProgress = {
				Reminder: {
					Name: '',
					Description: '',
					ProgressSettings: {
						Start: new Date(),
						Duration: 0,
						Interval: 0
					},
					TypeOfReminder: {
						Id: 2
					}
				}
			};

			$scope.addProgress.$setPristine();
		}
	}

	//Find the settings of countdowns for view.
	$scope.findSettings = function (countdown) {

		if (!countdown.style) {
			countdown.style = {};
		}


		for (var i = 0; i < countdown.Reminder.ReminderSettings.length; i++) {

			if (!countdown.style[countdown.Reminder.ReminderSettings[i].NameElement]) {

				Object.defineProperty(countdown.style, countdown.Reminder.ReminderSettings[i].NameElement,
                   {
                   	value: {},
                   	writable: true,
                   	enumerable: true,
                   	configurable: true
                   });
			}
			Object.defineProperty(countdown.style[countdown.Reminder.ReminderSettings[i].NameElement], countdown.Reminder.ReminderSettings[i].NameProperty,
				{
					value: countdown.Reminder.ReminderSettings[i].Value,
					writable: true,
					enumerable: true,
					configurable: true
				})
		}
	}

	//When drop completed.
	$scope.onDropComplete = function (index, obj, evt) {

		var otherObj = $scope.countdowns[index];
		var otherIndex = $scope.countdowns.indexOf(obj);
		$scope.countdowns[index] = obj;
		$scope.countdowns[otherIndex] = otherObj;

	}

	//Refresh progress state for countdown.
	$interval(function () {

		getProgresses($scope.countdowns);
	}, 1000);

	//Sort reminders.
	$scope.sort = function () {

		for (var i = 0; i < $scope.countdowns.length; i++) {

			$.cookie('reminder' + $scope.countdowns[i].Reminder.Id + ' left', 0);

			$.cookie('reminder' + $scope.countdowns[i].Reminder.Id + ' top', 0);
		}

		toDraggable($scope.countdowns);
		getPositions($scope.countdowns);
	}

	//Send to user notifications from modal.
	$scope.notify = function (countdown, size) {

		var modalInstance = $modal.open({
			templateUrl: 'notify.html',
			controller: 'notifyModal',
			size: size,
			resolve: {
				countdowns: function () {
					return $scope.countdowns;
				},
				deletingItem: function () {
					return countdown;
				}
			}
		});

		modalInstance.result.then(function (selectedItem) {
		}, function () {
		});
	}
}]);