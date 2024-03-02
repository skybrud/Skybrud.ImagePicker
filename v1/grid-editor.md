---
order: 3
title: Grid Editor
---

# Grid Editor

This package also supports adding an image picker as a grid control in the Umbraco grid. Since you most likely want to configure the image picker your self, you have to add your own `package.manifest` with the details about the editor.

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
            "title": { 
                "mode":  "required"
            },
            "description": { 
                "mode":  "required"
            },
            "link": { 
                "mode":  "optional"
            },
            "nocrop": {
               "mode": "hidden",
                "default": false
            }
        }
    }
}
```