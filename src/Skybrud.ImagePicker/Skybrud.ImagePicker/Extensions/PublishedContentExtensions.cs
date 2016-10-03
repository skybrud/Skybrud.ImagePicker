using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Skybrud.ImagePicker.Extensions {
    /// <summary>
    ///     Various extension methods for <code>IPublishedContent</code> and the LinkPicker.
    /// </summary>
    public static class PublishedContentExtensions {
        /// <summary>
        ///     Gets the first link item from a ImagePicker model from the property with the specified
        ///     <code>propertyAlias</code>. If property isn't a link picker (or the list is empty),
        ///     an empty item will be returned instead.
        /// </summary>
        /// <param name="content">The published content to read the property from.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static ImagePickerItem GetImagePickerItem(this IPublishedContent content, string propertyAlias) {
            var list = content.GetPropertyValue(propertyAlias) as ImagePickerList;
            ImagePickerItem item = (list == null ? null : list.Items.FirstOrDefault());
            return item ?? new ImagePickerItem();
        }

        /// <summary>
        ///     Gets the ImagePicker model from the property with the specified <code>propertyAlias</code>.
        /// </summary>
        /// <param name="content">The published content to read the property from.</param>
        /// <param name="propertyAlias">The alias of the property.</param>
        public static ImagePickerList GetImagePickerList(this IPublishedContent content, string propertyAlias) {
            return (content == null ? null : content.GetPropertyValue<ImagePickerList>(propertyAlias)) ??
                   new ImagePickerList {
                       Items = new ImagePickerItem[0]
                   };
        }
    }
}