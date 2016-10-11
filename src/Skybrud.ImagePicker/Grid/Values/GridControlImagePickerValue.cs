using Newtonsoft.Json.Linq;
using Skybrud.Umbraco.GridData;
using Skybrud.Umbraco.GridData.Interfaces;

namespace Skybrud.ImagePicker.Grid.Values {
    
    public class GridControlImagePickerValue : ImagePickerList, IGridControlValue {

        #region Properties

        /// <summary>
        /// Gets a reference to the parent control.
        /// </summary>
        public GridControl Control { get; private set; }

        public bool IsValid { get; private set; }

        #endregion

        #region Constructors

        protected GridControlImagePickerValue(GridControl control, JObject obj) : base(obj) {
            Control = control;
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