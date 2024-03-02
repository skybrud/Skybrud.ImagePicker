# Configuration

Data types using the **Skybrud ImagePicker Image** property editor will have the following configuration options:

## Pick multiple images

The property editor can be configured to either work as a single picker, where it's not possible to select more than a single image, or a multi picker, where the user can select multiple images.

![image](https://user-images.githubusercontent.com/3634580/139288880-417784bb-88c6-4ed1-aba6-0e16f0161942.png)
_A visualiazation of the editor when configured as a single picker._

![image](https://user-images.githubusercontent.com/3634580/139289146-a747c893-3269-46d3-8d81-f8a9fa8a97e4.png)
_A visualiazation of the editor when configured as a multi picker._

## Start node

By default, the picker will show a tree starting at the root of the media archive - or the user's start node(s) if they don't have access to the entire media archive. With this option, you can set custom start node that will be used instead.

Notice that a custom start node may still be affected by the user's permissions. See the [*Ignore user start nodes*](#ignore-user-start-nodes) option for more information on how to handle this.

## Ignore user start nodes

As user may not have access to the full media archive, the start node they're seing in the picker may be different from the select [*start node*](#start-node) depending on the user's permissions.

Enable this setting will tell the picker to ignore the user's permissions, enabling the to pick media they would normally not have access to.

## Value type

If nothing else is specified, selected images will be returned as instances of <code>Skybrud.ImagePicker.Models.ImagePickerImage</code>. However if another model is more desired, a custom type can be set via the *Value type* option in the data type configuration.

The option lets you pick any .NET type that has a public constructor, and the first parameter is of the type `IPublishedElement`. If a constructor has any additional parameters, the package will try resolving their values via dependency injection.

If the second parameter is of type <code>Skybrud.ImagePicker.PropertyEditors.ImagePickerConfiguration</code>, the configuration of the data type will also be supplied to the model.

![image](https://user-images.githubusercontent.com/3634580/139322771-308eba6e-e946-4123-ae16-a5a726186f67.png)