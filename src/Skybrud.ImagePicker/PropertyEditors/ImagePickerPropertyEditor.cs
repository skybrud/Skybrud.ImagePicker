using Umbraco.Core.Logging;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors {

    [DataEditor(EditorAlias, EditorType.PropertyValue, "Skybrud ImagePicker Image", EditorView, Group = "Skybrud.dk", Icon = "icon-picture", ValueType = ValueTypes.Text)]
    public class ImagePickerPropertyEditor : DataEditor {

        #region Constants

        /// <summary>
        /// Gets the alias of the editor.
        /// </summary>
        public const string EditorAlias = "Skybrud.ImagePicker.Image";

        /// <summary>
        /// Gets the view of the editor.
        /// </summary>
        public const string EditorView = "mediapicker";

        #endregion

        #region Constructors

        #endregion

        #region Member methods

        public ImagePickerPropertyEditor(ILogger logger) : base(logger) { }

        protected override IConfigurationEditor CreateConfigurationEditor() {
            return new ImagePickerConfigurationEditor();
        }

        #endregion

    }

}