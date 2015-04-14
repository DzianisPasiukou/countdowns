logApp.controller('loggerCtrl', ['$scope', function ($scope) {

	$scope.treedata = [];

	$scope.logger = {};

	$scope.chooseFile = function (element) {

		var file = element.files[0];

		$scope.logger.name = file.name;

		if (file.type.substr(0, 4) == 'text') {

			var fileReader = new FileReader();

			fileReader.readAsText(file);

			fileReader.onloadend = function () {

				$scope.$apply(function () {

					$scope.logger.data = splitter(fileReader.result, true);

					$scope.val = $scope.logger.data;

					for (var i = 0; i < $scope.val.length; i++) {
						$scope.val[i].Message = objectSplitter($scope.logger.data[i].Message);
					}

					getTree();
				});
			};
		}
	};

	function getTree() {

		$scope.treedata = initLog($scope.val);
	}

	function splitter(str, isInit) {

		if (!str) {
			return;
		}
		str = str.replace(/(\r\n|\n|\r)/gm, "");

		if (isInit) {
			str = str.substr(1);
		}

		str = str + ']';

		str = str.reverse();

		str = str + '[';

		str = str.reverse();

		var json = JSON.stringify(eval("(" + str + ")"));

		var arr = JSON.parse(json);

		return arr;
	}

	function objectSplitter(str) {

		if (!str) {
			return;
		}

		var val = {};

		val.description = "";

		val.description = str.substr(0, str.indexOf('['));

		val.description = val.description + '...';

		val.description = val.description + str.reverse().substr(0, str.reverse().indexOf(']')).reverse();

		val.object = {};

		val.object = splitter(str.substr(str.indexOf('[', str.reverse().indexOf(']') + 1)));

		return val;
	}

	String.prototype.replaceAll = function (str1, str2, ignore) {
		return this.replace(new RegExp(str1.replace(/([\/\,\!\\\^\$\{\}\[\]\(\)\.\*\+\?\|\<\>\-\&])/g, "\\$&"), (ignore ? "gi" : "g")), (typeof (str2) == "string") ? str2.replace(/\$/g, "$$$$") : str2);
	}

	String.prototype.reverse = function () {
		var s = "";
		var i = this.length;
		while (i > 0) {
			s += this.substring(i - 1, i);
			i--;
		}
		return s;
	}

	function init(value) {

		if (!value) {
			return;
		}

		var child = [];

		for (var prop in value) {

			if (value.hasOwnProperty(prop)) {

				if (Array.isArray(value[prop])) {
					child.push({
						label: prop,
						children: isObject(value[prop]) ? init(value[prop]) : null,
						collapsed: true,
						noLeaf: isObject(value[prop])
					})
				}
				else {
					child.push({
						label: prop,
						children: isObject(value[prop]) ? init(value[prop]) : null,
						collapsed: true,
						noLeaf: isObject(value[prop]),
						description: isObject(value[prop]) ? "" : value[prop]
					})
				}

			}
		};

		return child;
	}

	function initLog(log) {

		if (!log) {
			return;
		}

		var child = [];

		for (var i = 0; i < log.length; i++) {

			if (!log[i]) {
				continue;
			}

			child.push({
				label: log[i].Date,
				collapsed: true,
				children: init(log[i])
			})
		}
		return child;
	}

	function isObject(val) {
		if (val === null) { return false; }
		return ((typeof val === 'function') || (typeof val === 'object'));
	}

	$scope.selectNode = function (branch) {

		$scope.selected = branch;
	}

	$scope.collapse = function () {

		collapseAll($scope.treedata);
	}

	function collapseAll(value) {

		for (var i = 0; i < value.length; i++) {

			value[i].expanded = false;

			collapseAll(value[i].children);
		}
	}
}]);