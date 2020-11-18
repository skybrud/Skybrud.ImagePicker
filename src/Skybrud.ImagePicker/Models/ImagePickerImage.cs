using Newtonsoft.Json;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Skybrud.ImagePicker.Models {

    public class ImagePickerImage {

        #region Properties

        [JsonIgnore]
        public IPublishedContent Media { get; }

        [JsonProperty("id", Order = -500)]
        public virtual int Id => Media.Id;

        [JsonProperty("width", Order = -450)]
        public virtual int Width => Media.Value<int>(Constants.Conventions.Media.Width);

        [JsonProperty("height", Order = -400)]
        public virtual int Height => Media.Value<int>(Constants.Conventions.Media.Height);

        [JsonProperty("url", Order = -350)]
        public virtual string Url => Media.Url;

        [JsonProperty("cropUrl", Order = -300)]
        public virtual string CropUrl => Media.GetCropUrl(Width, Height, preferFocalPoint: true, imageCropMode: ImageCropMode.Crop);

        [JsonProperty("altText", Order = -250)]
        public string AlternativeText => Media.Value<string>("altText") ?? string.Empty;

        #endregion

        #region Constructors

        public ImagePickerImage(IPublishedContent content) {
            Media = content;
        }

        #endregion

    }

}