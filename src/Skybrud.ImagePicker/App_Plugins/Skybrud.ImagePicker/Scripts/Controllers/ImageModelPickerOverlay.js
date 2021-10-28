angular.module("umbraco").controller("Skybrud.ImagePicker.ImageModelPickerOverlay.Controller", function ($scope, $http, editorService) {

    const vm = this;

    vm.close = function () {
        $scope.model.close();
    };

    vm.select = function(model) {
        $scope.model.submit(model);
    };

});