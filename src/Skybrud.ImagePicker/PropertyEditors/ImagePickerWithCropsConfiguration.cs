using System;
using Newtonsoft.Json.Linq;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors {

    /// <summary>
    /// Extends MediaPicker3 with our own additional fields.
    /// </summary>
    /// <seealso cref="MediaPicker3Configuration" />
    public class ImagePickerWithCropsConfiguration : MediaPicker3Configuration {
        
        private Type _valueType;

        #region Properties
        
        /// <summary>
        /// Gets a reference to a <see cref="JObject"/> with information about the selected type converter.
        /// </summary>
        [ConfigurationField("typeConverter",
            "Type converter",
            "/App_Plugins/Skybrud.Umbraco.ImagePicker/Views/TypeConverter.html?editor=v3",
            Description = "<a href=\"https://packages.skybrud.dk/skybrud.imagepicker/docs/v3.0/imagewithcrops/configuration/#type-converter\" class=\"btn btn-primary btn-xs skybrud-image-picker-button\" target=\"_blank\" rel=\"noreferrer noopener\">See the documentation &rarr;</a>\r\n" +
                          "Select a type converter, which will be used for converting the selected items.")]
        public JObject TypeConverter { get; set; }

        /// <summary>
        /// Gets the name of the value type. This will be used for resolving the <see cref="ValueType"/> parameter.
        /// </summary>
        [ConfigurationField("model",
            "Value type",
            "/App_Plugins/Skybrud.Umbraco.ImagePicker/Views/ImageModelPicker.html?editor=v3",
            Description = "<a href=\"https://packages.skybrud.dk/skybrud.imagepicker/docs/v3.0/imagewithcrops/configuration/#value-type\" class=\"btn btn-primary btn-xs skybrud-image-picker-button\" target=\"_blank\" rel=\"noreferrer noopener\">See the documentation &rarr;</a>\r\n" +
                          "Select the .NET value type that should be used for representing the selected image(s).")]
        public string ValueTypeName { get; set; }

        /// <summary>
        /// Gets the value type.
        /// </summary>
        public Type ValueType => _valueType == null && string.IsNullOrWhiteSpace(ValueTypeName) == false ? _valueType = Type.GetType(ValueTypeName) : _valueType;

        /// <summary>
        /// Gets the crop mode to be used for the returned values. This property currently always returns <see cref="ImageCropMode"/>.
        /// </summary>
        public static ImageCropMode CropMode => ImageCropMode.Crop;

        /// <summary>
        /// Gets whether generated URLs should prefer a focal point. This property currently always returns <c>true</c>.
        /// </summary>
        public static bool PreferFocalPoint => true;

        #endregion

    }

}