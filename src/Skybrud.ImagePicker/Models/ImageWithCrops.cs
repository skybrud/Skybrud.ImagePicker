using Newtonsoft.Json;
using Skybrud.ImagePicker.PropertyEditors.ImagePickerWithCrops;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Extensions;

namespace Skybrud.ImagePicker.Models {
    /// <summary>
    /// Image class that extends MediaWithCrops with image specific properties
    /// </summary>
    public class ImageWithCrops {
        #region Properties

        /// <summary>
        /// Gets the IPublishedContent reference to the media node
        /// </summary>
        [JsonIgnore]
        public IPublishedContent Media { get; }

        /// <summary>
        /// The media int id
        /// </summary>
        [JsonProperty("id", Order = -500)]
        public virtual int Id => Media.Id;

        /// <summary>
        /// The width of the media
        /// </summary>
        [JsonProperty("width", Order = -450)]
        public virtual int Width { get; }

        /// <summary>
        /// The height of the media
        /// </summary>
        [JsonProperty("height", Order = -400)]
        public virtual int Height { get; }

        /// <summary>
        /// The url for the media
        /// </summary>
        [JsonProperty("url", Order = -350)]
        public virtual string Url => Media.Url();

        /// <summary>
        /// The generated crop url
        /// </summary>
        [JsonProperty("cropUrl", Order = -300)]
        public virtual string CropUrl { get; }

        /// <summary>
        /// Gets the alt text if an "altText" property exists on the media
        /// </summary>
        [JsonProperty("altText", Order = -250)]
        public string AlternativeText => Media.Value<string>("altText") ?? string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new image class
        /// </summary>
        /// <param name="content">The MediaWithCrops media model returned from mediapicker v3</param>
        /// <param name="config">The ImagePicker config</param>
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