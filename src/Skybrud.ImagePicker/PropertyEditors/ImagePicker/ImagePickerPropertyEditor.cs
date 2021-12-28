using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors {

    [DataEditor(EditorAlias, EditorType.PropertyValue, EditorName, EditorView, Group = EditorGroup, Icon = EditorIcon, ValueType = ValueTypes.Text)]
    public class ImagePickerPropertyEditor : DataEditor {

        #region Constants

        /// <summary>
        /// Gets the alias of the editor.
        /// </summary>
        public const string EditorAlias = "Skybrud.ImagePicker.Image";

        /// <summary>
        /// Gets the name of the editor.
        /// </summary>
        public const string EditorName = "Skybrud ImagePicker Image";

        /// <summary>
        /// Gets the group name of the editor.
        /// </summary>
        public const string EditorGroup = "Skybrud.dk";

        /// <summary>
        /// Gets the icon of the editor.
        /// </summary>
        public const string EditorIcon = "icon-picture color-skybrud";

        /// <summary>
        /// Gets the view of the editor.
        /// </summary>
        public const string EditorView = "mediapicker";

        private readonly IIOHelper _iOHelper;

        #endregion

        #region Constructors

        #endregion

        #region Member methods

        public ImagePickerPropertyEditor(IDataValueEditorFactory dataValueEditorFactory, IIOHelper iOHelper) : base(dataValueEditorFactory) {
            _iOHelper = iOHelper;
        }

        protected override IConfigurationEditor CreateConfigurationEditor() {
            return new ImagePickerConfigurationEditor(_iOHelper);
        }

        #endregion

    }

}