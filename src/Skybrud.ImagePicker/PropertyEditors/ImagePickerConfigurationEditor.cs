using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Umbraco.Core.PropertyEditors;

namespace Skybrud.ImagePicker.PropertyEditors {

    public class ImagePickerConfigurationEditor : ConfigurationEditor<ImagePickerConfiguration> {

        public override IDictionary<string, object> ToValueEditor(object configuration) {

            var d = base.ToValueEditor(configuration);

            JToken t1 = JToken.FromObject(d);
            
            d["idType"] = "udi";
            d["disableFolderSelect"] = "true";
            d["onlyImages"] = "true";

            throw new Exception(t1 + "\r\n\r\n" + JToken.FromObject(d) + "");

            return d;

        }

    }

}