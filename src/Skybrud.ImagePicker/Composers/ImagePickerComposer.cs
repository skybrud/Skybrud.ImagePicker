using Skybrud.ImagePicker.Factories;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Skybrud.ImagePicker.Composers {

    public class ImagePickerComposer : IComposer {        

        public void Compose(IUmbracoBuilder builder) {
            builder.DataValueReferenceFactories().Append<ImageReferenceFactory>();
        }
    }

}