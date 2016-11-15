using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json.Extensions;

namespace Skybrud.ImagePicker {
    
    /// <summary>
    /// Class representing an image picker list.
    /// </summary>
    public class ImagePickerList {
        
        #region Properties

        /// <summary>
        /// Gets a reference to the <see cref="JObject"/> the list was parsed from.
        /// </summary>
        [JsonIgnore]
        public JObject JObject { get; private set; }

        /// <summary>
        /// Gets the title of the list.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; private set; }

        /// <summary>
        /// Gets whether a title has been specified for the list.
        /// </summary>
        [JsonIgnore]
        public bool HasTitle {
            get { return !String.IsNullOrWhiteSpace(Title); }
        }

        /// <summary>
        /// Gets an array of all valid items of the list.
        /// </summary>
        [JsonProperty("items")]
        public ImagePickerItem[] Items { get; private set; }

        /// <summary>
        /// Gets whether the list hs any valid items.
        /// </summary>
        [JsonIgnore]
        public bool HasItems {
            get { return Items != null && Items.Any(); }
        }

        /// <summary>
        /// Gets the total amount of items in the list.
        /// </summary>
        [JsonProperty("count")]
        public int Count {
            get { return Items.Length; }
        }

        /// <summary>
        /// Gets whether the image picker list is valid (alias of <see cref="HasItems"/>).
        /// </summary>
        [JsonIgnore]
        public bool IsValid {
            get { return HasItems; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance with an empty model.
        /// </summary>
        public ImagePickerList() {
            Title = "";
            Items = new ImagePickerItem[0];
        }

        /// <summary>
        /// Initializes a new instance based on the specified <see cref="JObject" />.
        /// </summary>
        /// <param name="obj">An instance of <see cref="JObject" /> representing the image picker list.</param>
        protected ImagePickerList(JObject obj) {
            JObject = obj;
            Title = obj.GetString("title") ?? "";
            Items = (obj.GetArray("items", ImagePickerItem.Parse) ?? new ImagePickerItem[0]).Where(x => x.IsValid).ToArray();
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Parses the specified <see cref="JObject"/> into an instance of <see cref="ImagePickerList"/>.
        /// </summary>
        /// <param name="obj">The instance of <see cref="JObject"/> to be parsed.</param>
        /// <returns>Returns an instance of <see cref="ImagePickerList"/>, or <code>null</code> if <code>obj</code> is
        /// <code>null</code>.</returns>
        public static ImagePickerList Parse(JObject obj) {
            return obj == null ? null : new ImagePickerList(obj);
        }

        /// <summary>
        /// Parses the specified JSON <code>str</code> into an instance of <see cref="ImagePickerList"/>. If
        /// <code>str</code> is not a valid JSON object, an empty instance of <see cref="ImagePickerList"/> will be returned instead.
        /// </summary>
        /// <param name="str">The JSON string to be parsed.</param>
        /// <returns>Returns an instance of <see cref="ImagePickerList"/>.</returns>
        public static ImagePickerList Deserialize(string str) {
            if (str == null) return new ImagePickerList();
            if (str.StartsWith("{") && str.EndsWith("}")) return Parse(JsonConvert.DeserializeObject<JObject>(str));
            return new ImagePickerList();
        }

        #endregion
    
    }

}