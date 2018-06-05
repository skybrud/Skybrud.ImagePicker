using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.LinkPicker;
using Skybrud.LinkPicker.Json.Converters;
using Umbraco.Core.Models;

namespace Skybrud.ImagePicker {

    /// <summary>
    /// Class representing an item in an image picker list.
    /// </summary>
    public class ImagePickerItem {

        #region Properties

        /// <summary>
        /// Gets a reference to the <see cref="JObject"/> the item was parsed from.
        /// </summary>
        [JsonIgnore]
        public JObject JObject { get; }

        /// <summary>
        /// Gets a reference to the selected image.
        /// </summary>
        [JsonProperty("image")]
        public ImagePickerImage Image { get; private set; }

        /// <summary>
        /// Gets whether an image has been selected for this item.
        /// </summary>
        [JsonIgnore]
        public bool HasImage => Image != null;

        /// <summary>
        /// Gets the title of this item.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; private set; }

        /// <summary>
        /// Gets whether a title has been specified for this item.
        /// </summary>
        [JsonIgnore]
        public bool HasTitle => !String.IsNullOrWhiteSpace(Title);

        /// <summary>
        /// Gets the description of this item.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; private set; }

        /// <summary>
        /// Gets whether a description has been specified for this item.
        /// </summary>
        [JsonIgnore]
        public bool HasDescription => !String.IsNullOrWhiteSpace(Description);

        /// <summary>
        /// Gets a reference to the selected <see cref="LinkPickerItem"/>.
        /// </summary>
        [JsonProperty("link")]
        [JsonConverter(typeof(LinkPickerItemConverter))]
        public LinkPickerItem Link { get; private set; }

		[JsonProperty("noCrop")]
	    public bool NoCrop { get; private set; }



	    /// <summary>
        /// Gets whether a valid link has been specified for this item.
        /// </summary>
        [JsonIgnore]
        public bool HasLink => Link != null && Link.IsValid;

        /// <summary>
        /// Gets whether the item is valid. Since an item can exist without a title, description or link, but must have
        /// an image, this property equals calling <see cref="HasImage"/>.
        /// </summary>
        [JsonIgnore]
        public bool IsValid => HasImage;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes an empty image picker item.
        /// </summary>
        public ImagePickerItem() {
            Title = "";
            Description = "";
        }

        /// <summary>
        /// Initializes a new image picker item based on the specified <paramref name="image"/>, <paramref name="title"/>,
        /// <paramref name="description"/> and <paramref name="link"/>.
        /// </summary>
        /// <param name="image">An instance of <see cref="IPublishedContent"/> representing the selected image.</param>
        /// <param name="title">The title of the item.</param>
        /// <param name="description">The description of the item.</param>
        /// <param name="link">An instance of <see cref="LinkPickerItem"/> representing the link of the item.</param>
        public ImagePickerItem(ImagePickerImage image, string title, string description, LinkPickerItem link) {
            Image = image;
            Title = title;
            Description = description;
            Link = link ?? LinkPickerItem.Parse(new JObject());
        }

	    /// <summary>
	    /// Initializes a new image picker item based on the specified <paramref name="image"/>, <paramref name="title"/>,
	    /// <paramref name="description"/> and <paramref name="link"/>.
	    /// </summary>
	    /// <param name="image">An instance of <see cref="IPublishedContent"/> representing the selected image.</param>
	    /// <param name="title">The title of the item.</param>
	    /// <param name="description">The description of the item.</param>
	    /// <param name="link">An instance of <see cref="LinkPickerItem"/> representing the link of the item.</param>
	    /// <param name="noCrop">The NoCrop property value</param>
	    public ImagePickerItem(ImagePickerImage image, string title, string description, LinkPickerItem link, bool noCrop) {
		    Image = image;
		    Title = title;
		    Description = description;
		    Link = link ?? LinkPickerItem.Parse(new JObject());
		    NoCrop = noCrop;
	    }

        /// <summary>
        /// Initializes a new image picker item based on the specified <see cref="JObject"/>.
        /// </summary>
        /// <param name="obj">An instanceo of <see cref="JObject"/> representing the item.</param>
        protected ImagePickerItem(JObject obj) {
            JObject = obj;
            Image = obj.GetInt32("imageId", ImagePickerImage.GetFromId);
            Title = obj.GetString("title") ?? "";
            Description = obj.GetString("description") ?? "";
            Link = obj.GetObject("link", LinkPickerItem.Parse) ?? LinkPickerItem.Parse(new JObject());
	        NoCrop = obj.GetBoolean("nocrop");
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <see cref="JObject"/> into an instance of <see cref="ImagePickerItem"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>An instance of <see cref="ImagePickerItem"/>, or <c>null</c> if <paramref name="obj"/> is
        /// <c>null</c>.</returns>
        public static ImagePickerItem Parse(JObject obj) {
            return obj == null ? null : new ImagePickerItem(obj);
        }

        #endregion
    
    }

}