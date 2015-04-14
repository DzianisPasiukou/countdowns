/// <reference path="../Smoke/animation.js" />
/// <reference path="../Smoke/dat.gui.min.js" />
myApp.controller('fileController', ['$scope', function ($scope) {

	//Temp file for creation a new file.
	$scope.tempFile = {
		Name: '',
		Description: '',
		Images: [],
		opened: false
	};

	$scope.alerts = [];

	//Add new file to exercise.
	$scope.addFile = function (tempFile, exercise, isTempExercise) {

		$scope.alerts = [];

		if (!exercise.Files) {
			exercise.Files = [];
		}

		exercise.Files.push(
			{
				Name: tempFile.Name,
				Description: tempFile.Description,
				Images: tempFile.Images
			});

		$scope.tempFile = {
			Name: '',
			Description: '',
			Images: [],
			opened: false
		};

		if (isTempExercise) {
			$scope.infoCountdown.tempExerciseForm.tempFileForm.$setPristine();
		}
		else {
			$scope.infoCountdown.tempFileForm.$setPristine();
		}
	}

	//Delete existing file from exercise.
	$scope.deleteFile = function (file, exercise) {

		$scope.alerts = [];

		for (var i = 0; i < exercise.Files.length; i++) {

			if (file.Id == exercise.Files[i].Id) {
				exercise.Files.splice(i, 1);
				break;
			}
		}
	}

	//Delete existing image.
	$scope.deleteImage = function (image, file) {

		$scope.alerts = [];

		file.Images.splice(findIndexImage(image, file), 1);
	}

	//Find index of image.
	function findIndexImage(image, file) {

		for (var i = 0; i < file.Images.length; i++) {

			if (image.Data == file.Images[i].Data) {
				return i;
			}
		}
	}

	//Choose image.
	$scope.chooseImage = function (images, file, opt) {

		$scope.alerts = [];

		var image = images[0];

		if (image.type.substr(0, 5) == 'image') {

			var fileReader = new FileReader();

			fileReader.readAsDataURL(image);

			fileReader.onloadend = function () {

				file.Images.push({

					Name: image.name,
					Type: fileReader.result.substr(0, fileReader.result.indexOf(',') + 1),
					Data: fileReader.result.substr(fileReader.result.indexOf(',') + 1)
				});
			}

		}
		else {
			alert('The selected file is not an image', 'danger', opt);
		}
	}

	//Alert to user.
	function alert(message, type, opt) {

		$scope.alerts.push(
			{
				msg: message,
				type: type,
				opt: opt
			})
	}

	//Close the alert by index.
	$scope.closeAlert = function (index) {
		$scope.alerts.splice(index, 1);
	}
}]);