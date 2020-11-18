using Umbraco.Core.Logging;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors {

    [DataEditor("Skybrud.ImagePicker.Image", EditorType.PropertyValue, "Skybrud ImagePicker Image", "mediapicker", Group = "Skybrud.dk", Icon = "icon-picture", ValueType = ValueTypes.Text)]
    public class ImagePickerPropertyEditor : DataEditor {

        public ImagePickerPropertyEditor(ILogger logger) : base(logger) { }

        protected override IConfigurationEditor CreateConfigurationEditor() {
            return new ImagePickerConfigurationEditor();
        }

    }

}