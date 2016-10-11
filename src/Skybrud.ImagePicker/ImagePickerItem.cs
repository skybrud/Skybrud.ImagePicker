using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;
using Skybrud.LinkPicker;
using Umbraco.Core.Models;
using Umbraco.Web;

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
        public JObject JObject { get; private set; }

        /// <summary>
        /// Gets a reference to the selected image.
        /// </summary>
        public IPublishedContent Image { get; private set; }

        /// <summary>
        /// Gets whether an image has been selected for this item.
        /// </summary>
        public bool HasImage {
            get { return Image != null; }
        }

        /// <summary>
        /// Gets the title of this item.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Gets whether a title has been specified for this item.
        /// </summary>
        public bool HasTitle {
            get { return !String.IsNullOrWhiteSpace(Title); }
        }

        /// <summary>
        /// Gets the description of this item.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets whether a description has been specified for this item.
        /// </summary>
        public bool HasDescription {
            get { return !String.IsNullOrWhiteSpace(Description); }
        }

        /// <summary>
        /// Gets a reference to the selected <see cref="LinkPickerItem"/>.
        /// </summary>
        public LinkPickerItem Link { get; private set; }

        /// <summary>
        /// Gets whether a valid link has been specified for this item.
        /// </summary>
        public bool HasLink {
            get { return Link != null && Link.IsValid; }
        }

        /// <summary>
        /// Gets whether the item is valid. Since an item can exist without a title, description or link, but must have
        /// an image, this property equals calling <see cref="HasImage"/>.
        /// </summary>
        public bool IsValid {
            get { return HasImage; }
        }

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
        /// Initializes a new image picker item based on the specified <code>image</code>, <code>title</code>, <code>description</code> and <code>link</code>.
        /// </summary>
        /// <param name="image">An instance of <see cref="IPublishedContent"/> representing the selected image.</param>
        /// <param name="title">The title of the item.</param>
        /// <param name="description">The description of the item.</param>
        /// <param name="link">An instance of <see cref="LinkPickerItem"/> representing the link of the item.</param>
        public ImagePickerItem(IPublishedContent image, string title, string description, LinkPickerItem link) {
            Image = image;
            Title = title;
            Description = description;
            Link = link ?? LinkPickerItem.Parse(new JObject());
        }

        /// <summary>
        /// Initializes a new image picker item based on the specified <see cref="JObject"/>.
        /// </summary>
        /// <param name="obj">An instanceo of <see cref="JObject"/> representing the item.</param>
        protected ImagePickerItem(JObject obj) {

            int imageId = obj.GetInt32("imageId");
            IPublishedContent image = imageId > 0 && UmbracoContext.Current != null ? UmbracoContext.Current.MediaCache.GetById(imageId) : null;

            JObject = obj;
            Image = image;
            Title = obj.GetString("title") ?? "";
            Description = obj.GetString("description") ?? "";
            Link = obj.GetObject("link", LinkPickerItem.Parse) ?? LinkPickerItem.Parse(new JObject());
        
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <see cref="JObject"/> into an instance of <see cref="ImagePickerItem"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>Returns an instance of <see cref="ImagePickerItem"/>, or <code>null</code> if <code>obj</code> is
        /// <code>null</code>.</returns>
        public static ImagePickerItem Parse(JObject obj) {

            if (obj == null) return null;

            int imageId = obj.GetInt32("imageId");
            IPublishedContent image = imageId > 0 && UmbracoContext.Current != null ? UmbracoContext.Current.MediaCache.GetById(imageId) : null;
            
            return new ImagePickerItem {
                JObject = obj,
                Image = image,
                Title = obj.GetString("title") ?? "",
                Description = obj.GetString("description") ?? "",
                Link = obj.GetObject("link", LinkPickerItem.Parse) ?? LinkPickerItem.Parse(new JObject())
            };
        
        }

        #endregion
    
    }

}