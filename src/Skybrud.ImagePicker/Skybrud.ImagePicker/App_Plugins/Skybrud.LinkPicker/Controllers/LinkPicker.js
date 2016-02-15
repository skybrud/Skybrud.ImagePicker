angular.module("umbraco").controller("Skybrud.LinkPicker.Controller", function ($scope, assetsService, dialogService) {

    if (!$scope.model.value || !Array.isArray($scope.model.value)) {
        $scope.model.value = [];
    }

    $scope.cfg = $scope.model.config.config;
    
    // Set the "mode" property if not already present
    $scope.model.value.forEach(function (link) {
        if (!link.mode) link.mode = (link.id ? (link.url && link.url.indexOf('/media/') === 0 ? 'media' : 'content') : 'url');
    });
    
    function parseUmbracoLink(e) {
        return {
            id: e.id || 0,
            name: e.name || '',
            url: e.url,
            target: e.target || '_self',
            mode: (e.id ? (e.isMedia ? 'media' : 'content') : 'url')
        };
    }

    $scope.addLink = function () {
        dialogService.closeAll();
        dialogService.linkPicker({
            callback: function (e) {
                if (!e.id && !e.url && !confirm('The selected link appears to be empty. Do you want to continue anyways?')) return;
                $scope.model.value.push(parseUmbracoLink(e));
                dialogService.closeAll();
            }
        });
    };

    $scope.editLink = function (link, index) {
        dialogService.closeAll();
        if (link.mode == 'media') {
            dialogService.mediaPicker({
                callback: function (e) {
                    if (!e.id && !e.url && !confirm('The selected link appears to be empty. Do you want to continue anyways?')) return;
                    $scope.model.value[index] = parseUmbracoLink(e);
                    dialogService.closeAll();
                }
            });
        } else {
            dialogService.linkPicker({
                currentTarget: {
                    id: link.id,
                    name: link.name,
                    url: link.url,
                    target: link.target
                },
                callback: function (e) {
                    if (!e.id && !e.url && !confirm('The selected link appears to be empty. Do you want to continue anyways?')) return;
                    $scope.model.value[index] = parseUmbracoLink(e);
                    dialogService.closeAll();
                }
            });
        }
    };

    $scope.removeLink = function (index) {
        var temp = [];
        for (var i = 0; i < $scope.model.value.length; i++) {
            if (index != i) temp.push($scope.model.value[i]);
        }
        $scope.model.value = temp;
    };
    
    $scope.sortableOptions = {
        axis: 'y',
        cursor: 'move',
        handle: '.handle'
    };

});