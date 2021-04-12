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
