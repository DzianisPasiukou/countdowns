
//Convert to javascript normal date.
function convertDate(date) {
	return 'Years: ' + (date.getFullYear() - 1970) + ' Months: ' + (date.getMonth()) + ' Days: ' + (date.getDate() - 1) + ' Hours : ' + (date.getHours() - 6) +
		' Minutes: ' + date.getMinutes() + ' Seconds: ' + date.getSeconds();
}

//Diffirence beetween two date.
function timeDifference(laterdate, earlierdate) {
	var difference = laterdate.getTime() - earlierdate.getTime();

	var daysDifference = Math.floor(difference / 1000 / 60 / 60 / 24);
	difference -= daysDifference * 1000 * 60 * 60 * 24

	var hoursDifference = Math.floor(difference / 1000 / 60 / 60);
	difference -= hoursDifference * 1000 * 60 * 60

	var minutesDifference = Math.floor(difference / 1000 / 60);
	difference -= minutesDifference * 1000 * 60

	var secondsDifference = Math.floor(difference / 1000);



	if (daysDifference == 0) {
		return hoursDifference + ' : ' + convert(minutesDifference) + ' : ' + convert(secondsDifference);
	}

	else {
		return daysDifference + ' day/s \n ' + hoursDifference + ' : ' + convert(minutesDifference) + ' : ' + convert(secondsDifference);
	}
}

function convert(value) {

	if (value < 10) {
		return '0' + value;
	}
	else {

		return value;
	}
}

