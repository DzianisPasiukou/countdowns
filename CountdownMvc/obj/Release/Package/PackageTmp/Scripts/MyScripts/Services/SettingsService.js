myApp.service("settingsService", ['$http', '$location',function ($http, $location) {

	//GET settings from web api.
	this.getSettings = function () {

		var url = $location.absUrl() + "/api/SettingsApi";
		return $http.get(url);
	}
}]);