FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy everything to working directory
COPY . ./

# Build project
RUN dotnet publish YoutubeDownloader.Api -c Release -o YoutubeDownloader.Api/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/YoutubeDownloader.Api/out .
ENTRYPOINT ["dotnet", "YoutubeDownloader.Api.dll"]
