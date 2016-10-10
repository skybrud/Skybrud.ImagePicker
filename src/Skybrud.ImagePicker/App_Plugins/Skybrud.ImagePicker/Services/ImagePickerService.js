angular.module('umbraco.services').factory('skybrudImagePickerService', function (dialogService) {

    var service = {

        parseUmbracoLink: function (e) {
            return {
                id: e.id || 0,
                name: e.name || '',
                url: e.url,
                target: e.target || '_self',
                mode: (e.id ? (e.isMedia ? 'media' : 'content') : 'url')
            };
        }

    };

    return service;

});