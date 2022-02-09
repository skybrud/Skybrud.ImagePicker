using System.Collections.Generic;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors {

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Umbraco.Cms.Core.PropertyEditors.ConfigurationEditor" />
    public class ImagePickerWithCropsConfigurationEditor : ConfigurationEditor<ImagePickerWithCropsConfiguration> {

        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePickerWithCropsConfigurationEditor"/> class.
        /// </summary>
        /// <param name="ioHelper"></param>
        public ImagePickerWithCropsConfigurationEditor(IIOHelper ioHelper) : base(ioHelper) {
            Field(nameof(MediaPicker3Configuration.StartNodeId))
                .Config = new Dictionary<string, object> { { "idType", "udi" } };

            Field(nameof(MediaPicker3Configuration.Filter))
                .Config = new Dictionary<string, object> { { "itemType", "media" } };
        }

        /// <inheritdoc />
        public override IDictionary<string, object> ToValueEditor(object configuration) {

            var d = base.ToValueEditor(configuration);

            d["idType"] = "udi";
            d["disableFolderSelect"] = "true";
            d["onlyImages"] = "true";

            return d;

        }

    }

}