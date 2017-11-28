# Visual Studio Project Template for Microsoft Teams Extension

This Visual Studio template scaffolds out a new Visual Studio solution to create Microsoft Teams Extensions based on the Microsoft Bot Framework.

The project will pull in both the Microsoft Bot Framework and Microsoft Teams dependencies - along with a sample Controller code for handling different ActivityTypes.Invoke activity types.

## Installing the template

To add the project template to your machine, you have 2 options:

1. Download the [zip file](https://github.com/daltskin/MicrosoftTeams-VisualStudio-Template/raw/master/Teams%20Application.zip) and copy to your Visual Studio Projects Template folder: %userprofile%\documents\Visual Studio 2017\Templates\ProjectTemplates\Visual C#

2. Clone the repo and run the BuildTemplate.ps1 powershell script

## Build a new Microsoft Teams Extensions

To use the project template:

* Open Visual Studio
* File New Project
* Select Visual C#
* Select the Teams Application project template
* Right-click the Solution in VS Solution Explorer
* Restore Nuget Packages
* Add your implementation - look for the // TODO: comments in the MessageController.cs 
