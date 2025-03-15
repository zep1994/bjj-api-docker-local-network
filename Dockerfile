# Use the .NET 9 SDK image for build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the API project file and restore dependencies
COPY BjjTrainer_API/*.csproj BjjTrainer_API/
RUN dotnet restore BjjTrainer_API/BjjTrainer_API.csproj

# Copy the entire solution into the container
COPY . .

# Build and publish the API project
RUN dotnet run --project=BjjTrainer_API/

# Use the .NET 9 ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "run"]
