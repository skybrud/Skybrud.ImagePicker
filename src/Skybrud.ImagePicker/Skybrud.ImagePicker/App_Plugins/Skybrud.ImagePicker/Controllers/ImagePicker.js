angular.module("umbraco").controller("Skybrud.ImagePicker.Controller", function ($scope, assetsService, dialogService, userService) {

    $scope.cfg = $scope.control.editor.config;


    //Image picker related
    var startNodeId = null;
    userService.getCurrentUser().then(function (userData) {
        startNodeId = userData.startMediaId;
    });

    $scope.addImage = function() {
        dialogService.mediaPicker({
            startNodeId: startNodeId,
            multiPicker: false,
            onlyImages: true,
            callback: function(data) {
                $scope.control.value = data;
                dialogService.closeAll();
            }
        });
    };

    $scope.removeImage = function() {
        $scope.control.value = {};
    };


    //LinkPicker related
    $scope.addLink = function () {
        dialogService.closeAll();
        dialogService.linkPicker({
            callback: function (e) {
                if (!e.id && !e.url && !confirm('The selected link appears to be empty. Do you want to continue anyways?')) return;
                $scope.control.value.link = parseUmbracoLink(e);
                dialogService.closeAll();
            }
        });
    };

    $scope.editLink = function (index) {
        if (!$scope.control.value.link) {
            $scope.addLink(index);
            return;
        }
        dialogService.closeAll();
        if ($scope.control.value.link.mode == 'media') {
            dialogService.mediaPicker({
                callback: function (e) {
                    if (!e.id && !e.url && !confirm('The selected link appears to be empty. Do you want to continue anyways?')) return;
                    $scope.control.value.link = parseUmbracoLink(e);
                    dialogService.closeAll();
                }
            });
        } else {
            dialogService.linkPicker({
                currentTarget: {
                    id: $scope.control.value.link.id,
                    name: $scope.control.value.link.name,
                    url: $scope.control.value.link.url,
                    target: $scope.control.value.link.target
                },
                callback: function (e) {
                    if (!e.id && !e.url && !confirm('The selected link appears to be empty. Do you want to continue anyways?')) return;
                    $scope.control.value.link = parseUmbracoLink(e);
                    dialogService.closeAll();
                }
            });
        }
    };

    $scope.removeLink = function () {
        $scope.control.value.link = null;
    };

    function parseUmbracoLink(e) {
        return {
            id: e.id || 0,
            name: e.name || '',
            url: e.url,
            target: e.target || '_self',
            mode: (e.id ? (e.isMedia ? 'media' : 'content') : 'url')
        };
    }
});