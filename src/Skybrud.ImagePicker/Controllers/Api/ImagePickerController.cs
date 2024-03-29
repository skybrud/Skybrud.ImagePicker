﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Skybrud.ImagePicker.Converters.ImageWithCrops;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.BackOffice.Controllers;
using Umbraco.Cms.Web.Common.Attributes;
using Umbraco.Cms.Web.Common.Filters;

namespace Skybrud.ImagePicker.Controllers.Api {

    /// <summary>
    /// Umbraco authorized controller for the backoffice plugins
    /// </summary>
    [AngularJsonOnlyConfiguration]
    [PluginController("Skybrud")]
    public class ImagePickerController : UmbracoAuthorizedApiController {
        
        private readonly ImageWithCropsTypeConverterCollection _imageWithCropsTypeConverterCollection;

        #region Constructors

        /// <summary>
        /// Initializes a new instance based on the specified dependency.
        /// </summary>
        /// <param name="imageWithCropsTypeConverterCollection"></param>
        public ImagePickerController(ImageWithCropsTypeConverterCollection imageWithCropsTypeConverterCollection) {
            _imageWithCropsTypeConverterCollection = imageWithCropsTypeConverterCollection;
        }

        #endregion

        #region Public API methods

        /// <summary>
        /// Gets all models that can be cast to.
        /// </summary>
        /// <returns>Collection of custom types that can be cast to.</returns>
        [HttpGet]
        public IEnumerable<object> GetImageModels(string editor = null) {

            // Determine the type of the input type that custom model constructors should have as their first parameter
            Type inputModelType = editor == "v3" ? typeof(MediaWithCrops) : typeof(IPublishedContent);

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {

                AssemblyName assemblyName = assembly.GetName();

                switch (assemblyName.Name) {
                    case "Skybrud.LinkPicker":
                    case "Skybrud.ImagePicker":
                    case "Skybrud.Umbraco.Feedback":
                    case "Skybrud.Umbraco.Spa":
                    case "Umbraco.Core":
                    case "Umbraco.Infrastructure":
                    case "Umbraco.PublishedCache.NuCache":
                    case "Umbraco.Web.Website":
                    case "Umbraco.Cms.Web.Common.PublishedModels":
                        continue;
                }

                foreach (Type type in assembly.GetTypes()) {

                    ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);

                    foreach (ConstructorInfo constructor in constructors) {

                        ParameterInfo[] parameters = constructor.GetParameters();

                        if (parameters.Length == 0) continue;
                        if (parameters.Length > 1 && parameters.Skip(1).Any(x => !IsValidType(x))) continue;
                        if (parameters[0].ParameterType != inputModelType) continue;

                        yield return Map(type);

                        break;

                    }

                }

            }

        }

        /// <summary>
        /// Returns a list of all type converters for the iamge with crops property editor.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object GetTypeConverters(string editor = null) {
            return editor switch {
                "v3" => _imageWithCropsTypeConverterCollection
                    .ToArray()
                    .Select(x => new {
                        assembly = x.GetType().Assembly.FullName, key = x.GetType().AssemblyQualifiedName, name = x.Name
                    }),
                _ => Array.Empty<object>()
            };
        }

        #endregion

        #region Private helper methods

        private static bool IsValidType(ParameterInfo parameter) {
            return IsValidType(parameter.ParameterType);
        }

        private static bool IsValidType(Type type) {
            return !type.IsValueType && type != typeof(IPublishedValueFallback);
        }

        private static JObject Map(Type type) {

            JObject json = new() {
                { "assembly", type.Assembly.FullName },
                { "key", type.AssemblyQualifiedName },
                { "icon", $"icon-box color-{type.Assembly.FullName?.Split('.')[0].ToLower()}" },
                { "name", type.Name },
                { "description", type.AssemblyQualifiedName?.Split(new[] { ", Version" }, StringSplitOptions.None)[0] + ".dll" }
            };

            return json;

        }

        #endregion

    }

}