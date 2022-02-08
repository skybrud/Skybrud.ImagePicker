using Skybrud.ImagePicker.Factories;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Skybrud.ImagePicker.Composers {

    /// <summary>
    /// Composer to run our reference factories when the site starts up
    /// </summary>
    public class ImagePickerComposer : IComposer {

        /// <summary>
        /// Append reference factories on startup
        /// </summary>
        /// <param name="builder">Umbracos own injected builder that runs on startup</param>
        public void Compose(IUmbracoBuilder builder) {
            builder.DataValueReferenceFactories().Append<ImageReferenceFactory>();
            builder.DataValueReferenceFactories().Append<ImageWithCropsReferenceFactory>();
        }
    }

}