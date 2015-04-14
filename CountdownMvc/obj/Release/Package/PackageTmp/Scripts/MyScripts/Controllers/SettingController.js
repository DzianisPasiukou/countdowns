myApp.controller('settingController', ['$scope', 'settingsService', function ($scope, settingsService) {

	//Array with all settings.
	$scope.settings = [];

	//Temp settings for create a new setting to countdown.
	$scope.tempSetting = {};

	//Example fonts.
	$scope.fonts = [
		'Palatino Linotype',
		'Book Antiqua',
		'Georgia',
		'Times New Roman',
		'Arial',
		'Helvetica',
		'Arial Black',
		'Impact',
		'Lucida Sans Unicode',
		'Tahoma',
		'Verdana',
		'Courier New',
		'Lucida Console',
		'Initial'
	];

	$scope.alerts = [];

	//Get all settings.
	function getSettings() {

		settingsService.getSettings().success(function (data) {

			$scope.settings = data;
		}).error(function (data) {

			$('#errorModal').modal();
			$("#errorModalBody").text("Sorry, getting settings end with error.");
		});
	}

	getSettings();

	//Delete existing setting from countdown.
	$scope.deleteSetting = function (setting, countdown) {

		for (var i = 0; i < countdown.Reminder.ReminderSettings.length; i++) {

			if (countdown.Reminder.ReminderSettings[i].Id == setting.Id) {

				delete countdown.style[countdown.Reminder.ReminderSettings[i].NameElement][countdown.Reminder.ReminderSettings[i].NameProperty];

				countdown.Reminder.ReminderSettings.splice(i, 1);

				break;
			}
		}

	}

	//Choose setting from drop down.
	$scope.chooseSetting = function (setting) {

		$scope.tempSetting.Setting = setting;
		$scope.tempSetting.Value = '';
		$scope.alerts = [];

		if (setting.Name.toLowerCase().search("color") != -1) {
			$scope.tempSetting.isColor = true;
			$scope.tempSetting.isFont = false;
		}
		else if (setting.Name.toLowerCase().search("font") != -1) {
			$scope.tempSetting.isFont = true;
			$scope.tempSetting.isColor = false;
		}
		else {

		    $scope.tempSetting.isFont = false;
		    $scope.tempSetting.isColor = false;
		    $('#valueSetting').empty();
		    var element = angular.element(document.querySelector('#valueSetting'));;
		    element.append('Value <input type="text" ng-model="tempSetting.Value" />');
		}
	}

	//Choose setting from drop down.
	$scope.initSetting = function (setting) {

	    if (setting.Name.toLowerCase().search("color") != -1) {
	        setting.isColor = true;
	    }
	    else if (setting.Name.toLowerCase().search("font") != -1) {
	        setting.isFont = true;
	    }
	}

	$scope.changeSetting = function (setting, countdown) {

		countdown.style[setting.NameElement][setting.NameProperty] = setting.Value;
	}

	$scope.selectType = function (countdown, setting, item, model, label) {
		countdown.style[setting.NameElement][setting.NameProperty] = item;
	}

	//Add a new setting to countdown.
	$scope.addSetting = function (tempSetting, countdown) {

		if (tempSetting.Setting) {

			for (var i = 0; i < countdown.Reminder.ReminderSettings.length; i++) {

				if (tempSetting.Setting.Name == countdown.Reminder.ReminderSettings[i].Name) {

					alert("Setting is exists", "danger");

					return;
				}
			}

			countdown.Reminder.ReminderSettings.push(
			{
				Id: tempSetting.Setting.Id,
				Name: tempSetting.Setting.Name,
				Description: tempSetting.Setting.Description,
				Value: tempSetting.Value,
				NameProperty: tempSetting.Setting.NameProperty,
				NameElement: tempSetting.Setting.NameElement
			});

			if(!countdown.style){
			    countdown.style = {};
			}

			if (!countdown.style[tempSetting.Setting.NameElement]) {

				Object.defineProperty(countdown.style, tempSetting.Setting.NameElement,
                    {
                        value: {},
                        writable: true,
                        enumerable: true,
                        configurable: true
                    })
			}

			Object.defineProperty(countdown.style[tempSetting.Setting.NameElement], tempSetting.Setting.NameProperty,
                    {
                        value: tempSetting.Value,
                        writable: true,
                        enumerable: true,
                        configurable: true
                    })

			$scope.tempSetting = {};
		}
	}

	//Alert with message by type.
	function alert(message, type) {

		$scope.alerts = [];

		$scope.alerts.push(
			{
				msg: message,
				type: type
			})
	}

	//Close the alert by index.
	$scope.closeAlert = function (index) {
		$scope.alerts.splice(index, 1);
	}

}]);