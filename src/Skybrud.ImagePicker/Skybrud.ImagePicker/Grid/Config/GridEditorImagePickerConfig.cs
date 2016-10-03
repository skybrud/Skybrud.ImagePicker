using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.ImagePicker.Grid.Config {

    /// <summary>
    /// Class representing the configuration of a image picker.
    /// </summary>
    public class GridEditorImagePickerConfig : GridJsonObject, IGridEditorConfig {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent editor.
        /// </summary>
        public GridEditor Editor { get; private set; }
            
        #endregion

        #region Constructors

        private GridEditorImagePickerConfig(GridEditor editor, JObject obj) : base(obj) {
            Editor = editor;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an instance of <see cref="GridEditorImagePickerConfig"/> from the specified <see cref="JObject"/>.
        /// </summary>
        /// <param name="editor">The parent editor.</param>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static GridEditorImagePickerConfig Parse(GridEditor editor, JObject obj) {
            return obj == null ? null : new GridEditorImagePickerConfig(editor, obj);
        }

        #endregion

    }

}