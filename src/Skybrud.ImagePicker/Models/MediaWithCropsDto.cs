using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;

namespace Skybrud.ImagePicker.Models {
    [DataContract]
    internal class MediaWithCropsDto {
        [DataMember(Name = "key")]
        public Guid Key { get; set; }

        [DataMember(Name = "mediaKey")]
        public Guid MediaKey { get; set; }

        [DataMember(Name = "crops")]
        public IEnumerable<ImageCropperValue.ImageCropperCrop> Crops { get; set; }

        [DataMember(Name = "focalPoint")]
        public ImageCropperValue.ImageCropperFocalPoint FocalPoint { get; set; }
    }
}
