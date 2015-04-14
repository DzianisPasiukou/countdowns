
function toDraggable(countdowns) {

	for (var i = 0; i < countdowns.length; i++) {

		$("#tile" + countdowns[i].Reminder.Id).draggable({
			drag: function (event, ui) {

				var idReminder = ui.helper.context.id.substr(4);

				var countdown = findCountdown(idReminder, countdowns);

				if (!countdown.style) {
					countdown.style = {};
				}

				if (!countdown.style.tile) {
					countdown.style.tile = {};
				}

				countdown.style.tile.left = ui.position.left;

				countdown.style.tile.top = ui.position.top;

				$.cookie('reminder' + idReminder + ' top', ui.position.top.toString(), { expires: 1000 });

				$.cookie('reminder' + idReminder + ' left', ui.position.left.toString(), { expires: 1000 });
			},
			containment: "#countdownsMain"
		});
	}
}

function getPositions(countdowns) {

	for (var i = 0; i < countdowns.length; i++) {

		if (!countdowns[i].style) {
			countdowns[i].style = {};
		}

		if ($.cookie('reminder' + countdowns[i].Reminder.Id + ' left')) {

			if (!countdowns[i].style.tile) {
				countdowns[i].style.tile = {};
			}

			countdowns[i].style.tile.left = $.cookie('reminder' + countdowns[i].Reminder.Id + ' left') + 'px';
		}

		if ($.cookie('reminder' + countdowns[i].Reminder.Id + ' top')) {

			if (!countdowns[i].style.tile) {
				countdowns[i].style.tile = {};
			}

			countdowns[i].style.tile.top = $.cookie('reminder' + countdowns[i].Reminder.Id + ' top') + 'px';
		}
	}
}

//Find index of countdown.
function findIndex(countdowns, countdown) {

	for (var i = 0; i < countdowns.length; i++) {

		if (countdowns[i].Reminder.Id == countdown.Reminder.Id) {
			return i;
		}
	}
}

//Re init selected repeat
function selectedRepeat(countdown) {

	if (countdown.Reminder.CountdownSettings) {

		countdown.Reminder.CountdownSettings.Days = [];
		countdown.Reminder.CountdownSettings.Weeks = [];
		countdown.Reminder.CountdownSettings.Months = [];

		for (var i = 0; i < countdown.selectedDays.length; i++) {

			countdown.Reminder.CountdownSettings.Days.push(
				{
					Name: countdown.selectedDays[i],
					Number: countdown.selectedDays[i]
				})
		}
		for (var i = 0; i < countdown.selectedWeekly.length; i++) {

			countdown.Reminder.CountdownSettings.Weeks.push(
				{
					Name: countdown.selectedWeekly[i],
					Number: countdown.selectedWeekly[i]
				})
		}
		for (var i = 0; i < countdown.selectedMonthly.length; i++) {

			countdown.Reminder.CountdownSettings.Months.push(
				{
					Name: countdown.selectedMonthly[i],
					Number: countdown.selectedMonthly[i]
				})
		}
	}
}

//Check repeat of countdown.
function checkRepeat(countdown) {

	if (countdown.Reminder.CountdownSettings.Days.length > 0) {

		return true;
	}
	if (countdown.Reminder.CountdownSettings.Weeks.length > 0) {

		return true;
	}
	if (countdown.Reminder.CountdownSettings.Months.length > 0) {

		return true;
	}

	return false;
}


//Check countdown duration.
function checkCountdownDuration(countdown) {

	var prevStart = new Date(countdown.Reminder.CountdownSettings.Start);

	if (countdown.Reminder.CountdownSettings.Days.length != 0) {

		var prevDay = countdown.Reminder.CountdownSettings.Days[0].Number;

		if (prevStart.getDay() == countdown.Reminder.CountdownSettings.Days.slice(0).sort(function (a, b) { return b.Number - a.Number })[0].Number) {

			prevDay = countdown.Reminder.CountdownSettings.Days.slice(0).sort(function (a, b) { return a.Number - b.Number })[0].Number;

			prevStart.setDate(prevStart.getDate() - (7 - prevDay + prevStart.getDay()));
		}

		else {
			for (var i = 0; i < countdown.Reminder.CountdownSettings.Days.length; i++) {

				if (countdown.Reminder.CountdownSettings.Days[i].Number > prevStart.getDay()) {
					break;
				}

				prevDay = countdown.Reminder.CountdownSettings.Days[i].Number;
			}

			prevStart.setDate(prevStart.getDate() - (7 - prevDay + prevStart.getDay()));
		}
	}
	else {
		if (countdown.Reminder.CountdownSettings.Weeks.length != 0) {

			prevStart.setDate(prevStart.getDate() - 7);
		}
		else if (countdown.Reminder.CountdownSettings.Months.length != 0) {

			prevStart.setMonth(prevStart.getMonth() - 1);
		}
	}

	var nowDuration = prevStart;
	var now = new Date();

	nowDuration.setMinutes(nowDuration.getMinutes() + countdown.Reminder.CountdownSettings.Duration);

	var diff = nowDuration.getTime() - now.getTime();

	if (diff > 0) {

		if ((Math.round(((countdown.Reminder.CountdownSettings.Duration - Math.round((diff / (1000 * 60) * 100)) / 100) / countdown.Reminder.CountdownSettings.Duration) * 100) / 100).toFixed(2) > 0) {

			countdown.selected = 'selected';
		}
		else {
			countdown.selected = '';
		}

		return (Math.round(((countdown.Reminder.CountdownSettings.Duration - Math.round((diff / (1000 * 60) * 100)) / 100) / countdown.Reminder.CountdownSettings.Duration) * 100) / 100).toFixed(2);
	}
	else {

		countdown.selected = '';
		return -1;
	}
}

