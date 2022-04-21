using System;
using Skybrud.Essentials.Reflection;
using Skybrud.Essentials.Strings.Extensions;
using Skybrud.ImagePicker.Composers;
using Umbraco.Cms.Core.Semver;

namespace Skybrud.ImagePicker {

    /// <summary>
    /// Static class with various information and constants about the package.
    /// </summary>
    public static class ImagePickerPackage {

        /// <summary>
        /// Gets the alias of the package.
        /// </summary>
        public const string Alias = "Skybrud.ImagePicker";

        /// <summary>
        /// Gets the alias of the package.
        /// </summary>
        internal static readonly string SmidgeAlias = Alias.ToLower().ToKebabCase();

        /// <summary>
        /// Gets the friendly name of the package.
        /// </summary>
        public const string Name = "Skybrud Image Picker";

        /// <summary>
        /// Gets the version of the package.
        /// </summary>
        public static readonly Version Version = typeof(ImagePickerPackage).Assembly.GetName().Version;

        /// <summary>
        /// Gets the semantic version of the package.
        /// </summary>
        public static readonly SemVersion SemVersion = SemVersion.Parse(ReflectionUtils.GetInformationalVersion<ImagePickerComposer>());

        /// <summary>
        /// Gets the URL of the GitHub repository for this package.
        /// </summary>
        public const string GitHubUrl = "https://github.com/skybrud/Skybrud.Umbraco.Redirects";

        /// <summary>
        /// Gets the URL of the issue tracker for this package.
        /// </summary>
        public const string IssuesUrl = "https://github.com/skybrud/Skybrud.Umbraco.Redirects/issues";

        /// <summary>
        /// Gets the URL of the documentation for this package.
        /// </summary>
        public const string DocumentationUrl = "https://packages.skybrud.dk/skybrud.umbraco.redirects/";

    }

}