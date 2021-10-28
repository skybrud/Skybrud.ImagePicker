using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace Skybrud.ImagePicker.Controllers.Api {

    [AngularJsonOnlyConfiguration]
    [PluginController("Skybrud")]
    public class ImagePickerController : UmbracoAuthorizedApiController {

        #region Public API methods

        [HttpGet]
        public IEnumerable<object> GetImageModels() {

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {

                AssemblyName assemblyName = assembly.GetName();
                    
                switch (assemblyName.Name) {
                    case "Skybrud.ImagePicker":
                    case "Skybrud.LinkPicker":
                    case "Umbraco.Core":
                    case "Umbraco.Web":
                        continue;
                }

                foreach (Type type in assembly.GetTypes()) {

                    ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);

                    foreach (ConstructorInfo constructor in constructors) {

                        ParameterInfo[] parameters = constructor.GetParameters();

                        if (parameters.Length == 0) continue;
                        if (parameters.Length > 1 && parameters.Skip(1).Any(x => x.ParameterType.IsValueType)) continue;
                        if (parameters[0].ParameterType != typeof(IPublishedContent)) continue;
                        
                        yield return Map(type);

                        break;

                    }
                    
                }

            }

        }

        #endregion

        #region Private helper methods

        private JObject Map(Type type) {

            JObject json = new JObject {
                { "assembly", type.Assembly.FullName },
                { "key", type.AssemblyQualifiedName },
                { "icon", $"icon-box color-{type.Assembly.FullName.Split('.')[0].ToLower()}" },
                { "name", type.Name },
                { "description", type.AssemblyQualifiedName?.Split(new string[] { ", Version" }, StringSplitOptions.None)[0] + ".dll" }
            };

            return json;

        }

        #endregion

    }

}