# Use the .NET 6 SDK as the base image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory
WORKDIR /app

EXPOSE 4444

# Copy the project files to the container
COPY . .

# Restore NuGet packages
RUN dotnet restore

# Build the project
RUN dotnet build --no-restore

# Run the tests using SpecFlow
RUN dotnet test --no-restore 

# Set the entry point
ENTRYPOINT ["dotnet","seleniumspecflow.dll"]