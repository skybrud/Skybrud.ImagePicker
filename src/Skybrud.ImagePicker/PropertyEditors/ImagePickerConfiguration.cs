using System;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors {
    
    public class ImagePickerConfiguration : MediaPickerConfiguration {

        private Type _type;

        [ConfigurationField("model", "Model", "textstring", Description = "Specify the .NET type to be used as model. Default is ImagePickerImage.")]
        public string Model { get; set; }

        public Type ModelType {

            get {

                if (_type == null && string.IsNullOrWhiteSpace(Model) == false) {
                    _type = Type.GetType(Model);
                }

                return _type;

            }

        }

    }

}