## Expressions

An expression can be specified as the value of the filter property on certain layers or to define some of their options. They are represented as JSON array.

Some fields on the layer's options accept either an expression or a value, for example a string or a number. For example, the `FillColor` property of the `PolygonExtrusionLayerOptions` is of type `ExpressionOrString`. The constructor of the `ExpressionOrString` class allows to give either : 

- A `string` representing the expression.
- Subexpressions which will be combined to build the expression.
- A  `JsonDocument` representing the expression.

The `ExpressionOrNumber` and `ExpressionOrStringArray` classes offers the same possibilities with different types, respectively `double?` and `IEnumerable<string>`.

The `Expression` class only offers the possiblity to combine expressions or use a `JsonDocument`.

For more information on the Expressions, take a look at the [Web SDK Documentation](https://docs.microsoft.com/en-us/azure/azure-maps/data-driven-style-expressions-web-sdk).

### Examples

The following examples show you the multiple ways to set the `FillColor` of the `PolygonExtrusionLayerOptions`.

#### Using a value

The following example would fill the entire layer with a red color : 

```
var layer = new AzureMapsControl.Components.Layers.PolygonExtrusionLayer
{
    Options = new Components.Layers.PolygonExtrusionLayerOptions
    {
        FillColor = new Components.Atlas.ExpressionOrString("red")
    }
};
```

### Using expressions

The following example would fill the layer with a color based on the `DENSITY` property of the layer source :

```
var layer = new AzureMapsControl.Components.Layers.PolygonExtrusionLayer
        {
            Options = new Components.Layers.PolygonExtrusionLayerOptions
            {
                FillColor = new Components.Atlas.ExpressionOrString(new AzureMapsControl.Components.Atlas.Expression[] {
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("step"),
                    new AzureMapsControl.Components.Atlas.Expression(new AzureMapsControl.Components.Atlas.Expression[]{
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("get"),
                        new AzureMapsControl.Components.Atlas.ExpressionOrString("DENSITY")
                    }),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#00ff80"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(10),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#09e076"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(20),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#0bbf67"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(50),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#f7e305"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(100),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#f7c707"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(200),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#f78205"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(500),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#f75e05"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(1000),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#f72505"),
                    new AzureMapsControl.Components.Atlas.ExpressionOrNumber(10000),
                    new AzureMapsControl.Components.Atlas.ExpressionOrString("#6b0a05")
                }),
            }
        };
```

### Using a JSON Document

The following example has the same result as the one using the expressions, but uses the JSON representation of the expression :

```
var fillColorExpressionJsonString = "[\"step\", [\"get\", \"DENSITY\"], \"#00ff80\", 10, \"#09e076\", 20, \"#0bbf67\", 50, \"#f7e305\", 100, \"#f7c707\", 200, \"#f78205\", 500, \"#f75e05\", 1000, \"#f72505\", 10000, \"#6b0a05\"]";

var layer = new AzureMapsControl.Components.Layers.PolygonExtrusionLayer
{
    Options = new Components.Layers.PolygonExtrusionLayerOptions
    {
        FillColor = new Components.Atlas.ExpressionOrString("red")
    }
};
```