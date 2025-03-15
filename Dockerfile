# Use the .NET 9 SDK image for the build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /

# Copy the project file and restore dependencies
COPY BjjTrainer_API/*.csproj BjjTrainer_API/
RUN dotnet restore BjjTrainer_API/BjjTrainer_API.csproj

# Copy the rest of the solution
COPY . .

# Publish the API to the /app/publish folder
RUN dotnet publish BjjTrainer_API/BjjTrainer_API.csproj -c Release -o /app/publish

# Use the .NET 9 ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

# Set the app to listen on port 80 (all interfaces)
ENV ASPNETCORE_URLS=http://*:80

# Start the API
ENTRYPOINT ["dotnet", "BjjTrainer_API.dll"]
