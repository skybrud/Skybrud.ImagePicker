using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.ImagePicker {
    
    internal class ImagePickerPropertyValueConverter : IPropertyValueConverter {
        
        public bool IsConverter(PublishedPropertyType propertyType) {
            return propertyType.PropertyEditorAlias == "Skybrud.ImagePicker";
        }

        public object ConvertDataToSource(PublishedPropertyType propertyType, object data, bool preview) {
            return ImagePickerList.Deserialize(data as string);
        }

        public object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview) {
            return source;
        }

        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview) {
            return null;
        }
    
    }

}