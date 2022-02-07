using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors.ImagePickerWithCrops {

    [DataEditor(EditorAlias, EditorType.PropertyValue, EditorName, EditorView, Group = EditorGroup, Icon = EditorIcon, ValueType = ValueTypes.Text)]
    public class ImagePickerWithCropsPropertyEditor : MediaPicker3PropertyEditor {

        #region Constants

        /// <summary>
        /// Gets the alias of the editor.
        /// </summary>
        public const string EditorAlias = "Skybrud.ImagePickerWithCrops.Image";

        /// <summary>
        /// Gets the name of the editor.
        /// </summary>
        public const string EditorName = "Skybrud ImagePickerWithCrops Image";

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
        public const string EditorView = "mediapicker3";

        private readonly IIOHelper _iOHelper;

        #endregion

        #region Constructors

        #endregion

        #region Member methods

        public ImagePickerWithCropsPropertyEditor(IDataValueEditorFactory dataValueEditorFactory, IIOHelper iOHelper) : base(dataValueEditorFactory, iOHelper) {
            _iOHelper = iOHelper;
        }

        protected override IConfigurationEditor CreateConfigurationEditor() => new ImagePickerWithCropsConfigurationEditor(_iOHelper);

        #endregion

    }

}