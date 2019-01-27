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

### Add AWS Service References

```sh
dotnet add ConsoleApp/ConsoleApp.csproj package AWSSDK.S3

# Ensure it builds
dotnet build LocalstackDocker.sln
```

### Firing up Localstack

```sh
# Pull the Localstack image
docker pull localstack/localstack

# Run the image in a container, exposing ports 8080 for the web dashboard and 4572 for S3. Replace -d with -it to run the container interactively instead of in the background.
docker run -d -p 8080:8080 -p 4572:4572 localstack/localstack

# Or to expose all the default ports for all the services
docker run -d -p 8080:8080 -p 4567-4584:4567-4584 localstack/localstack
```

Navigate to http://localhost:8080 to view the dashboard.