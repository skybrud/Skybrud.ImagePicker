using Newtonsoft.Json.Linq;
using Skybrud.ImagePicker.Grid.Config;
using Skybrud.ImagePicker.Grid.Values;
using Skybrud.Umbraco.GridData;
using Skybrud.Umbraco.GridData.Interfaces;
using Skybrud.Umbraco.GridData.Rendering;

namespace Skybrud.ImagePicker.Grid.Converters {
    
    /// <summary>
    /// Grid converter for the ImagePicker.
    /// </summary>
    public class ImagePickerGridConverter : IGridConverter {

        /// <summary>
        /// Converts the specified <code>token</code> into an instance of <see cref="IGridControlValue"/>.
        /// </summary>
        /// <param name="control">A reference to the parent <see cref="GridControl"/>.</param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the control value.</param>
        /// <param name="value">The converted control value.</param>
        public bool ConvertControlValue(GridControl control, JToken token, out IGridControlValue value) {
            value = null;
            if (IsImagePickerEditor(control.Editor)) {
                value = GridControlImagePickerValue.Parse(control, token as JObject);
            }
            return value != null;
        }

        /// <summary>
        /// Converts the specified <code>token</code> into an instance of <see cref="IGridEditorConfig"/>.
        /// </summary>
        /// <param name="editor">A reference to the parent <see cref="GridEditor"/>.</param>
        /// <param name="token">The instance of <see cref="JToken"/> representing the editor config.</param>
        /// <param name="config">The converted editor config.</param>
        public bool ConvertEditorConfig(GridEditor editor, JToken token, out IGridEditorConfig config) {
            config = null;
            if (IsImagePickerEditor(editor)) {
                config = GridEditorImagePickerConfig.Parse(editor, token as JObject);
            }
            return config != null;
        }

        /// <summary>
        /// Gets an instance <see cref="GridControlWrapper"/> for the specified <code>control</code>.
        /// </summary>
        /// <param name="control">The control to be wrapped.</param>
        /// <param name="wrapper">The wrapper.</param>
        public bool GetControlWrapper(GridControl control, out GridControlWrapper wrapper) {
            wrapper = null;
            if (IsImagePickerEditor(control.Editor)) {
                wrapper = control.GetControlWrapper<GridControlImagePickerValue, GridEditorImagePickerConfig>();
            }
            return wrapper != null;
        }

        private bool IsImagePickerEditor(GridEditor editor) {
            return editor.Alias.ToLower() == "skybrud.imagepicker" || editor.Alias.ToLower().StartsWith("skybrud.imagepicker.");
        }

    }

}