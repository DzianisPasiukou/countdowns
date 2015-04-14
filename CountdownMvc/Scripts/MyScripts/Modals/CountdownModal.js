myApp.controller('countdownModal', ['$scope', '$modalInstance', 'countdownService', 'countdowns', 'countdown', function ($scope, $modalInstance, countdownService, countdowns, countdown) {

	//When modal open.
	function opened() {

		$scope.countdown = countdown;

		if (countdown.Reminder.CountdownSettings) {

			countdown.tempData = countdown.Reminder.CountdownSettings.Start;

			countdown.selectedDays = [];

			for (var i = 0; i < countdown.Reminder.CountdownSettings.Days.length; i++) {

				countdown.selectedDays.push(countdown.Reminder.CountdownSettings.Days[i].Number);
			}

			countdown.selectedWeekly = [];
			countdown.selectedMonthly = [];

			for (var i = 0; i < countdown.Reminder.CountdownSettings.Weeks.length; i++) {

				if (countdown.Reminder.CountdownSettings.Weeks[i].Number == 0) {

					countdown.selectedWeekly.push(0);
				}
			}

			for (var i = 0; i < countdown.Reminder.CountdownSettings.Months.length; i++) {

				if (countdown.Reminder.CountdownSettings.Months[i].Number == 0) {

					countdown.selectedMonthly.push(0);
				}
			}
		}
		else {

			countdown.tempData = countdown.Reminder.ProgressSettings.Start;
		}

		$scope.countdownTemp = jQuery.extend(true, {}, countdown);
	}

	opened();

	//Function for get to init countdown.
	function getCountdown(countdown) {
		countdownService.getCountdown(countdown.Reminder.Id).success(function (data) {

			initCountdown(countdowns, data);

			$modalInstance.dismiss('close');

		}).error(function (data) {

			$modalInstance.dismiss('close');

			$('#errorModal').modal();
			$("#errorModalBody").text("Sorry, getting data ended with error.");
		});
	}

	//Activate specific countdown now.
	$scope.activateNow = function (countdown) {

		countdownService.activateReminder(countdown.Reminder.Id).success(function (data) {

			initCountdown(countdowns, data);

			$modalInstance.dismiss('close');

		}).error(function (data) {

			$modalInstance.dismiss('close');

			$('#errorModal').modal();
			$("#errorModalBody").text("Sorry, activating reminder ended with error.");
		});
	}

	//Postpone specific countdown on time.
	$scope.postPone = function (countdown, time) {

		if (time <= countdown.Progress) {
			countdownService.postPone(countdown.Reminder.Id, time).success(function (data) {

				initCountdown(countdowns, data);

				$modalInstance.dismiss('close');

			}).error(function (data) {

				$modalInstance.dismiss('close');

				$('#errorModal').modal();
				$("#errorModalBody").text("Sorry, postponing reminder ended with error.");
			});
		}
	}

	//Update specific countdown.
	$scope.update = function (countdown) {

		selectedRepeat(countdown);

		if (countdown.Reminder.CountdownSettings) {

			countdown.Reminder.CountdownSettings.Start = new Date(countdown.tempData).toJSON();
		}
		if (countdown.Reminder.ProgressSettings) {

			countdown.Reminder.ProgressSettings.Start = new Date(countdown.tempData).toJSON();
		}

		countdownService.update(countdown).success(function (data) {

			initCountdown(countdowns, data);
			$scope.countdownTemp = data;

			$modalInstance.dismiss('close');

		}).error(function (data) {

			$modalInstance.dismiss('close');

			$('#errorModal').modal();
			$("#errorModalBody").text("Sorry, updating reminder ended with error.");

		});
	}

	//Change pospone time.
	$scope.changePostponeTime = function (time, countdown) {

		if (!time) {
			countdown.postPoneDisabled = true;
			return;
		}

		if (time < 1) {
			countdown.postPoneDisabled = true;
			return;
		}
		countdown.postPoneDisabled = false;

	}

	//Validate
	$scope.validate = function (countdown) {

		for (var i = 0; i < countdown.Reminder.ReminderSettings.length; i++) {

			if (!countdown.Reminder.ReminderSettings[i].Value) {
				return true;
			}
		}

		for (var i = 0; i < countdown.Reminder.Exercises.length; i++) {

			if (!countdown.Reminder.Exercises[i].Name) {
				return true;
			}

			for (var j = 0; j < countdown.Reminder.Exercises[i].Files.length; j++) {

				if (!countdown.Reminder.Exercises[i].Files[j].Name) {
					return true;
				}

			}
		}

		return false;
	}

	//Close the modal.
	$scope.close = function () {

		initCountdown(countdowns, $scope.countdownTemp);

		$modalInstance.dismiss('close');
	};
}]);