using System.Collections.Generic;
using Umbraco.Cms.Core.Manifest;

namespace Skybrud.ImagePicker.Manifests {

    /// <inheritdoc />
    public class ImagePickerManifestFilter : IManifestFilter {

        /// <inheritdoc />
        public void Filter(List<PackageManifest> manifests) {
            manifests.Add(new PackageManifest {
                PackageName = ImagePickerPackage.SmidgeAlias,
                BundleOptions = BundleOptions.Independent,
                Scripts = new[] {
                    $"/App_Plugins/{ImagePickerPackage.Alias}/Scripts/Controllers/ImageModelPicker.js",
                    $"/App_Plugins/{ImagePickerPackage.Alias}/Scripts/Controllers/ImageModelPickerOverlay.js",
                    $"/App_Plugins/{ImagePickerPackage.Alias}/Scripts/Controllers/TypeConverter.js"
                },
                Stylesheets = new[] {
                    $"/App_Plugins/{ImagePickerPackage.Alias}/Styles/Default.css"
                }
            });
        }

    }

}