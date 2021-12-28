using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Skybrud.Essentials.Collections.Extensions;
using Skybrud.ImagePicker.Extensions;
using Skybrud.ImagePicker.Models;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Serialization;
using Umbraco.Extensions;

namespace Skybrud.ImagePicker.PropertyEditors.ImagePickerWithCrops.ValueConverters {

    public class ImageWithCropsValueConverter : PropertyValueConverterBase {

        private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor; 
        private readonly IServiceProvider _serviceProvider;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IPublishedUrlProvider _publishedUrlProvider;
        private readonly IPublishedValueFallback _publishedValueFallback;

        #region Constructors

        public ImageWithCropsValueConverter(IPublishedSnapshotAccessor publishedSnapshotAccessor,
                                            IServiceProvider serviceProvider,
                                            IJsonSerializer jsonSerializer,
                                            IPublishedUrlProvider publishedUrlProvider,
                                            IPublishedValueFallback publishedValueFallback) {
            _publishedSnapshotAccessor = publishedSnapshotAccessor ?? throw new ArgumentNullException(nameof(publishedSnapshotAccessor));
            _serviceProvider = serviceProvider;
            _jsonSerializer = jsonSerializer;
            _publishedUrlProvider = publishedUrlProvider;
            _publishedValueFallback = publishedValueFallback;
        }

        #endregion

        #region Member methods
        
        /// <summary>
        /// Returns whether this class is the value converter for the specified <paramref name="propertyType"/>.
        /// </summary>
        /// <param name="propertyType">The property type.</param>
        /// <returns><c>true</c> if this class is the value converter for <paramref name="propertyType"/>; otherwise <c>false</c>.</returns>
        public override bool IsConverter(IPublishedPropertyType propertyType) {
            return propertyType.EditorAlias == ImagePickerWithCropsPropertyEditor.EditorAlias;
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
                .Select(UdiParser.Parse)
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
            ImagePickerWithCropsConfiguration config = propertyType.DataType.ConfigurationAs<ImagePickerWithCropsConfiguration>();

            // Get the UDIs from the intermediate value
            var dtos = Deserialize(_jsonSerializer, inter);

            // Initialize a collection for the items
            List<object> items = new List<object>();
            
            // Determine the item value type
            Type valueType = propertyType.DataType.ConfigurationAs<ImagePickerWithCropsConfiguration>()?.ValueType;

            foreach (MediaWithCropsDto dto in dtos)  {

                var canGetPublishedSnapshot = _publishedSnapshotAccessor.TryGetPublishedSnapshot(out var publishedSnapshotAccessor);

                if (!canGetPublishedSnapshot)
                    continue;
                
                // Look up the media
                IPublishedContent mediaItem = publishedSnapshotAccessor.Media.GetById(dto.MediaKey);

                if (mediaItem == null) continue;

                var localCrops = new ImageCropperValue {
                    Crops = dto.Crops,
                    FocalPoint = dto.FocalPoint,
                    Src = mediaItem.Url(_publishedUrlProvider)
                };

                localCrops.ApplyConfiguration(config);

                // TODO: This should be optimized/cached, as calling Activator.CreateInstance is slow
                var mediaWithCropsType = typeof(MediaWithCrops<>).MakeGenericType(mediaItem.GetType());
                var mediaWithCrops = (MediaWithCrops)Activator.CreateInstance(mediaWithCropsType, mediaItem, _publishedValueFallback, localCrops);

                if (!config.Multiple) {
                    // Short-circuit on single item
                    break;
                }
                

                // If the configuration doesn't specify a value type, we just create a new ImagePickerImage
                if (valueType == null) {
                    items.Add(new ImagePickerWithCropsImage(mediaWithCrops, config));
                    continue;
                }

                // If the selected type has a constructor with an ImagePickerConfiguration as the second parameter, we choose that constructor
                if (HasConstructor<MediaWithCrops, ImagePickerWithCropsConfiguration>(valueType)) {
                    items.Add(ActivatorUtilities.CreateInstance(_serviceProvider, valueType, mediaWithCrops, config));
                    continue;
                }

                items.Add(ActivatorUtilities.CreateInstance(_serviceProvider, valueType, mediaWithCrops));
            }

            // Return the item(s) with the correct value type
            valueType ??= typeof(ImagePickerWithCropsImage);
            return config.Multiple ? items.Cast(valueType).ToList(valueType) : items.FirstOrDefault();

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

            Type valueType = propertyType.DataType.ConfigurationAs<ImagePickerWithCropsConfiguration>()?.ValueType ?? typeof(ImagePickerWithCropsImage);

            return isMultiple ? typeof(IEnumerable<>).MakeGenericType(valueType) : valueType;

        }

        /// <summary>
        /// Returns whether the specified <paramref name="dataType"/> represents a multi picker.
        /// </summary>
        /// <param name="dataType">The data type.</param>
        /// <returns><c>true</c> if <paramref name="dataType"/> is a multi picker; otherwise <c>false</c>.</returns>
        private static bool IsMultiPicker(PublishedDataType dataType) {
            return dataType.ConfigurationAs<ImagePickerWithCropsConfiguration>()?.Multiple ?? false;
        }

        /// <summary>
        /// Returns whether the specified <paramref name="type"/> contains at least one constructor where the first
        /// parameter is of type <typeparamref name="T1"/> and the second parameter is of type <typeparamref name="T2"/>.
        ///
        /// Any additional parameter the constructors may have are not relevant here, as their values will be attempted
        /// to be solved using dependency injection.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter.</typeparam>
        /// <typeparam name="T2">The type of the second parameter.</typeparam>
        /// <param name="type">The type to check.</param>
        /// <returns><c>true</c> if at least one constructor is a match; otherwise <c>false</c>.</returns>
        private static bool HasConstructor<T1, T2>(Type type) {
            return type
                .GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .Select(cs => cs.GetParameters())
                .Any(ps => ps.Length >= 2 && ps[0].ParameterType == typeof(T1) && ps[1].ParameterType == typeof(T2));
        }

        #endregion

        internal static IEnumerable<MediaWithCropsDto> Deserialize(IJsonSerializer jsonSerializer, object value) {
            var rawJson = value is string str ? str : value?.ToString();
            if (string.IsNullOrWhiteSpace(rawJson)) {
                yield break;
            }

            if (!rawJson.DetectIsJson()) {
                // Old comma seperated UDI format
                foreach (var udiStr in rawJson.Split(Constants.CharArrays.Comma)) {
                    if (UdiParser.TryParse(udiStr, out GuidUdi udi)) {
                        yield return new MediaWithCropsDto {
                            Key = Guid.NewGuid(),
                            MediaKey = udi.Guid,
                            Crops = Enumerable.Empty<ImageCropperValue.ImageCropperCrop>(),
                            FocalPoint = new ImageCropperValue.ImageCropperFocalPoint {
                                Left = 0.5m,
                                Top = 0.5m
                            }
                        };
                    }
                }
            } else {
                // New JSON format
                foreach (var dto in jsonSerializer.Deserialize<IEnumerable<MediaWithCropsDto>>(rawJson)) {
                    yield return dto;
                }
            }
        }

        [DataContract]
        internal class MediaWithCropsDto {
            [DataMember(Name = "key")]
            public Guid Key { get; set; }

            [DataMember(Name = "mediaKey")]
            public Guid MediaKey { get; set; }

            [DataMember(Name = "crops")]
            public IEnumerable<ImageCropperValue.ImageCropperCrop> Crops { get; set; }

            [DataMember(Name = "focalPoint")]
            public ImageCropperValue.ImageCropperFocalPoint FocalPoint { get; set; }
        }        

    }

}