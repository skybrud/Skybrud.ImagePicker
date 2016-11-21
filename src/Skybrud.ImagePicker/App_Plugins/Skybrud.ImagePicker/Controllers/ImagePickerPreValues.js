angular.module("umbraco").controller("Skybrud.ImagePickerPreValues.Controller", function ($scope) {

    if (!$scope.model.value) {
        $scope.model.value = {
            limit: 0
        };
    }

    var cgf = $scope.model.value;

    if (!cgf.title) cgf.title = {};
    if (!cgf.title.show) cgf.title.show = false;
    if (!cgf.title.placeholder) cgf.title.placeholder = '';

    if (!cgf.layout) cgf.layout = {};
    if (!cgf.layout.initial) cgf.layout.initial = 'tiles';
    if (!cgf.layout.hideSelector) cgf.layout.hideSelector = false;

    if (!cgf.image) cgf.image = {};
    if (!cgf.image.width) cgf.image.width = 250;
    if (!cgf.image.height) cgf.image.height = 0;

    if (!cgf.items) cgf.items = {};
    if (!cgf.items.hideTitle) cgf.items.hideTitle = false;
    if (!cgf.items.hideDescription) cgf.items.hideDescription = false;
    if (!cgf.items.hideLink) cgf.items.hideLink = false;

    $scope.layouts = [
        { name: 'Tiles', alias: 'tiles' },
        { name: 'List', alias: 'list' }
    ];

    $scope.layout = $scope.layouts[0];
    angular.forEach($scope.layouts, function(layout) {
        if (layout.alias == cgf.layout.initial) {
            $scope.layout = layout;
        }
    });

});