using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Skybrud.LinkPicker;
using System.Web.Mvc;
using Skybrud.LinkPicker.Extensions.Json;
using StackExchange.Profiling.Data;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Skybrud.ImagePicker {

    public class ImagePickerItem {
        #region Properties

        public IPublishedContent Image { get; private set; }

        public string AltText { get; private set; }

        public MvcHtmlString Description { get; private set; }

        public LinkPickerItem Link { get; private set; }

        #endregion

        #region Constructors

        internal ImagePickerItem() {}

        #endregion

        #region Static methods

        public static ImagePickerItem Parse(JObject obj) {

            if (obj == null) return null;

            int id = obj.GetInt32("image");  //not sure

            IPublishedContent image = UmbracoContext.Current.MediaCache.GetById(id);

            if (image == null) return null;

            return new ImagePickerItem {
                Image = image,
                AltText = obj.GetString("alttext"),
                Description = new MvcHtmlString(obj.GetString("description")),
                Link = null //TODO:
            };
        }

        #endregion
    }
}
