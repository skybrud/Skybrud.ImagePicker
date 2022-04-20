angular.module("umbraco").controller("Skybrud.ImagePicker.TypeConverter", function ($scope, $http) {

    // Get the base URL for the API controller
    const baseUrl = Umbraco.Sys.ServerVariables.umbracoSettings.umbracoPath;

    const vm = this;

    vm.types = [
        {
            key: "",
            name: "No converter"
        }
    ];

    vm.selected = vm.types[0];

    vm.error = null;

    // Get the query string of the view URL
    const urlParts = $scope.model.view.split("?");
    const urlQuery = urlParts.length === 1 ? "" : urlParts[1];

    // Get the "editor" parameter from the query string
    const editor = new URLSearchParams(urlQuery).get("editor");

    $http.get(`${baseUrl}/backoffice/Skybrud/ImagePicker/GetTypeConverters?editor=${editor}`).then(function (r) {

        vm.types = vm.types.concat(r.data);

        let found = false;

        r.data.forEach(function (t) {

            if ($scope.model.value && t.key === $scope.model.value.key) {
                vm.selected = t;
                found = true;
            }

        });

        if (!found && $scope.model.value && $scope.model.value.key) {
            $scope.error = `An item converter with the key <strong>${$scope.model.value.key}</strong> could not be found.`;
        }

    });

    vm.updated = function () {

        vm.error = null;

        if (!vm.selected.key) {
            $scope.model.value = null;
            return;
        }

        $scope.model.value = {
            key: vm.selected.key
        };

    };

});