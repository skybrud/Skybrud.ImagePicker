angular.module('umbraco').controller('Skybrud.ImagePicker.Controller', function ($scope) {

    // Since Umbraco will strip all $ properties from the model on save, we need to create a "shadow" object
    $scope.selected = $scope.model.value ? angular.copy($scope.model.value) : null;

    // And when the "shadow" object changes, we update the model value
    $scope.$watch('selected', function() {
        $scope.model.value = $scope.selected;
    }, true);

});