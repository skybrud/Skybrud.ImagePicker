angular.module("umbraco").controller("Skybrud.LinkPickerPreValues.Controller", function ($scope) {

    var v = Umbraco.Sys.ServerVariables.application.version.split('.');
    $scope.umbVersion = parseFloat(v[0] + '.' + v[1]);

    $scope.views = [
        { alias: 'preview', name: 'Preview' },
        { alias: 'table', name: 'Table' },
        { alias: 'list', name: 'List' }
    ];

    $scope.view = $scope.views[0];

    if (!$scope.model.value) {
        $scope.model.value = {
            limit: 0,
            types: {
                url: true,
                content: true,
                media: true
            },
            view: 'preview',
            columns: {
                type: true,
                id: true,
                name: true,
                url: true,
                target: true
            }
        };
    }

    // Make sure we still support the old "showTable" option
    if ($scope.model.value.showTable === true) {
        delete $scope.model.value.showTable;
        $scope.model.value.view = 'table';
    } else if ($scope.model.value.showTable === false) {
        delete $scope.model.value.showTable;
        $scope.model.value.view = 'list';
    }

    // Make sure the selected view is reflected in the UI
    angular.forEach($scope.views, function (view) {
        if ($scope.model.value.view == view.alias) {
            $scope.view = view;
        }
    });

    $scope.update = function () {
        $scope.model.value.limit = parseInt($scope.model.value.limit) | 0;
        $scope.model.value.view = $scope.view.alias;
    };

});