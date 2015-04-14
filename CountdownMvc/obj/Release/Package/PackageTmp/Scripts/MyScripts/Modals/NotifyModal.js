myApp.controller('notifyModal', ['$scope', '$modalInstance', 'countdownService', 'countdowns', 'deletingItem', function ($scope, $modalInstance, countdownService, countdowns, deletingItem) {

	$scope.countdown = deletingItem;

	$scope.delete = function () {

		countdownService.delete(deletingItem).success(function (data) {

			$.removeCookie('reminder' + deletingItem.Reminder.Id + ' left');

			$.removeCookie('reminder' + deletingItem.Reminder.Id + ' top');

			countdowns.splice(findIndex(countdowns, deletingItem), 1);

			$modalInstance.dismiss('close');

		}).error(function (data) {

			$modalInstance.dismiss('close');

			$('#errorModal').modal();
			$("#errorModalBody").text("Sorry, getting data ended with error.");
		})
	}

	$scope.close = function () {
		$modalInstance.dismiss('close');
	};
}])