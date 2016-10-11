using Skybrud.ImagePicker.Grid.Converters;
using Skybrud.Umbraco.GridData;
using Umbraco.Core;

namespace Skybrud.ImagePicker {
    
    public class Startup : ApplicationEventHandler {
        
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext) {
            
            GridContext.Current.Converters.Add(new ImagePickerGridConverter());

        }

    }

}