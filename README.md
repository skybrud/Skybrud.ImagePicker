Skybrud.Umbraco.Image
=====================

Skybrud.ImagePicker is a package for Umbraco 7+ containing a property editor and a grid editor for selecting a number of images for use in a slider, gallery or similar.





## Links

- <a href="#installation">Installation</a>
- <a href="#using-the-property-editor">Using the property editor</a>
- <a href="#using-the-grid-editor">Using the grid editor</a>
- <a href="#using-the-link-picker-in-your-own-projects">Using the link picker in your own projects</a>





## Installation

1. [**NuGet Package**][NuGetPackage]  
Install this NuGet package in your Visual Studio project. Makes updating easy.

1. [**ZIP file**][GitHubRelease]  
Grab a ZIP file of the latest release; unzip and move the contents to the root directory of your web application.

<!--1. [**Umbraco package**][UmbracoPackage]  
Install the package through the Umbraco backoffice.-->

[NuGetPackage]: https://www.nuget.org/packages/Skybrud.ImagePicker
[UmbracoPackage]: https://our.umbraco.org/projects/backoffice-extensions/skybrudimagepicker/
[GitHubRelease]: https://github.com/skybrud/Skybrud.ImagePicker/releases





## Using the property editor

The property can be used a either a single image picker or a multiple image picker, but both having the same model (an instance of `ImagePickerList`).

So to get the list, you can use the `GetImagePickerList` extension method (with `multiImagePicker` being the alias of the property):

```C#
ImagePickerList imagePickerList = Model.GetImagePickerList("multiImagePicker");
```

Using this extension method, you can be certain to get an instance of `ImagePickerList`. If the property value is nto an `ImagePickerList`, the method will simply return an empty list instead.

If you just need to get the first item, you can use the `GetImagePickerItem` extension method instead:

```C#
ImagePickerItem imagePickerItem = Model.GetImagePickerItem("singleImagePicker");
```

In a similar way, you can be certain that this method will always return an instance of `ImagePickerItem`.

Both extension methods are defined in the `Skybrud.ImagePicker.Extensions` namespace.





## Using the grid editor

This package also supports a adding an image picker as a grid control in the Umbraco grid. Since you most likely want to configure the image picker your self, you have to add your own `package.manifest` with the details about the editor.

In it's simplest form (default options), the JSON for the editor can look like this (here with a slider as an example):

```JSON
{
    "name": "Slider",
    "alias": "Skybrud.ImagePicker.Slider",
    "view": "/App_Plugins/Skybrud.ImagePicker/Views/ImagePickerGridEditor.html",
    "icon": "icon-pictures-alt-2",
}
```

The full configuration for the image picker looks like this:

```JSON
{
    "name": "Slider",
    "alias": "Skybrud.ImagePicker.Slider",
    "view": "/App_Plugins/Skybrud.ImagePicker/Views/ImagePickerGridEditor.html",
    "icon": "icon-pictures-alt-2",
    "config": {
        "limit": 5,
        "layout": {
        "initial": "list",
        "hideSelector": false
        },
        "title": {
        "show": false,
        "placeholder": ""
        },
        "image": {
        "width": 250,
        "height": 0
        },
        "items": {
        "hideTitle": false,
        "hideDescription": false,
        "hideLink": false
        }
    }
}
```





### Skybrud.Umbraco.GridData
The image picker also works with our <a href="https://github.com/skybrud/Skybrud.Umbraco.GridData" target="_blank"><strong>Skybrud.Umbraco.GridData</strong></a> package.

Given that editor alias is either `Skybrud.ImagePicker` or starts with `Skybrud.ImagePicker.` (both case-insensitive) and you have an instance of `GridControl` representing a control with the image picker, the `Value` property will expose an instance of `GridControlImagePickerValue`, while the `Editor.Config` property will expose an instance of `GridEditorImagePickerConfig` for the editor configuration.





## Using the link picker in your own projects

In relation to the backoffice, the main logic of the image picker has been isolated into an Angular directive that can be used in your custom Angular views.

Below is an example of the view for the property editor:

```HTML
<div>
    <skybrud-imagepicker value="model.value" config="model.config.config">Sponsored by omgbacon.dk</skybrud-imagepicker>
</div>
```

The model of the image picker list is specified through the `value` attribute - you can simply pass a variable with an empty JavaScript object, and the image picker directive will make sure to set the correct properties.

In a similar way, the configuration can be specified through the `config` attribute. The value specified through this attribute is a JavaScript object similar to the `config` object in the grid editor configuration as shown above.
