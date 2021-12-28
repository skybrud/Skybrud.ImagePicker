using System.Collections.Generic;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors.ImagePickerWithCrops {

    public class ImagePickerWithCropsConfigurationEditor : ConfigurationEditor<ImagePickerWithCropsConfiguration> {
        public ImagePickerWithCropsConfigurationEditor(IIOHelper ioHelper) : base(ioHelper) {
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