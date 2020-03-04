angular.module("umbraco").controller("Skybrud.ImagePicker.ListPreview.Controller", function ($scope, skyElements) {

    $scope.images = {};

    $scope.update = function () {

        if (!$scope.item) return;
        if (!$scope.item.value) return;

        $scope.item.value.properties.elements.forEach(function (el) {

            if (el.properties.image) {
                skyElements.getImage(el.properties.image).then(function (res) {
                    $scope.images[el.key] = res.data;
                }, function (res) {
                    if (res.status === 404) {
                        $scope.images[el.key] = {
                            name: "Deleted",
                            id: null,
                            udi: el.properties.image,
                            thumbnail: null,
                            trashed: false,
                            deleted: true
                        };
                    }
                });
            }

        });

    };

    $scope.update();

});