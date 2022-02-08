using System.Collections.Generic;
using Skybrud.ImagePicker.Json;
using Skybrud.ImagePicker.Models;
using Skybrud.ImagePicker.PropertyEditors.ImagePickerWithCrops;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.Editors;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Serialization;
using Umbraco.Extensions;

namespace Skybrud.ImagePicker.Factories {
    public class ImageWithCropsReferenceFactory : IDataValueReferenceFactory, IDataValueReference {
        private readonly IJsonSerializer _jsonSerializer;

        public ImageWithCropsReferenceFactory(IJsonSerializer jsonSerializer) {
            _jsonSerializer = jsonSerializer;
        }

        public IDataValueReference GetDataValueReference() => this;

        public bool IsForEditor(IDataEditor dataEditor) => dataEditor.Alias.InvariantEquals(ImagePickerWithCropsPropertyEditor.EditorAlias);

        IEnumerable<UmbracoEntityReference> IDataValueReference.GetReferences(object value) {
            List<UmbracoEntityReference> references = new List<UmbracoEntityReference>();

            var dtos = MediaWithCropsDeserializer.Deserialize(_jsonSerializer, value);

            if (value is not string udis) return references;

            // If for some reason we can't convert it, but it's a list of udis - fx if upgrading from mediapickerv2 to v3
            if (dtos is null) {
                foreach (string udi in udis.Split(',')) {
                    if (UdiParser.TryParse(udi, out GuidUdi guidUdi)) references.Add(new UmbracoEntityReference(guidUdi));
                }

                return references;
            }

            foreach (MediaWithCropsDto dto in dtos) {
                var guidUdi = new GuidUdi(Constants.UdiEntityType.Media, dto.MediaKey);
                if (guidUdi is not null) references.Add(new UmbracoEntityReference(guidUdi));
            }

            return references;
        }
    }
}