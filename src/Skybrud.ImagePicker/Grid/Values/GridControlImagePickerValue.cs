using System.Text;
using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.ImagePicker.Grid.Values {
    
    /// <summary>
    /// Class representing the value of an ImagePicker control in the Umbraco grid.
    /// </summary>
    public class GridControlImagePickerValue : ImagePickerList, IGridControlValue {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent control.
        /// </summary>
        public GridControl Control { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a enw instance based on the specified <paramref name="control"/> and <paramref name="obj"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        protected GridControlImagePickerValue(GridControl control, JObject obj) : base(obj) {
            Control = control;
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Gets the searchable text for this image picker value.
        /// </summary>
        public string GetSearchableText() {
            
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Title);

            foreach (ImagePickerItem item in Items) {
                sb.AppendLine(item.Title);
                sb.AppendLine(item.Description);
            }

            return sb.ToString();

        }

        #endregion

        #region Static methods

        /// <summary>
        /// Gets an instance of <see cref="GridControlImagePickerValue"/> from the specified <see cref="JObject"/>.
        /// </summary>
        /// <param name="control">The parent control.</param>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        public static GridControlImagePickerValue Parse(GridControl control, JObject obj) {
            return control == null ? null : new GridControlImagePickerValue(control, obj ?? new JObject());
        }

        #endregion

    }

}