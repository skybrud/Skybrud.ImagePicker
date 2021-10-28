using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        #region Member methods
        
        /// <summary>
        /// Returns whether this class is the value converter for the specified <paramref name="propertyType"/>.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <returns><c>true</c> if this class is the value converter for <paramref name="propertyType"/>; otherwise <c>false</c>.</returns>
        public override bool IsConverter(IPublishedPropertyType propertyType) {
            return propertyType.EditorAlias == ImagePickerPropertyEditor.EditorAlias;
        }

        /// <summary>
        /// Converts the source value to a corresponding intermediate value.
        /// </summary>
        /// <param name="owner">The element holding the property type.</param>
        /// <param name="propertyType">The property type.</param>
        /// <param name="source">The source value.</param>
        /// <param name="preview">Whether preview mode is enabled.</param>
        /// <returns>An array of <see cref="Udi"/> representing the intermediate value.</returns>
        public override object ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview) {
            return source?.ToString()
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(Udi.Parse)
                .ToArray();
        }
        
        /// <summary>
        /// Converts the intermediate value to a corresponding object value.
        /// </summary>
        /// <param name="owner">The element holding the property type.</param>
        /// <param name="propertyType">The property type.</param>
        /// <param name="referenceCacheLevel">The reference cache level.</param>
        /// <param name="inter">The intermediate value.</param>
        /// <param name="preview">Whether preview mode is enabled.</param>
        /// <returns>The results of the conversion.</returns>
        public override object ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview) {

            // Get the data type configuration
            ImagePickerConfiguration config = propertyType.DataType.ConfigurationAs<ImagePickerConfiguration>();

            // Get the UDIs from the intermediate value
            Udi[] udis = inter as Udi[] ?? Array.Empty<Udi>();

            // Initialize a collection for the items
            List<object> items = new List<object>();
            
            // Determine the item type
            Type type = propertyType.DataType.ConfigurationAs<ImagePickerConfiguration>()?.ModelType;

            foreach (Udi udi in udis)  {
                
                // Look up the media
                IPublishedContent media = _publishedSnapshotAccessor.PublishedSnapshot.Media.GetById(udi);
                if (media == null) continue;

                // If the configuration doesn't specify a value type, we just create a new ImagePickerImage
                if (type == null) {
                    items.Add(new ImagePickerImage(media, config));
                    continue;
                }

                // If the selected type has a constructor with an ImagePickerConfiguration as the second parameter, we choose that constructor
                if (HasConstructor<IPublishedContent, ImagePickerConfiguration>(type)) {
                    items.Add(Current.Factory.CreateInstance(type, media, config));
                    continue;
                }

                items.Add(Current.Factory.CreateInstance(type, media));

            }

            // Return the item(s) with the correct value type
            type ??= typeof(ImagePickerImage);
            return config.IsMultiPicker ? items.Cast(type).ToList(type) : items.FirstOrDefault();

        }

        /// <summary>
        /// Gets the property cache level.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <returns>The property cache level.</returns>
        public override PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) {
            return PropertyCacheLevel.Snapshot;
        }
        
        /// <summary>
        /// Returns the type of values returned by the converter.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <returns>The CLR type of values returned by the converter.</returns>
        public override Type GetPropertyValueType(IPublishedPropertyType propertyType) {
            
            bool isMultiple = IsMultiPicker(propertyType.DataType);

            Type type = propertyType.DataType.ConfigurationAs<ImagePickerConfiguration>()?.ModelType ?? typeof(ImagePickerImage);

            return isMultiple ? typeof(IEnumerable<>).MakeGenericType(type) : type;

        }

        /// <summary>
        /// Returns whether the specified <paramref name="dataType"/> represents a multi picker.
        /// </summary>
        /// <param name="dataType">The data type.</param>
        /// <returns><c>true</c> if <paramref name="dataType"/> is a multi picker; otherwise <c>false</c>.</returns>
        private bool IsMultiPicker(PublishedDataType dataType) {
            return dataType.ConfigurationAs<ImagePickerConfiguration>()?.IsMultiPicker ?? false;
        }

        public static bool HasConstructor<T1, T2>(Type type) {
            return type
                .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .Select(cs => cs.GetParameters())
                .Any(ps => ps.Length >= 2 && ps[0].ParameterType == typeof(T1) && ps[1].ParameterType == typeof(T2));
        }

        #endregion

    }

}