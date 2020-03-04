using System.Collections.Generic;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors {

    public class ImagePickerConfigurationEditor : ConfigurationEditor<MediaPickerConfiguration> {

        public ImagePickerConfigurationEditor() {
            Field("StartNodeId").Config = new Dictionary<string, object> {
                { "multiPicker", false },
                { "onlyImages", true },
                { "disableFolderSelect", true },
                { "idType", "udi" }
            };
        }

    }

}