myApp.controller('repeatController', ['$scope', function ($scope) {

	$scope.tempDays = [{ Name: 'Daily', Number: 0 },
		{ Name: 'Monday', Number: 1 },
		{ Name: 'Tuesday', Number: 2 },
		{ Name: 'Wednesday', Number: 3 },
		{ Name: 'Thursday', Number: 4 },
		{ Name: 'Friday', Number: 5 },
		{ Name: 'Saturday', Number: 6 },
		{ Name: 'Sunday', Number: 7 }];

	$scope.tempWeekly = [{ Name: 'Weekly', Number: 0 }];

	$scope.tempMonthly = [{ Name: 'Monthly', Number: 0 }];

}]);