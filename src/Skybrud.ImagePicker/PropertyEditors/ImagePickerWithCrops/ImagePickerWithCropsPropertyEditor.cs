using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors.ImagePickerWithCrops {

    /// <summary>
    /// Extends the MediaPicker3 property editor with our additional config options
    /// </summary>
    /// <seealso cref="Umbraco.Cms.Core.PropertyEditors.MediaPicker3PropertyEditor" />
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


        /// <summary>
        /// Initializes a new instance of the <see cref="ImagePickerWithCropsPropertyEditor"/> class.
        /// </summary>
        /// <param name="dataValueEditorFactory">The data value editor factory.</param>
        /// <param name="iOHelper">The io helper.</param>
        public ImagePickerWithCropsPropertyEditor(IDataValueEditorFactory dataValueEditorFactory, IIOHelper iOHelper) : base(dataValueEditorFactory, iOHelper) {
            _iOHelper = iOHelper;
        }

        /// <inheritdoc />
        protected override IConfigurationEditor CreateConfigurationEditor() => new ImagePickerWithCropsConfigurationEditor(_iOHelper);

        #endregion

    }

}