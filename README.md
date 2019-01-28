# localstack-docker

Dotnet Core console app demonstrating the use of Localstack with AWS SDK (S3).

## Info

Note that this guide assumes you are running an Ubuntu-based system.

## Setup

For reference, the following outlines the steps used to initialize this project with the dotnet CLI.

Initialize the project structure:

```sh
dotnet new sln --name LocalstackDocker
dotnet new console --name ConsoleApp --output ConsoleApp
dotnet sln add ConsoleApp/ConsoleApp.csproj
dotnet new nunit --name ConsoleApp.UnitTests --output ConsoleApp.UnitTests
dotnet sln add ConsoleApp.UnitTests/ConsoleApp.UnitTests.csproj
```

Add AWS S3 SDK reference:

```sh
dotnet add ConsoleApp/ConsoleApp.csproj package AWSSDK.S3
```

Ensure all projects build:

```sh
dotnet build LocalstackDocker.sln
```

## Firing up Localstack

Pull the latest Localstack image:

```sh
docker pull localstack/localstack:latest
```

Run the image in a container with only the S3 service, exposing ports `8080` for the web dashboard and `4572` for S3. Replace `-d` with `-it` to run the container interactively instead of in the background:

```sh
docker run -d -p 8080:8080 -p 4572:4572 -e SERVICES=s3 localstack/localstack:latest
```

Or to run all services (default if not overridden) and expose all default ports for all services:

```sh
docker run -d -p 8080:8080 -p 4567-4584:4567-4584 localstack/localstack:latest
```

Alternatively you can run either the included `start-localstack.sh` or `start-localstack.ps1` scripts to pull the latest Localstack Docker image and start the container.

Assuming you have the Microsoft apt repos configured, PowerShell Core (pwsh) can be installed via:

```sh
sudo apt update && sudo apt install powershell -y
```

Navigate to http://localhost:8080 to view the dashboard.