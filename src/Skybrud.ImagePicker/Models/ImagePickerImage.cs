using Newtonsoft.Json;
using Skybrud.ImagePicker.PropertyEditors;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace Skybrud.ImagePicker.Models {

    public class ImagePickerImage {

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
        public virtual string Url => Media.Url;

        [JsonProperty("cropUrl", Order = -300)]
        public virtual string CropUrl { get; }

        [JsonProperty("altText", Order = -250)]
        public string AlternativeText => Media.Value<string>("altText") ?? string.Empty;

        #endregion

        #region Constructors

        public ImagePickerImage(IPublishedContent content) : this(content, null) { }

        public ImagePickerImage(IPublishedContent content, ImagePickerConfiguration config) {

            // Make sure we have a configuration
            config ??= new ImagePickerConfiguration();

            // Get the width and height
            int width = content.Value<int>(Constants.Conventions.Media.Width);
            int height = content.Value<int>(Constants.Conventions.Media.Height);
            
            // Populate the properties
            Media = content;
            Width = width;
            Height = height;
            CropUrl = content.GetCropUrl(width, height, preferFocalPoint: config.PreferFocalPoint, imageCropMode: config.CropMode);

        }

        #endregion

    }

}