using System;
using Newtonsoft.Json;

namespace Skybrud.ImagePicker.Json.Converters {

    public class ImagePickerEnumConverter : JsonConverter {

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            
            if (value == null) {
                writer.WriteNull();
                return;
            }

            writer.WriteValue((value + "").ToLower());

        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType) {
            return objectType == typeof(Enum);
        }

    }

}