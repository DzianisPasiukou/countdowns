$(function () {
	var $elem = $('.animation');
	$({ deg: 1 }).animate({ deg: 0 }, {
		duration: 1,
		step: function (angle) {
			$elem.css({
				marginRight: 0
			});
		}
	});
	var $elem1 = $('.animation1');
	$({ deg: 1 }).animate({ deg: 0 }, {
		duration: 1,
		step: function (angle) {
			$elem1.css({
				marginRight: -350
			});
		}
	});

	var flag = false;

	var beginFalse = 360;
	var endFalse = 0;
	var endFalse1 = 0;

	var current = 360;
	var current1 = -350;

	var beginTrue = 0;
	var endTrue = 360;
	var endTrue1 = -350;
	$('.animation').click(function () {

		var $elem = $('.animation');
		var $elem1 = $('.animation1');
		if (!flag) {
			flag = true;
			$({ deg: current }).animate({ deg: endFalse }, {
				duration: 2000,
				step: function (angle) {
					current = angle;
					$elem.css({
						transform: 'rotate(' + angle + 'deg)',
						marginRight: (beginFalse - angle)
					});
				}
			});

			$({ deg: current1 }).animate({ deg: endFalse1 }, {
				duration: 2000,
				step: function (angle) {
					current1 = angle;
					$elem1.css({
						marginRight: angle
					});
				}
			});
		}

		else {
			flag = false;
			$({ deg: current }).animate({ deg: endTrue }, {
				duration: 2000,
				step: function (angle) {
					current = angle;
					$elem.css({
						transform: 'rotate(' + angle + 'deg)',
						marginRight: (endTrue - angle)
					});
				}
			});
			$({ deg: current1 }).animate({ deg: endTrue1 }, {
				duration: 2000,
				step: function (angle) {
					current1 = angle;
					$elem1.css({
						marginRight: angle
					});
				}
			});
		}
	})
});