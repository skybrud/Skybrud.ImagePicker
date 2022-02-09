using System.Collections.Generic;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors {

    /// <summary>
    /// 
    /// </summary>
    public class ImagePickerConfigurationEditor : ConfigurationEditor<ImagePickerConfiguration> {

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePickerConfigurationEditor"/> class.
        /// </summary>
        /// <param name="ioHelper"></param>
        public ImagePickerConfigurationEditor(IIOHelper ioHelper) : base(ioHelper) {
        }

        /// <summary>Converts to valueeditor.</summary>
        /// <param name="configuration">The configuration.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public override IDictionary<string, object> ToValueEditor(object configuration) {

            var d = base.ToValueEditor(configuration);

            d["idType"] = "udi";
            d["disableFolderSelect"] = "true";
            d["onlyImages"] = "true";

            return d;

        }

    }

}