using System;
using System.Collections.Generic;
using System.Linq;
using Skybrud.ImagePicker.Models;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;
using Umbraco.Cms.Core.Serialization;
using Umbraco.Extensions;

namespace Skybrud.ImagePicker.Json {
    /// <summary>
    /// Helper class to deserialize a json blob into the MediaWithCrops model
    /// </summary>
    public class MediaWithCropsDeserializer {

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
    }
}
