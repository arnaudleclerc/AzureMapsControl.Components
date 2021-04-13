# Contributing to AzureMapsControl.Components

Thank you for taking time to contribute to this library!

The following is a set of guidelines to contribute to the `AzureMapsControl.Components` library. Those are mostly guidelines, not rules. 

## Code of Conduct

This project and everyone participating on it is governed by the [Contributor Covenant Code of Conduct](./CODE_OF_CONDUCT.md).

## How can I contribute?

### I just have a question

If you do not find your answer on the documentation or on an already existing issue, please either file a new one or ask your question on the `#questions` channel on [Slack](https://azuremapscontrolcomp.slack.com/).

### Reporting bugs

Before creating a bug, please check the list of [issues](https://github.com/arnaudleclerc/AzureMapsControl.Components/issues) as you might find out that you do not need to create one. The identified bugs will usually be marked with a `bug` tag. If you need to create a bug, please fill out the [bug template](.github/ISSUE_TEMPLATE/bug_report.md) and give as much information as possible. 

### Suggesting enhancements

If you have an idea how this package could be improved, please fill out the [feature request template](.github/ISSUE_TEMPLATE/feature_request.md) and open an issue.

### Suggesting new functionalities

Suggesting a new functionality is fine, as long as the functionality already exists on the `azure-maps-control` npm package or can be added as a script dependency inside your application.  If this is the case, please fill out the [feature request template](.github/ISSUE_TEMPLATE/feature_request.md) and open an issue.

### Your contribution

Please take a look at the list of issues or enhancements. Usually the issues which are available will be marked with an `help-wanted` tag. If you find something you want to work on, please drop a message on the issue. Once the issue is assigned to you, fork the repository and create a pull request against the develop branch once you are done.

- If you are working on a new feature or an enhancement, please host your code under a `features/<name of the feature>` branch.
- If you want to fix a bug, please host your code under a `hotfix/<name of the hotfix>` branch.

Please consider the following points when creating your pull request :

- The C# code has to respect the `.editorconfig`. Changes or improvements to this file are welcome, please discuss them on [Slack](https://azuremapscontrolcomp.slack.com/) before.
- Please add unit tests if possible and verify that all the unit tests are passing. For that, run `dotnet test` on the `AzureMapsControl.Components.Tests` project.
- The typescript code uses `eslint` as a linter. Changes to the rules are welcome, please discuss them on [Slack](https://azuremapscontrolcomp.slack.com/) before.
- Please verify that all the samples on the `AzureMapsControl.Sample` project are working. Please take time to add new samples if necessary.

## Local development

### Prerequisites

- You will need an Azure Maps Account. More information here: [https://docs.microsoft.com/en-us/azure/azure-maps/quick-demo-map-app#create-an-azure-maps-account](https://docs.microsoft.com/en-us/azure/azure-maps/quick-demo-map-app#create-an-azure-maps-account)
- You need to be able restore the packages using `npm`.
- You will need to be able to compile with .NET 5. Please ensure this version is installed on your machine. 

### Structure of the repository

The GIT repository is made of 4 parts :
- The `docs` folder contains the documentation of the library.
- The `samples` folder contains the examples used to showcase the functionalities.
- The `src` contains the source code of the library itself. The `typescript` part can be found under `src/AzureMapsControl.Components/typescript`.
- The `tests` folder contains the unit tests of the library.

The repository does not contain any solution file as it is not necessary to have one. If you prefer to work with a solution, you can create an `AzureMapsControl.Components.sln` solution file which will be automatically ignored by the `.gitignore` file.

### Set up your environment

- Restore the NPM packages. Under `src/AzureMapsControl.Components`, run `npm i`.
- Restore the Nuget packages. Under `src/AzureMapsControl.Components` and `tests/AzureMapsControl.Components.Tests`, run `dotnet restore`.
- For the samples to work, you will need to set up your configuration azure maps. Under `samples/AzureMapsControl.Sample`, create a `appsettings.Development.json` file. This will be automatically ignored by the `.gitignore` file, so you can safely store your authentication information in it. **Please be sure not to host any of your keys on your GitHub repository**. The sample expects the configuration to be found under an `AzureMaps` entry, so your file should look like following : 

```
{
  "AzureMaps": {
    "SubscriptionKey": "<your-subscription-key>",
    "ClientId": "<your-client-id>",
    "AadAppId": "<your-aad-app-id>",
    "AadTenant": "<your-aad-tenant>"
  }
}
```

### Build the library

The build of the library is made of two steps :

- Transpile the typescript code. Under `src/AzureMapsControl.Components`, run `npm run build`. This will create a `azure-maps-control.js` and `azure-maps-control.min.js` files.
- Build the library. Under `src/AzureMapsControl.Components`, run `dotnet build`.

### Run the samples

The samples can be run using `dotnet run` under `samples/AzureMapsControl.Sample`. This will start an host on `https://localhost:5001`. 

### Before creating a pull request

- Please verify that the linting of the typescript is valid using `npm run lint` under `src/AzureMapsControl.Components`. 
- Please verify that the library also compiles with a release configuration using `dotnet build -c Release` before creating your pull request.
- Please be sure that all the unit tests are passing using `dotnet test`.
- Please verify that the rules in `.editorconfig` are respected.
- Please update the documentation if necessary.