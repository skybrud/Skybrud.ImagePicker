using System;
using Umbraco.Core;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors {
    
    public class ImagePickerConfiguration {

        private Type _type;

        #region Properties

        [ConfigurationField("multiPicker",
            "Pick multiple images?",
            "boolean",
            Description = "Select whether this picker should support picking multiple images.")]
        public bool Multiple { get; set; }
        
        [ConfigurationField("startNodeId",
            "Start node",
            "mediapicker",
            Description = "Select the start node of the picker. If not specified, the start node will be based on the user's permissions.")]
        public Udi StartNodeId { get; set; }

        [ConfigurationField(Constants.DataTypes.ReservedPreValueKeys.IgnoreUserStartNodes,
            "Ignore user start nodes",
            "boolean",
            Description = "Selecting this option allows a user to choose images that they normally don't have access to.")]
        public bool IgnoreUserStartNodes { get; set; }
        
        [ConfigurationField("model",
            "Value type",
            "/App_Plugins/Skybrud.ImagePicker/Views/ImageModelPicker.html",
            Description = "Select the .NET type that should be used for representing the selected image(s).<br /><br /><a href=\"https://packages.skybrud.dk/skybrud.imagepicker/docs/v2.0/configuration/#value-type\" class=\"btn btn-primary btn-xs skybrud-image-picker-button\" target=\"_blank\" rel=\"noreferrer noopener\">See the documentation &rarr;</a>")]
        public string Model { get; set; }

        public Type ModelType {

            get {

                if (_type == null && string.IsNullOrWhiteSpace(Model) == false) {
                    _type = Type.GetType(Model);
                }

                return _type;

            }

        }

        #endregion

    }

}