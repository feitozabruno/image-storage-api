FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ImageStorage.Api/*.csproj ./ImageStorage.Api/
RUN dotnet restore ./ImageStorage.Api/ImageStorage.Api.csproj

COPY . .
RUN dotnet publish ImageStorage.Api/ImageStorage.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "ImageStorage.Api.dll"]