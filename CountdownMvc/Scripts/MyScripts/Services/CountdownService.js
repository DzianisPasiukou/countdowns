myApp.service("countdownService", ['$http', '$location', function ($http, $location) {

	//GET countdowns from web api.
	this.getCountdowns = function () {

		var url = $location.absUrl() + "/api/CountdownApi";
		return $http.get(url);
	}

	//GET countdown from web api.
	this.getCountdown = function (id) {
		var url = $location.absUrl() + "/api/CountdownApi?id=" + id;
		return $http.get(url);
	}

	//PUT countdown state activation from web api.
	this.activateReminder = function (idReminder) {
		var url = $location.absUrl() + "/api/CountdownApi?id=" + idReminder;
		return $http.put(url);
	}

	//PUT to pospone countdown with specific reminder id.
	this.postPone = function (idReminder, time) {
		var url = $location.absUrl() + "/api/CountdownApi?id=" + idReminder + "&time=" + time;
		return $http.put(url);
	}

	//PUT to update countdown.
	this.update = function (countdown) {

		var data = JSON.stringify(countdown);

		return $.ajax({
			url: $location.absUrl() + '/api/CountdownApi',
			cache: false,
			type: 'PUT',
			contentType: 'application/json; charset=utf-8',
			data: data,
			dataType: "json"
		});
	}

	//DELETE specific countdown.
	this.delete = function (countdown) {

		var url = $location.absUrl() + "/api/CountdownApi?id=" + countdown.Reminder.Id;

		return $http.delete(url);
	}

	//POST to add new countdown.
	this.post = function (countdown) {

		var data = JSON.stringify(countdown);

		return $.ajax({
			url: $location.absUrl() + '/api/CountdownApi',
			cache: false,
			type: 'POST',
			contentType: 'application/json; charset=utf-8',
			data: data,
			dataType: "json"
		});
	}
}]);