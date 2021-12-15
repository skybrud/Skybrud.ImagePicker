using System.Collections.Generic;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors {

    public class ImagePickerConfigurationEditor : ConfigurationEditor<ImagePickerConfiguration> {
        public ImagePickerConfigurationEditor(IIOHelper ioHelper) : base(ioHelper) {
        }

        public override IDictionary<string, object> ToValueEditor(object configuration) {

            var d = base.ToValueEditor(configuration);
            
            d["idType"] = "udi";
            d["disableFolderSelect"] = "true";
            d["onlyImages"] = "true";

            return d;

        }

    }

}