using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.LinkPicker.Extensions.Json;

namespace Skybrud.ImagePicker {
    public class ImagePickerList {
        #region Properties

        [JsonIgnore]
        public JObject JObject { get; private set; }

        [JsonProperty("title")]
        public string Title { get; private set; }

        [JsonIgnore]
        public bool HasTitle {
            get { return !String.IsNullOrWhiteSpace(Title); }
        }

        [JsonProperty("items")]
        public ImagePickerItem[] Items { get; internal set; }

        [JsonIgnore]
        public bool HasItems {
            get { return Items != null && Items.Any(); }
        }

        [JsonProperty("count")]
        public int Count {
            get { return Items.Length; }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance with an empty model.
        /// </summary>
        internal ImagePickerList() {
            Items = new ImagePickerItem[0];
        }

        /// <summary>
        ///     Initializes a new instance based on the specified <see cref="JObject" />.
        /// </summary>
        /// <param name="obj">An instance of <see cref="JObject" /> representing the link picker list.</param>
        protected ImagePickerList(JObject obj) {
            JObject = obj;
            Title = obj.GetString("title");
            Items = (obj.GetArray("items", ImagePickerItem.Parse) ?? new ImagePickerItem[0]).ToArray();
        }

        /// <summary>
        ///     Initializes a new instance based on the specified <see cref="JArray" />.
        /// </summary>
        /// <param name="array">An instance of <see cref="JArray" /> representing the link picker list.</param>
        protected ImagePickerList(JArray array) {
            Items = (
                from obj in array
                let link = ImagePickerItem.Parse(obj as JObject)
                where link != null
                select link
                ).ToArray();
        }

        #endregion

        #region Static methods

        public static ImagePickerList Parse(JObject obj) {
            return obj == null ? null : new ImagePickerList(obj);
        }

        public static ImagePickerList Parse(JArray array) {
            return array == null ? null : new ImagePickerList(array);
        }

        public static ImagePickerList Deserialize(string json) {
            if (json == null) return new ImagePickerList();
            if (json.StartsWith("{") && json.EndsWith("}")) return Parse(JsonConvert.DeserializeObject<JObject>(json));
            if (json.StartsWith("[") && json.EndsWith("]")) return Parse(JsonConvert.DeserializeObject<JArray>(json));
            return new ImagePickerList();
        }

        #endregion
    }
}