Compress-Archive -Path '.\Teams Application\*' -DestinationPath .\"Teams Application.zip" -Force
Copy-Item ".\Teams Application.zip" -Destination "$($env:USERPROFILE)\documents\Visual Studio 2017\Templates\ProjectTemplates\Visual C#"