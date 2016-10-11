using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.Umbraco.GridData.Json;

namespace Skybrud.ImagePicker.Grid.Config {

    /// <summary>
    /// Class representing the title configuration of a image picker.
    /// </summary>
    public class GridEditorImagePickerTitleConfig : GridJsonObject {

        #region Properties

        /// <summary>
        /// Gets whether the title of the image picker list should be shown.
        /// </summary>
        public bool Show { get; private set; }
            
        #endregion

        #region Constructors

        private GridEditorImagePickerTitleConfig(JObject obj) : base(obj) {
            Show = obj.GetBoolean("show");
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an instance of <see cref="GridEditorImagePickerTitleConfig"/> from the specified <see cref="JObject"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static GridEditorImagePickerTitleConfig Parse(JObject obj) {
            return obj == null ? null : new GridEditorImagePickerTitleConfig( obj);
        }

        #endregion

    }

}