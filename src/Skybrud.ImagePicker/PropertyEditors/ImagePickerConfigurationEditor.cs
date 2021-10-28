using System.Collections.Generic;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors {

    public class ImagePickerConfigurationEditor : ConfigurationEditor<ImagePickerConfiguration> {

        public override IDictionary<string, object> ToValueEditor(object configuration) {

            var d = base.ToValueEditor(configuration);
            
            d["idType"] = "udi";
            d["disableFolderSelect"] = "true";
            d["onlyImages"] = "true";

            return d;

        }

    }

}