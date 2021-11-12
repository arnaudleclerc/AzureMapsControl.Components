## Sources

Most of the layers require `sources` to read their data from. A source needs to be added on the `Map` and its ID needs to be provided on the options of the layer. It does not matter if the source or the layer is added first, the layer being refreshed whenever the source changes.

Three types of sources are available :

- [Datasource](./datasource)
- [Gridded Datasource](./griddeddatasource)
- [Vector Tile source](./vectortilesource)

## Add a source

A source can be added to the map using the `AddSourceAsync` method.

## Removing the source

A source can be removed from the map using the `RemoveSourceAsync` method. A call to `ClearSourcesAsync` removes all the sources from the map.