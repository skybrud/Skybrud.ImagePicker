using Newtonsoft.Json;
using Skybrud.ImagePicker.PropertyEditors.ImagePickerWithCrops;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Skybrud.ImagePicker.Models {
    public class ImageWithCrops {
        #region Properties

        [JsonIgnore]
        public IPublishedContent Media { get; }

        [JsonProperty("id", Order = -500)]
        public virtual int Id => Media.Id;

        [JsonProperty("width", Order = -450)]
        public virtual int Width { get; }

        [JsonProperty("height", Order = -400)]
        public virtual int Height { get; }

        [JsonProperty("url", Order = -350)]
        public virtual string Url => Media.Url();

        [JsonProperty("cropUrl", Order = -300)]
        public virtual string CropUrl { get; }

        [JsonProperty("altText", Order = -250)]
        public string AlternativeText => Media.Value<string>("altText") ?? string.Empty;

        #endregion

        #region Constructors

        public ImageWithCrops(MediaWithCrops content, ImagePickerWithCropsConfiguration config) {

            int width = content.Value<int>(Constants.Conventions.Media.Width);
            int height = content.Value<int>(Constants.Conventions.Media.Height);

            Media = content;
            Width = width;
            Height = height;
            CropUrl = content.GetCropUrl(width, height, preferFocalPoint: ImagePickerWithCropsConfiguration.PreferFocalPoint, imageCropMode: ImagePickerWithCropsConfiguration.CropMode);

        }

        #endregion
    }
}