using Newtonsoft.Json;
using Umbraco.Core;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Skybrud.ImagePicker.Models {

    public class ImagePickerImage {

        [JsonProperty("id")]
        public int Id { get; }

        [JsonProperty("width")]
        public int Width { get; }

        [JsonProperty("height")]
        public int Height { get; }

        [JsonProperty("url")]
        public string Url { get; }

        [JsonProperty("cropUrl")]
        public string CropUrl { get; }

        [JsonProperty("altText")]
        public string AlternativeText { get; }

        public ImagePickerImage(IPublishedContent content) {
            Id = content.Id;
            Width = content.Value<int>(Constants.Conventions.Media.Width);
            Height = content.Value<int>(Constants.Conventions.Media.Height);
            Url = content.Url;
            CropUrl = content.GetCropUrl(Width, Height, preferFocalPoint: true, imageCropMode: ImageCropMode.Crop);
            AlternativeText = content.Value<string>("altText") ?? string.Empty;
        }

    }

}