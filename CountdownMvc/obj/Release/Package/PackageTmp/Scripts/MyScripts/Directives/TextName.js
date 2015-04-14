
//Directive for set up length strings.
myApp.directive('textName', function () {

	return function ($scope, element, attrs) {

		$scope.$watch(attrs.textName, function (value) {

			if (!attrs.textLength) {
				attrs.textLength = 11;
			}

			var text = value;

			if (value.length > attrs.textLength) {

				var text = value.slice(0, attrs.textLength);
				text = text + '...';
			}


			element.text(text);
		});
	}
});

//Directive for set up text.
myApp.directive('myText', function () {

	return function ($scope, element, attrs) {


		$scope.$watch(attrs.myText, function (value) {

			var counter = 27;
			var isPopover = false;

			if ((attrs.popover) && (value)) {

				attrs.popover = '';
				isPopover = true;
			}

			for (var i = 0; i < value.length; i++) {

				if (isPopover) {

					attrs.popover = (attrs.popover.toString() + value[i]).toString();

					if (i == counter) {
						attrs.popover = (attrs.popover.toString() + '\n').toString();

						counter += 27;
					}
				}

				else {

					text = element.text() + value[i];

					if (i == counter) {
						var text = (text + '\n').toString();

						counter += 27;
					}

					element.text(text);
				}

			}
		});

	};
});

//Directive for set up text.
myApp.directive('headingText', function () {

	return function ($scope, element, attrs) {

		$scope.$watch(attrs.headingText, function (value) {

			if (value.length > 50) {
				attrs.heading = value.splice(0, 50);

				attrs.heading += '...';
			}

		});

	};
});
