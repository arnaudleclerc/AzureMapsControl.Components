![Nuget](https://img.shields.io/nuget/v/AzureMapsControl.Components) ![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/AzureMapsControl.Components) [![Build](https://github.com/arnaudleclerc/AzureMapsControl.Components/actions/workflows/build.yml/badge.svg)](https://github.com/arnaudleclerc/AzureMapsControl.Components/actions/workflows/build.yml) [![Unit Test](https://github.com/arnaudleclerc/AzureMapsControl.Components/actions/workflows/unit-tests.yml/badge.svg)](https://github.com/arnaudleclerc/AzureMapsControl.Components/actions/workflows/unit-tests.yml) [![release](https://github.com/arnaudleclerc/AzureMapsControl.Components/actions/workflows/release.yml/badge.svg)](https://github.com/arnaudleclerc/AzureMapsControl.Components/actions/workflows/release.yml) [![codecov](https://codecov.io/gh/arnaudleclerc/AzureMapsControl.Components/branch/develop/graph/badge.svg?token=KXPTJAXUFC)](https://codecov.io/gh/arnaudleclerc/AzureMapsControl.Components) [![Slack](https://img.shields.io/badge/chat-slack-blue)](https://join.slack.com/t/azuremapscontrolcomp/shared_invite/zt-oyoclzro-ZoFakjPLD8Y~nlxO49ybjg)

This library allows you to use `Azure Maps` inside your razor application.

![Custom Azure Map](./docs/assets/map.png) 

## Install the Nuget Package

This library is available on [Nuget](https://www.nuget.org/packages/AzureMapsControl.Components/) as `AzureMapsControl.Components`.

## Setup

### Add the css and scripts

You will need to add the atlas script and css files as well as the script generated by the library on your application.


```
<link href="https://atlas.microsoft.com/sdk/javascript/mapcontrol/3/atlas.min.css" rel="stylesheet" />
```

```
<script src="https://atlas.microsoft.com/sdk/javascript/mapcontrol/3/atlas.min.js"></script>
<script src="_content/AzureMapsControl.Components/azure-maps-control.js"></script>
```

Or use the minimized version : 

```
<script src="https://atlas.microsoft.com/sdk/javascript/mapcontrol/3/atlas.min.js"></script>
<script src="_content/AzureMapsControl.Components/azure-maps-control.min.js"></script>
```

### Register the Components

You will need to pass the authentication information of your `AzureMaps` instance to the library. `SubscriptionKey`, `Aad` and `Anonymous` authentication are supported. You will need to call the `AddAzureMapsControl` method on your services.

#### Using a Subscription Key

You can authenticate using a `subscription key` :

```
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();
        
        services.AddAzureMapsControl(configuration => configuration.SubscriptionKey = "Your Subscription Key");
    }
```

#### Using Azure Active Directory

Or using `Azure Active Directory`:

```
public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor(options => options.DetailedErrors = true);

            services.AddAzureMapsControl(configuration => {
                configuration.AadAppId = "Your Aad App Id";
                configuration.AadTenant = "Your Aad Tenant";
                configuration.ClientId = "Your Client Id";
            });
        }
```

The `Anonymous` authentication requires only a `ClientId`:

```
public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor(options => options.DetailedErrors = true);

            services.AddAzureMapsControl(configuration => configuration.ClientId = Configuration["AzureMaps:ClientId"])
        }
```

It also needs to fetch the token to send to the requests of the atlas library. For that, you have to override the `azureMapsControl.Extensions.getTokenCallback` method on your application after referencing `azure-maps-control.min.js` and resolve the token in it. For example : 

```
@page "/"
@namespace AzureMapsControl.Sample.Pages
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@{
    Layout = null;
}

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>AzureMapsControl.Sample</title>
    <base href="~/" />
    <link rel="stylesheet" href="https://atlas.microsoft.com/sdk/javascript/mapcontrol/3/atlas.min.css" type="text/css" />
    <link rel="stylesheet" href="https://atlas.microsoft.com/sdk/javascript/drawing/0.1/atlas-drawing.min.css" type="text/css" />
    <style>
        body {
            margin: 0;
        }

        #map {
            position: absolute;
            width: 100%;
            min-width: 290px;
            height: 100%;
        }
    </style>
</head>
<body>
    <app>
        <component type="typeof(App)" render-mode="ServerPrerendered" />
    </app>
    <script src="https://atlas.microsoft.com/sdk/javascript/mapcontrol/3/atlas.min.js"></script>
    <script src="https://atlas.microsoft.com/sdk/javascript/drawing/0.1/atlas-drawing.min.js"></script>
    <script src="_content/AzureMapsControl.Components/azure-maps-control.min.js"></script>
    <script src="_framework/blazor.server.js"></script>
    <script type="text/javascript">
        azureMapsControl.Extensions.getTokenCallback = (resolve, reject, map) => {
            const url = "url_of_my_token_endpoint";
            fetch(url).then(function (response) {
                return response.text();
            }).then(function (token) {
                resolve(token);
            });        
        };
    </script>
</body>
</html>

* End using Azure Active Directory part 
```

## Running the Samples

1. Read the second half of [CONTRIBUTING](CONTRIBUTING) for details on getting the whole thing to build.
2. In appsettings.Development.json, enter your Azure Maps Subscription Key in AzureMaps:SubscriptionKey. The default is to use this, not the Entra keys.
3. It generally works well to create . .sln file in the root that has both the erc and samples as projects.
3. Set AzureMapsControl.Sample to have a Project Reference to AzureMapsControl.Component.

## How to use

- [Map](docs/map)
- [Controls](docs/controls)
- [Drawing Toolbar](docs/drawingtoolbar)
- [Html Markers](docs/htmlmarkers)
- [Layers](docs/layers)
- [Sources](docs/sources)
- [Popups](docs/popups)
- [Traffic](docs/traffic)
- [Expressions](docs/expressions)
- [Animations](docs/animations)
- [Indoor Module](docs/indoor)

## Community links

- [Creating Azure Maps Applications In Blazor](https://blazorhelpwebsite.com/ViewBlogPost/59)
- [Blazor Store Finder](https://github.com/ADefWebserver/BlazorStoreFinder)

## Want to contribute ?

Contributions are welcome! One of the best way to start is to take a look at the list of [issues where help is wanted](https://github.com/arnaudleclerc/AzureMapsControl.Components/issues?q=is%3Aissue+is%3Aopen+label%3A%22help+wanted%22).

If you need a new feature which is not listed on the [issues](https://github.com/arnaudleclerc/AzureMapsControl.Components/issues), feel free to open a new one. Take also a look at the [Contributing guidelines](https://github.com/arnaudleclerc/AzureMapsControl.Components/blob/develop/CONTRIBUTING.md).
