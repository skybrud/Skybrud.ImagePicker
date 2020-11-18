using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Skybrud.Essentials.Collections.Extensions;
using Skybrud.ImagePicker.Models;
using Umbraco.Core;
using Umbraco.Core.Composing;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web.PublishedCache;

namespace Skybrud.ImagePicker.PropertyEditors.ValueConverters {

    public class ImageValueConverter : PropertyValueConverterBase {

        private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor;

        #region Constructors

        public ImageValueConverter(IPublishedSnapshotAccessor publishedSnapshotAccessor) {
            _publishedSnapshotAccessor = publishedSnapshotAccessor ?? throw new ArgumentNullException(nameof(publishedSnapshotAccessor));
        }

        #endregion

        public override bool IsConverter(IPublishedPropertyType propertyType) {
            return propertyType.EditorAlias == "Skybrud.ImagePicker.Image";
        }

        public override object ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview) {
            return source?.ToString()
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(Udi.Parse)
                .ToArray();
        }
        
        public override object ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview) {

            var isMultiple = IsMultipleDataType(propertyType.DataType);

            var udis = inter as Udi[] ?? new Udi[0];
            var mediaItems = new List<object>();

            if (inter == null) return isMultiple ? mediaItems : null;

            Type type = propertyType.DataType.ConfigurationAs<ImagePickerConfiguration>()?.ModelType;

            foreach (Udi udi in udis) {
                if (udi is GuidUdi guidUdi) {
                    IPublishedContent media = _publishedSnapshotAccessor.PublishedSnapshot.Media.GetById(guidUdi.Guid);
                    if (media != null) {
                        mediaItems.Add(type == null ? new ImagePickerImage(media) : Current.Factory.CreateInstance(type, media));
                    }
                }
            }

            return isMultiple ? mediaItems.Cast(type) : mediaItems.FirstOrDefault();

        }

        public override object ConvertIntermediateToXPath(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview) {
            return null;
        }

        public override PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) {
            return PropertyCacheLevel.Snapshot;
        }

        public override Type GetPropertyValueType(IPublishedPropertyType propertyType) {
            
            bool isMultiple = IsMultipleDataType(propertyType.DataType);

            Type type = propertyType.DataType.ConfigurationAs<ImagePickerConfiguration>()?.ModelType ?? typeof(ImagePickerImage);

            return isMultiple ? typeof(IEnumerable<>).MakeGenericType(type) : type;

        }

        private bool IsMultipleDataType(PublishedDataType dataType) {
            return dataType.ConfigurationAs<ImagePickerConfiguration>()?.Multiple ?? false;
        }

    }
}