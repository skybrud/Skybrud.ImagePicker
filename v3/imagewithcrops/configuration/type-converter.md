# Type Converter

The **type converter** option lets you specify a custom class that will then be used for converting the selected `MediaWithCrops` instances into a more suitable type.

> **INFO:** Notice that if you select a type converter, the [value type](./../value-type/) option will be ignored.

![image](https://user-images.githubusercontent.com/3634580/164420865-2be90e93-2c57-497c-804b-b16b2dabfdba.png)
*An example of the **Type converter** option on the **Skybrud ImageWithCrops** data type.*

Eg. if your picker is configured to only allow images, you may like to represent the selected images using your custom `ImageItem` class rather than the default `MediaWithCrops`. To do something like this, you can create a new class that implements the `IImageWithCropsTypeConverter` interface:

```csharp
using System;
using Skybrud.ImagePicker.Converters.ImageWithCrops;
using Skybrud.ImagePicker.Models;
using Skybrud.ImagePicker.PropertyEditors;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoNineTests.Converters
{
    
    public class ImageTypeConverter : IImageWithCropsTypeConverter
    {

        public string Name => "Image Type Converter";
        
        public Type GetType(IPublishedPropertyType propertyType,
            ImagePickerWithCropsConfiguration config)
        {
            return typeof(ImageItem);
        }

        public object Convert(IPublishedElement owner,
            IPublishedPropertyType propertyType,
            MediaWithCrops source,
            ImagePickerWithCropsConfiguration config)
        {
            return new ImageItem(source, config);
        }

    }

}
```

The interface describes two methods - first the `GetType` that returns the common type of the items returned by the `Convert` method, and then the `Convert` method handles the actual conversion.

In the example above, the `Convert` method will always convert an item to an `ImageItem` instance, so the `GetType` method should also return a `Type` instance representing `ImageItem`.

But if you have another case where the picker is configured to allow the user to pick a mix of images and videos, you can use the type converter to convert image and video items to different types:

```csharp
using System;
using Skybrud.ImagePicker.Converters.ImageWithCrops;
using Skybrud.ImagePicker.Models;
using Skybrud.ImagePicker.PropertyEditors;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace UmbracoNineTests.Converters
{
    
    public class MixedTypeConverter : IImageWithCropsTypeConverter
    {

        public string Name => "Mixed Type Converter";

        public Type GetType(IPublishedPropertyType propertyType,
            ImagePickerWithCropsConfiguration config)
        {
            return typeof(Item);
        }

        public object Convert(IPublishedElement owner,
            IPublishedPropertyType propertyType,
            MediaWithCrops source,
            ImagePickerWithCropsConfiguration config)
        {

            switch (source.ContentType.Alias)
            {

                case "image":
                    return new ImageItem(source, config);

                case "video":
                    return new VideoItem(source, config);

                default:
                    return new Item(source);

            }

        }

    }

}
```

In this example, image items are converted to `ImageItem` instances, while video items are converted to `VideoItems` instances. Notice that the `GetType` method no longer returns the type of `ImageItem`, but now instead `Item`. This assumes that both `ImageItem` and `VideoItem` extends the `Item` class, and as such the `Item` class serves as a common type.