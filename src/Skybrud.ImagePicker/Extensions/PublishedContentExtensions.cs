using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Skybrud.ImagePicker.Extensions {
    
    /// <summary>
    /// Various extension methods for <see cref="IPublishedContent"/> and the image picker.
    /// </summary>
    public static class PublishedContentExtensions {
        
        /// <summary>
        /// Gets the first item from an <see cref="ImagePickerList"/> model from the property with the specified
        /// <code>propertyAlias</code>. If property isn't an image picker (or the list is empty), an empty item will be
        /// returned instead.
        /// </summary>
        /// <param name="content">An instance of <see cref="IPublishedContent"/> to read the property from.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        /// <returns>An instance of <see cref="ImagePickerItem"/>.</returns>
        public static ImagePickerItem GetImagePickerItem(this IPublishedContent content, string propertyAlias) {
            var list = content.GetPropertyValue(propertyAlias) as ImagePickerList;
            ImagePickerItem item = list?.Items.FirstOrDefault();
            return item ?? new ImagePickerItem();
        }

        /// <summary>
        /// Gets an instance of <see cref="ImagePickerList"/> from the property with the specified <code>propertyAlias</code>.
        /// </summary>
        /// <param name="content">An instance of <see cref="IPublishedContent"/> to read the property from.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        /// <returns>An instance of <see cref="ImagePickerList"/>.</returns>
        public static ImagePickerList GetImagePickerList(this IPublishedContent content, string propertyAlias) {
            return content?.GetPropertyValue<ImagePickerList>(propertyAlias) ?? new ImagePickerList();
        }
    }

}