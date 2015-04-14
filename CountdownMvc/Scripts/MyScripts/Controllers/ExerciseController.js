myApp.controller('exerciseController', ['$scope', function ($scope) {

	//Temp exercise for create a new exercise.
	$scope.tempExercise = {
		Name: '',
		Description: '',
		Files: [],
		opened: false
	};

	//Add new exercise to countdown.
	$scope.addExercise = function (tempExercise, countdown) {

		if (!tempExercise.Files) {

			tempExercise.Files = [];
		}

		countdown.Reminder.Exercises.push(
			{
				Name: tempExercise.Name,
				Description: tempExercise.Description,
				Files: tempExercise.Files
			});

		$scope.tempExercise = {
			Name: '',
			Description: '',
			Files: [],
			opened: false
		};

		$scope.infoCountdown.tempExerciseForm.$setPristine();
	}

	//Delete existing exercise from countdown.
	$scope.deleteExercise = function (exercise, countdown) {

		for (var i = 0; i < countdown.Reminder.Exercises.length; i++) {

			if (countdown.Reminder.Exercises[i].Id == exercise.Id) {

				countdown.Reminder.Exercises.splice(i, 1);
				break;
			}
		}
	}
}]);