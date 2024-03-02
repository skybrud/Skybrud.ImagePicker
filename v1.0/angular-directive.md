---
order: 4
---

# Angular Directive

In relation to the backoffice, the main logic of the image picker has been isolated into an Angular directive that can be used in your custom Angular views.

Below is an example of the view for the property editor:

```HTML
<div>
    <skybrud-imagepicker value="model.value" config="model.config.config"></skybrud-imagepicker>
</div>
```

The model of the image picker list is specified through the `value` attribute - you can simply pass a variable with an empty JavaScript object, and the image picker directive will make sure to set the correct properties.

In a similar way, the configuration can be specified through the `config` attribute. The value specified through this attribute is a JavaScript object similar to the `config` object in the grid editor configuration as shown above.