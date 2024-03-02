---
order: 2
---

# Property Editor

The property can be used a either a single image picker or a multiple image picker, but both having the same model (an instance of `ImagePickerList`).

So to get the list, you can use the `GetImagePickerList` extension method (with `multiImagePicker` being the alias of the property):

```C#
ImagePickerList imagePickerList = Model.GetImagePickerList("multiImagePicker");
```

Using this extension method, you can be certain to get an instance of `ImagePickerList`. If the property value is not an image picker, the method will simply return an empty list instead.

If you just need to get the first item, you can use the `GetImagePickerItem` extension method instead:

```C#
ImagePickerItem imagePickerItem = Model.GetImagePickerItem("singleImagePicker");
```

In a similar way, you can be certain that this method will always return an instance of `ImagePickerItem`, although it may not be valid (eg. if no items have been selected).

Both extension methods are defined in the `Skybrud.ImagePicker.Extensions` namespace.
