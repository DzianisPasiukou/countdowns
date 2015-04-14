myApp.directive('myDrag', function () {

	return function ($scope, element, attrs) {

		$scope.$watch(element.css('left'), function (value) {
		});

		$scope.$watch(element.css('top'), function (value) {
		});
	}

});