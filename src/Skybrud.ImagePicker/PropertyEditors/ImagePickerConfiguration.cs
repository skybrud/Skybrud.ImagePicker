﻿using System;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors {

    /// <summary>
    /// Extends the mediapicker configuration and adds our own fields.
    /// </summary>
    /// <seealso cref="MediaPickerConfiguration" />
    public class ImagePickerConfiguration : MediaPickerConfiguration {

        private Type _valueType;

        #region Properties

        /// <summary>
        /// Gets the name of the value type. This will be used for resolving the <see cref="ValueType"/> parameter.
        /// </summary>
        [ConfigurationField("model",
            "Value type",
            "/App_Plugins/Skybrud.Umbraco.ImagePicker/Views/ImageModelPicker.html?editor=v2",
            Description = "<a href=\"https://packages.skybrud.dk/skybrud.imagepicker/docs/v3.0/image/configuration/value-type\" class=\"btn btn-primary btn-xs skybrud-image-picker-button\" target=\"_blank\" rel=\"noreferrer noopener\">See the documentation &rarr;</a>\r\n" +
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