//Get progresses of countdowns function.
function getProgresses(countdowns) {

	for (var i = 0; i < countdowns.length; i++) {

		if (countdowns[i].Reminder.TypeOfReminder.Id == 1) {

			countdowns[i]["CountdownEnabled"] = true;
			countdowns[i]["ProgressEnabled"] = false;

			var start = new Date(countdowns[i].Reminder.CountdownSettings.Start);

			var now = new Date();
			var diff = now.getTime() - start.getTime();

			if ((diff > 0) && (checkRepeat(countdowns[i]))) {

				getCountdown(countdowns[i]);
			}

			if (diff < 0) {
				var stay = timeDifference(start, now);

				countdowns[i]["Stay"] = stay;

			}
			else {

				countdowns[i]["Stay"] = "Done";
			}

			countdowns[i]["CountdownProgress"] = checkCountdownDuration(countdowns[i]);
		}
		else {

			countdowns[i]["CountdownEnabled"] = false;
			countdowns[i]["ProgressEnabled"] = true;
			countdowns[i]["postPoneDisabled"] = true;
			countdowns[i]["ActivateDisabled"] = false;

			var start = new Date(countdowns[i].Reminder.ProgressSettings.Start);

			countdowns[i]["Progress"] = 100;
			countdowns[i]["ProgressDuration"] = 100;

			var now = new Date();

			countdowns[i].selected = 'selected';


			if (start.getTime() < now.getTime()) {

				countdowns[i].selected = '';

				var diff = now.getTime() - start.getTime();

				if (countdowns[i].Reminder.ProgressSettings.Interval != 0) {
					countdowns[i]["Progress"] = (Math.round(((diff / (1000 * 60)) / countdowns[i].Reminder.ProgressSettings.Interval) * 100));
				}
				else {
					countdowns[i]["Progress"] = 100;
				}

				if (countdowns[i]["Progress"] >= 100) {

					countdowns[i]["ActivateDisabled"] = true;
					var newStart = new Date(countdowns[i].Reminder.ProgressSettings.Start);

					newStart.setMinutes(newStart.getMinutes() + countdowns[i].Reminder.ProgressSettings.Duration + countdowns[i].Reminder.ProgressSettings.Interval);
					countdowns[i].Reminder.ProgressSettings.Start = newStart.toJSON();
				}
			}

			else if ((start.getTime() - now.getTime()) > 60000 * (countdowns[i].Reminder.ProgressSettings.Duration + countdowns[i].Reminder.ProgressSettings.Interval)) {

				var start = new Date(countdowns[i].Reminder.ProgressSettings.Start);

				var now = new Date();
				var diff = now.getTime() - start.getTime();

				var stay = timeDifference(start, now);

				countdowns[i]["Stay"] = stay;

				countdowns[i].selected = '';
			}
			else {

				countdowns[i]["ActivateDisabled"] = true;

				var startReminder = start;
				startReminder.setMinutes(startReminder.getMinutes() - countdowns[i].Reminder.ProgressSettings.Duration);

				var nowProgress = new Date();

				var diff = nowProgress.getTime() - startReminder.getTime();

				if (countdowns[i].Reminder.ProgressSettings.Duration != 0) {
					countdowns[i]["ProgressDuration"] = (Math.round((((diff / (1000 * 60))) / countdowns[i].Reminder.ProgressSettings.Duration) * 100));
				}
				else {
					countdowns[i]["ProgressDuration"] = 100;
				}

			}
		}
	}
}

//Re initialize countdown function.
function initCountdown(countdowns, countdown) {

	for (var i = 0; i < countdowns.length; i++) {

		if (countdowns[i].Reminder.Id == countdown.Reminder.Id) {

			countdowns[i] = countdown;

			break;
		}
	};

	getProgresses(countdowns);
}

function findCountdown(idReminder, countdowns) {

	for (var i = 0; i < countdowns.length; i++) {

		if (countdowns[i].Reminder.Id == idReminder) {
			return countdowns[i];
		}
	}
}
