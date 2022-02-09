angular.module("umbraco").controller("Skybrud.ImagePicker.ImageModelPicker.Controller", function ($scope, $http, editorService) {

    // Get the base URL for the API controller
    const baseUrl = Umbraco.Sys.ServerVariables.umbracoSettings.umbracoPath;

    const vm = this;

    vm.loaded = false;
    vm.models = [];

    // Get the query string of the view URL
    const urlParts = $scope.model.view.split("?");
    const urlQuery = urlParts.length === 1 ? "" : urlParts[1];

    // Get the "editor" parameter from the query string
    const editor = new URLSearchParams(urlQuery).get("editor");

    vm.changed = function () {
        $scope.model.value = vm.selected ? vm.selected.key : "";
    };

    vm.reset = function () {
        vm.selected = null;
        $scope.model.value = "";
        delete vm.notFound;
    };

    vm.add = function () {

        editorService.open({
            title: "Select image model",
            size: "medium",
            view: "/App_Plugins/Skybrud.Umbraco.ImagePicker/Views/ImageModelPickerOverlay.html",
            filter: true,
            availableItems: vm.models,
            submit: function (model) {
                vm.selected = model;
                $scope.model.value = model.key;
                delete vm.notFound;
                editorService.close();
            },
            close: function () {
                editorService.close();
            }
        });

    };

    function init() {

        if (!$scope.model.value) $scope.model.value = "";

        $http.get(`${baseUrl}/backoffice/Skybrud/ImagePicker/GetImageModels?editor=${editor}`).then(function (response) {

            vm.loaded = true;
            vm.models = response.data;

            vm.selected = vm.models.find(x => x.key === $scope.model.value);

            if ($scope.model.value && !vm.selected) {
                const m = $scope.model.value.match(/^([a-zA-Z0-9\\.]+), ([a-zA-Z0-9\\.]+)$/);
                console.log(m);
                if (m) vm.selected = vm.models.find(x => x.key.indexOf(`${$scope.model.value},`) === 0);
                if (!vm.selected) vm.notFound = true;
            }

        });

    }

    init();

});