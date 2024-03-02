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

<a href="./value-type/" class="btn btn-success">
    Read more about the value type option
    <i class="fa fa-arrow-circle-right" aria-hidden="true"></i>
</a>