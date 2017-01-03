angular.module("umbraco").controller("Skybrud.ImagePickerPreValues.Controller", function ($scope) {

    // List of available layouts
    $scope.layouts = [
        { name: 'Tiles', alias: 'tiles' },
        { name: 'Liste', alias: 'list' }
    ];

    // List of available modes for "title", "description" and "link"
    $scope.propertyModes = [
        { name: 'Optional', alias: 'optional' },
        { name: 'Required', alias: 'required' },
        { name: 'Hidden', alias: 'hidden' }
    ];

    if (!$scope.model.value) {
        $scope.model.value = {
            limit: 0
        };
    }

    var cfg = $scope.model.value;

    if (!cfg.title) cfg.title = {};
    if (!cfg.title.show) cfg.title.show = false;
    if (!cfg.title.placeholder) cfg.title.placeholder = '';

    if (!cfg.layout) cfg.layout = {};
    if (!cfg.layout.initial) cfg.layout.initial = 'tiles';
    if (!cfg.layout.hideSelector) cfg.layout.hideSelector = false;

    if (!cfg.image) cfg.image = {};
    if (!cfg.image.width) cfg.image.width = 250;
    if (!cfg.image.height) cfg.image.height = 0;

    if (!cfg.items) cfg.items = {};

    // Set initial values for the dropdowns
    $scope.titleMode = $scope.propertyModes[0];
    $scope.descriptionMode = $scope.propertyModes[0];
    $scope.linkMode = $scope.propertyModes[0];

    // Update the values for "titleMode", "descriptionMode" and "linkMode"
    angular.forEach($scope.propertyModes, function (mode) {
        if (cfg.items.title && cfg.items.title.mode == mode.alias) $scope.titleMode = mode;
        if (cfg.items.description && cfg.items.description.mode == mode.alias) $scope.descriptionMode = mode;
        if (cfg.items.link && cfg.items.link.mode == mode.alias) $scope.linkMode = mode;
    });

    $scope.layout = $scope.layouts[0];
    angular.forEach($scope.layouts, function (layout) {
        if (layout.alias == cfg.layout.initial) {
            $scope.layout = layout;
        }
    });

    $scope.update = function () {
        cfg.items = {
            title: { mode: $scope.titleMode.alias },
            description: { mode: $scope.descriptionMode.alias },
            link: { mode: $scope.linkMode.alias }
        };
    };

});