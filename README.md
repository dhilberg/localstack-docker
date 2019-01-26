# localstack-docker

Dotnet Core console app demonstrating the use of Localstack with the AWS SDK.

## Setup

### Initialize the Project Structure

```sh
dotnet new sln --name LocalstackDocker
dotnet new console --name ConsoleApp --output ConsoleApp
dotnet sln add ConsoleApp/ConsoleApp.csproj
dotnet new nunit --name ConsoleApp.UnitTests --output ConsoleApp.UnitTests
dotnet sln add ConsoleApp.UnitTests/ConsoleApp.UnitTests.csproj
```

### Add AWS SDK Reference

```sh
dotnet add ConsoleApp/ConsoleApp.csproj package AWSSDK.Core

# Ensure it builds
dotnet build LocalstackDocker.sln
```