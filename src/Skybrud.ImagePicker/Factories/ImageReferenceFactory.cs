using System.Collections.Generic;
using Skybrud.ImagePicker.PropertyEditors;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.Editors;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Extensions;

namespace Skybrud.ImagePicker.Factories {

    public class ImageReferenceFactory : IDataValueReferenceFactory, IDataValueReference {
        
        public IDataValueReference GetDataValueReference() => this;

        public static IEnumerable<UmbracoEntityReference> GetReferences(object value) {
            
            List<UmbracoEntityReference> references = new List<UmbracoEntityReference>();
            if (value is not string udis) return references;

            foreach (string udi in udis.Split(',')) {
                if (UdiParser.TryParse(udi, out GuidUdi guidUdi)) references.Add(new UmbracoEntityReference(guidUdi));
            }
            
            return references;

        }

        public bool IsForEditor(IDataEditor dataEditor) => dataEditor.Alias.InvariantEquals(ImagePickerPropertyEditor.EditorAlias);

        IEnumerable<UmbracoEntityReference> IDataValueReference.GetReferences(object value) {
            throw new System.NotImplementedException();
        }
    }

}