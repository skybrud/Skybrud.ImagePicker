using Umbraco.Cms.Core.Composing;

namespace Skybrud.ImagePicker.Converters.ImageWithCrops {
    
    internal sealed class ImageWithCropsTypeConverterCollectionBuilder : LazyCollectionBuilderBase<ImageWithCropsTypeConverterCollectionBuilder, ImageWithCropsTypeConverterCollection, IImageWithCropsTypeConverter> {

        protected override ImageWithCropsTypeConverterCollectionBuilder This => this;

    }

}