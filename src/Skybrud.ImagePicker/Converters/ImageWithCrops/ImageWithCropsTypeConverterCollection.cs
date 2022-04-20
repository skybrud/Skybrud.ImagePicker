using System;
using System.Collections.Generic;
using Umbraco.Cms.Core.Composing;

#pragma warning disable 1591

namespace Skybrud.ImagePicker.Converters.ImageWithCrops {

    /// <summary>
    /// Collection of <see cref="IImageWithCropsTypeConverter"/>.
    /// </summary>
    public sealed class ImageWithCropsTypeConverterCollection : BuilderCollectionBase<IImageWithCropsTypeConverter> {

        private readonly Dictionary<string, IImageWithCropsTypeConverter> _lookup;

        public ImageWithCropsTypeConverterCollection(Func<IEnumerable<IImageWithCropsTypeConverter>> items) : base(items) {
            
            _lookup = new Dictionary<string, IImageWithCropsTypeConverter>(StringComparer.OrdinalIgnoreCase);

            foreach (IImageWithCropsTypeConverter item in this) {
                string typeName = item.GetType().AssemblyQualifiedName;
                if (typeName != null && _lookup.ContainsKey(typeName) == false) {
                    _lookup.Add(typeName, item);
                }
            }

        }

        public bool TryGet(string typeName, out IImageWithCropsTypeConverter item) {
            return _lookup.TryGetValue(typeName, out item);
        }

    }

}