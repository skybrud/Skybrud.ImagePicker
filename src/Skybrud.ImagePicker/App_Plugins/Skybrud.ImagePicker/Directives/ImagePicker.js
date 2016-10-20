angular.module('umbraco').directive('skybrudImagepicker', ['dialogService', 'skybrudImagePickerService', 'skybrudLinkPickerService', 'userService', 'entityResource', 'mediaHelper', function (dialogService, p, links, userService, entityResource, mediaHelper) {
    return {
        scope: {
            value: '=',
            config: '='
        },
        transclude: true,
        restrict: 'E',
        replace: true,
        templateUrl: '/App_Plugins/Skybrud.ImagePicker/Views/ImagePickerDirective.html',
        link: function (scope) {

            // Initial values
            scope.layout = 'list';
            scope.mode = 'view';

            // Image picker related
            var startNodeId = null;
            userService.getCurrentUser().then(function (userData) {
                startNodeId = userData.startMediaId;
            });

            function initValue() {

                // Initialize the value
                if (!scope.value) scope.value = {};
                if (!scope.value.items) scope.value.items = [];

                // Lookup the IDs
                if (scope.value.items.length > 0) {

                    // Get the IDs of the images currently selected
                    var ids = [];
                    angular.forEach(scope.value.items, function(item) {
                        if (item.imageId) {
                            ids.push(item.imageId);
                        }
                    });

                    if (ids.length > 0) {

                        // Initialize a new hashset (JavaScript object)
                        var hash = {};

                        entityResource.getByIds(ids, 'media').then(function (data) {

                            // Add each media to the hashset for O(1) lookups
                            angular.forEach(data, function (media) {
                                if (!media.image) media.image = mediaHelper.resolveFileFromEntity(media);
                                hash[media.id] = media;
                            });

                            // Update each item with its corresponding media
                            angular.forEach(scope.value.items, function (item) {
                                if (item.imageId && hash[item.imageId]) {
                                    item.$image = hash[item.imageId];
                                    item.$imageUrl = getImageUrl(item);
                                    item.imageUrl = getImageUrl(item);
                                }
                            });

                        });

                    }

                }

            }

            function initConfig() {

                scope.cfg = scope.config ? scope.config : {};

                // Restore configuration not specified (can probably be made prettier)
                if (!scope.cfg.limit) scope.cfg.limit = 0;
                
                if (!scope.cfg.image) scope.cfg.image = {};
                if (!scope.cfg.image.width) scope.cfg.image.width = 250;
                if (!scope.cfg.image.height) scope.cfg.image.height = scope.cfg.image.width / 16 * 10;

                if (!scope.cfg.layout) scope.cfg.layout = {};
                if (scope.cfg.layout.initial != 'tiles') scope.cfg.layout.initial = 'list';
                if (!scope.cfg.layout.hideSelector) scope.cfg.layout.hideSelector = false;

                scope.itemStyles = {
                    width: scope.cfg.image.width + 'px',
                    maxWidth: scope.cfg.image.width + 'px'
                };

                scope.layout = scope.cfg.layout.initial;

            }

            /// Gets an URL for a cropped version of the image (based on the current configuration)
            function getImageUrl(item) {
                return item.$image ? item.$image.image + '?width=' + scope.cfg.image.width + '&height=' + scope.cfg.image.height + '&mode=crop' : null;
            }

            /// Swaps two items in an array
            function swap(array, i, j) {
                var a = array[i];
                array[i] = array[j];
                array[j] = a;
            }

            scope.addItem = function () {

                var item = {
                    title: '',
                    description: '',
                    imageId: 0,
                    link: null
                };

                // Collapse all open rows
                scope.toggleOpen({});

                scope.value.items.push(item);

                scope.addImage(item, function() {
                    if (scope.layout == 'list') {
                        item.$showInfo = true;
                    }
                });

            };

            scope.removeItem = function (index) {
                if (confirm('Er du sikker på at du vil slette denne slide?')) {
                    scope.value.items.splice(index, 1);
                }
            };

            // Opens a dialog/overlay for selecting the image of the item
            scope.addImage = function (item, callback) {

                // Get the Umbraco version
                var v = Umbraco.Sys.ServerVariables.application.version.split('.');
                v = parseFloat(v[0] + '.' + v[1]);

                // The new overlay only works from 7.4 and up, so for older
                // versions we should use the dialogService instead
                if (v < 7.4) {

                    dialogService.mediaPicker({
                        startNodeId: startNodeId,
                        multiPicker: false,
                        onlyImages: true,
                        callback: function (data) {
                            item.imageId = data.id;
                            item.$image = data;
                            item.$imageUrl = getImageUrl(item);
                            item.imageUrl = getImageUrl(item);
                            dialogService.closeAll();
                            if (callback) callback(data);
                        }
                    });

                } else {

                    scope.mediaPickerOverlay = {
                        view: "mediapicker",
                        title: "Select media",
                        startNodeId: startNodeId,
                        multiPicker: false,
                        onlyImages: true,
                        disableFolderSelect: true,
                        show: true,
                        submit: function (model) {

                            var data = model.selectedImages[0];

                            item.imageId = data.id;
                            item.$image = data;
                            item.$imageUrl = getImageUrl(item);
                            item.imageUrl = getImageUrl(item);
                            if (callback) callback(data);

                            scope.mediaPickerOverlay.show = false;
                            scope.mediaPickerOverlay = null;

                        }
                    };

                }

            };

            scope.removeImage = function (item) {
                item.imageId = 0;
                item.$image = null;
                item.$imageUrl = null;
            };

            scope.addLink = function (item) {
                links.addLink(function (link) {
                    item.link = link;
                });
            };

            scope.editLink = function (item) {
                links.editLink(item.link, function (link) {
                    item.link = link;
                });
            };

            scope.removeLink = function (item) {
                item.link = null;
            };

            scope.moveItemLeft = function (index) {
                swap(scope.value.items, index, index - 1);
            };

            scope.moveItemRight = function (index) {
                swap(scope.value.items, index, index + 1);
            };

            scope.toggleOpen = function (item) {

                if (item.$showInfo) {
                    item.$showInfo = false;
                    return;
                }

                angular.forEach(scope.value.items, function (i) {
                    i.$showInfo = i == item;
                });

            };

            scope.sortableOptions = {
                axis: 'y',
                cursor: 'move',
                handle: '.handle',
                tolerance: 'pointer',
                containment: 'parent',
                forcePlaceholderSize: true
            };

            initValue();
            initConfig();

        }
    };
}]);