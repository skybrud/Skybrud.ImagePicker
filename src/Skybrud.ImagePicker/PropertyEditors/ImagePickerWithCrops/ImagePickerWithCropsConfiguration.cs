using System;
using System.Runtime.Serialization;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors.ImagePickerWithCrops {

    public class ImagePickerWithCropsConfiguration {

        private Type _valueType;

        #region Properties

        [ConfigurationField("filter", "Accepted types", "treesourcetypepicker",
            Description = "Limit to specific types")]
        public string Filter { get; set; }

        [ConfigurationField("multiple", "Pick multiple items", "boolean", Description = "Outputs a IEnumerable")]
        public bool Multiple { get; set; }

        [ConfigurationField("validationLimit", "Amount", "numberrange", Description = "Set a required range of medias")]
        public NumberRange ValidationLimit { get; set; } = new NumberRange();

        [DataContract]
        public class NumberRange {
            [DataMember(Name = "min")]
            public int? Min { get; set; }

            [DataMember(Name = "max")]
            public int? Max { get; set; }
        }

        [ConfigurationField("crops", "Image Crops", "views/propertyeditors/mediapicker3/prevalue/mediapicker3.crops.html", Description = "Local crops, stored on document")]
        public CropConfiguration[] Crops { get; set; }

        [DataContract]
        public class CropConfiguration {
            [DataMember(Name = "alias")]
            public string Alias { get; set; }

            [DataMember(Name = "label")]
            public string Label { get; set; }

            [DataMember(Name = "width")]
            public int Width { get; set; }

            [DataMember(Name = "height")]
            public int Height { get; set; }
        }
        
        /// <summary>
        /// Gets the UDI of the selected start node.
        /// </summary>
        [ConfigurationField("startNodeId",
            "Start node",
            "mediapicker",
            Description = "Select the start node of the picker. If not specified, the start node will be based on the user's permissions.")]
        public Udi StartNodeId { get; set; }

        /// <summary>
        /// Gets whether the user's start nodes should be ignored.
        /// </summary>
        [ConfigurationField(Constants.DataTypes.ReservedPreValueKeys.IgnoreUserStartNodes,
            "Ignore user start nodes",
            "boolean",
            Description = "Selecting this option allows a user to choose images that they normally don't have access to.")]
        public bool IgnoreUserStartNodes { get; set; }

        [ConfigurationField("enableLocalFocalPoint", "Enable Focal Point", "boolean")]
        public bool EnableLocalFocalPoint { get; set; }

        /// <summary>
        /// Gets the name of the value type. This will be used for resolving the <see cref="ValueType"/> parameter.
        /// </summary>
        [ConfigurationField("model",
            "Value type",
            "/App_Plugins/Skybrud.ImagePicker/Views/ImageWithCropsModelPicker.html",
            Description = "Select the .NET value type that should be used for representing the selected image(s).<br /><br /><a href=\"https://packages.skybrud.dk/skybrud.imagepicker/docs/v2.0/configuration/#value-type\" class=\"btn btn-primary btn-xs skybrud-image-picker-button\" target=\"_blank\" rel=\"noreferrer noopener\">See the documentation &rarr;</a>")]
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