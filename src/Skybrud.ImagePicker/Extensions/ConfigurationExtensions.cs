using System.Collections.Generic;
using System.Linq;
using Skybrud.ImagePicker.PropertyEditors.ImagePickerWithCrops;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;

namespace Skybrud.ImagePicker.Extensions {
    public static class ConfigurationExtensions {

        // Based on https://github.com/umbraco/Umbraco-CMS/blob/v9/contrib/src/Umbraco.Infrastructure/Extensions/MediaPicker3ConfigurationExtensions.cs
        public static void ApplyConfiguration(this ImageCropperValue imageCropperValue, ImagePickerWithCropsConfiguration configuration) {
            var crops = new List<ImageCropperValue.ImageCropperCrop>();

            var configuredCrops = configuration?.Crops;
            if (configuredCrops != null) {
                foreach (var configuredCrop in configuredCrops) {
                    var crop = imageCropperValue.Crops?.FirstOrDefault(x => x.Alias == configuredCrop.Alias);

                    crops.Add(new ImageCropperValue.ImageCropperCrop {
                        Alias = configuredCrop.Alias,
                        Width = configuredCrop.Width,
                        Height = configuredCrop.Height,
                        Coordinates = crop?.Coordinates
                    });
                }
            }

            imageCropperValue.Crops = crops;
        }
    }
}
