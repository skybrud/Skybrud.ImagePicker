using Newtonsoft.Json;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Skybrud.ImagePicker {
    
    /// <summary>
    /// Class with information about a selected image.
    /// </summary>
    public class ImagePickerImage {
        
        #region Properties

        /// <summary>
        /// Gets a reference to the underlying instance of <see cref="IPublishedContent"/>.
        /// </summary>
        [JsonIgnore]
        public IPublishedContent Image { get; }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; private set; }
        
        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; private set; }

        /// <summary>
        /// Gets the URL of the image.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; private set; }

        /// <summary>
        /// Gets the crop URL of the image.
        /// </summary>
        [JsonProperty("cropUrl")]
        public string CropUrl { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified <paramref name="content"/>.
        /// </summary>
        /// <param name="content">An instance of <see cref="IPublishedContent"/> representing the selected image.</param>
        protected ImagePickerImage(IPublishedContent content) {
            Image = content;
            Width = content.GetPropertyValue<int>(global::Umbraco.Core.Constants.Conventions.Media.Width);
            Height = content.GetPropertyValue<int>(global::Umbraco.Core.Constants.Conventions.Media.Height);
            Url = content.Url;
            CropUrl = content.GetCropUrl(Width, Height, preferFocalPoint: true, imageCropMode: ImageCropMode.Crop);
        }

        #endregion

        #region Member methods

        /// <summary>
        /// Return a crop URL of the image based on the specified parameters.
        /// </summary>
        /// <param name="width">The width of the output image.</param>
        /// <param name="height">The height of the output image.</param>
        /// <param name="propertyAlias">Property alias of the property containing the Json data.</param>
        /// <param name="cropAlias">The crop alias.</param>
        /// <param name="quality">Quality percentage of the output image.</param>
        /// <param name="imageCropMode">The image crop mode.</param>
        /// <param name="imageCropAnchor">The image crop anchor.</param>
        /// <param name="preferFocalPoint">Use focal point, to generate an output image using the focal point instead of the predefined crop.</param>
        /// <param name="useCropDimensions">Use crop dimensions to have the output image sized according to the predefined crop sizes, this will override the width and height parameters.</param>
        /// <param name="cacheBuster">Add a serialised date of the last edit of the item to ensure client cache refresh when updated.</param>
        /// <param name="furtherOptions">The further options.</param>
        /// <param name="ratioMode">Use a dimension as a ratio.</param>
        /// <param name="upScale">If the image should be upscaled to requested dimensions.</param>
        /// <returns>Returns a string with the crop URL.</returns>
        public string GetCropUrl(int? width = null, int? height = null, string propertyAlias = "umbracoFile", string cropAlias = null, int? quality = null, ImageCropMode? imageCropMode = null, ImageCropAnchor? imageCropAnchor = null, bool preferFocalPoint = false, bool useCropDimensions = false, bool cacheBuster = true, string furtherOptions = null, ImageCropRatioMode? ratioMode = null, bool upScale = true) {
            return Image.GetCropUrl(width, height, propertyAlias, cropAlias, quality, imageCropMode, imageCropAnchor, preferFocalPoint, useCropDimensions, cacheBuster, furtherOptions, ratioMode, upScale);
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <paramref name="content"/> into an instance of <see cref="ImagePickerImage"/>.
        /// </summary>
        /// <param name="content">The instance of <see cref="ImagePickerImage"/> to be parsed.</param>
        /// <returns>Returns an instance of <see cref="ImagePickerImage"/>, or <code>null</code> if
        /// <paramref name="content"/> is <code>null</code>.</returns>
        public static ImagePickerImage GetFromContent(IPublishedContent content) {
            return content == null ? null : new ImagePickerImage(content);
        }

        /// <summary>
        /// Gets a reference to the image with the specified <paramref name="imageId"/>.
        /// </summary>
        /// <param name="imageId">The ID of the image..</param>
        /// <returns>Returns an instance of <see cref="ImagePickerImage"/>, or <code>null</code> if the image could not
        /// be found.</returns>
        public static ImagePickerImage GetFromId(int imageId) {
            return UmbracoContext.Current == null ? null : GetFromContent(UmbracoContext.Current.MediaCache.GetById(imageId));
        }

        #endregion

    }

}