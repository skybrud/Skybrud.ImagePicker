using Skybrud.ImagePicker.Tracking;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace Skybrud.ImagePicker.Composers {
    
    public class ImagePickerComposer : IUserComposer {
        
        public void Compose(Composition composition) {
            composition.DataValueReferenceFactories().Append<MediaTracking>();
        }

    }

}