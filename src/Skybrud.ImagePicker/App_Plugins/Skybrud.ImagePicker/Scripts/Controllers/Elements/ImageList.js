angular.module("umbraco").controller("Skybrud.ImagePicker.Elements.ImageList.Controller", function ($scope, editorService, skyElements) {

    $scope.images = {};

    $scope.addImage = function () {

        // TODO: Make content type selectable if more than one
        var contentType = $scope.contentTypes[0];

        editorService.mediaPicker({
            multiPicker: true,
            submit: function (model) {

                if (model.selection.length === 0) {
                    editorService.close();
                    return;
                }

                // Get the UDIs of each selected image
                var udis = [];
                var temp = {};
                model.selection.forEach(function (image) {
                    temp[image.udi] = image;
                    udis.push(image.udi);
                });
                
                // Get metadata about each image
                skyElements.getImages(udis).then(function (res) {

                    // Add a new item for each image
                    res.data.forEach(function (image) {

                        var item = $scope.initNewItem(contentType, {
                            image: image.udi
                        });

                        item.image = image;

                        if (contentType.hasPropertyType("title")) {
                            item.value.properties.title = image.name;
                        } else if (contentType.hasPropertyType("name")) {
                            item.value.properties.name = image.name;
                        }

                        $scope.items.push(item);

                    });

                    // Synchronize "items" to "value"
                    $scope.sync();

                    // Close the picker/overlay
                    editorService.close();

                    // If the user only selected a single image, the item of that image should be opened for editing
                    if (model.selection.length === 1) {
                        $scope.editItem($scope.items[$scope.items.length - 1]);
                    }

                });

            },
            close: function () {
                editorService.close();
            }
        });
    };

    $scope.editMedia = function (item) {
        var image = $scope.images[item.key];
        editorService.mediaEditor({
            id: image.id,
            submit: function () {
                editorService.close();
            },
            close: function () {
                editorService.close();
            }
        });
    };

    if (!Array.isArray($scope.items)) return;

    angular.forEach($scope.items, function (item) {
        if (item.value.properties.image) {
            skyElements.getImage(item.value.properties.image).then(function (res) {
                item.image = res.data;
            }, function (res) {
                if (res.status === 404) {
                    item.image = {
                        name: "Deleted",
                        id: null,
                        udi: item.value.properties.image,
                        thumbnail: null,
                        trashed: false,
                        deleted: true
                    };
                }
            });
        }
    });

});