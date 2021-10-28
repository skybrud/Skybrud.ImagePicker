using System.Collections.Generic;
using Skybrud.ImagePicker.PropertyEditors;
using Umbraco.Core;
using Umbraco.Core.Models.Editors;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.ImagePicker.Factories {
    
    public class ImageReferenceFactory : IDataValueReferenceFactory, IDataValueReference {
        
        public IDataValueReference GetDataValueReference() => this;

        public IEnumerable<UmbracoEntityReference> GetReferences(object value) {
            
            List<UmbracoEntityReference> references = new List<UmbracoEntityReference>();
            if (value is not string udis) return references;

            foreach (string udi in udis.Split(',')) {
                if (GuidUdi.TryParse(udi, out GuidUdi guidUdi)) references.Add(new UmbracoEntityReference(guidUdi));
            }
            
            return references;

        }

        public bool IsForEditor(IDataEditor dataEditor) => dataEditor.Alias.InvariantEquals(ImagePickerPropertyEditor.EditorAlias);

    }

}