myApp.directive('onFinishRender', ['$timeout', function ($timeout) {
	return {
		restrict: 'A',
		scope:true,
		link: function (scope, element, attr) {
			if (scope.$last === true) {
				$timeout(function () {
					scope.$emit(attr.onFinishRender);
				});
			}

			scope.$watch(scope.countdown, function (value) {
				$timeout(function () {
					scope.$emit(attr.onFinishRender);
				});
			})

			scope.$watch(scope.countdowns, function (value) {
				$timeout(function () {
					scope.$emit(attr.position);
				});
			})
		}
	}
}]);
