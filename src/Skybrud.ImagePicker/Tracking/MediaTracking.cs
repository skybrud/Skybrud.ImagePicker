using System.Collections.Generic;
using Skybrud.ImagePicker.PropertyEditors;
using Umbraco.Core;
using Umbraco.Core.Models.Editors;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.ImagePicker.Tracking {
    
    public class MediaTracking : IDataValueReferenceFactory, IDataValueReference {
        
        public IDataValueReference GetDataValueReference() => this;

        public IEnumerable<UmbracoEntityReference> GetReferences(object value) {
            var references = new List<UmbracoEntityReference>();
            if (value != null) {
                foreach (var image in value.ToString().Split(',')) {
                    var isValid = Udi.TryParse(image, out var udi);
                    if (isValid) {
                        references.Add(new UmbracoEntityReference(udi));
                    }
                }
            }
            return references;
        }

        public bool IsForEditor(IDataEditor dataEditor) => dataEditor.Alias.InvariantEquals(ImagePickerPropertyEditor.EditorAlias);
    }

}