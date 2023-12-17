FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
# COPY ./ProductService.sln .
# COPY ./ProductService.API/ProductService.API.csproj ./ProductService.API

# COPY ./ProductService.BL/ProductService.BL.csproj ./ProductService.BL
# COPY ./ProductService.DL/ProductService.DL.csproj ./ProductService.API
# COPY ./ProductService.Common/ProductService.Common.csproj ./ProductService.Common


# COPY ./Nuget.Config .
# COPY ./Lib/Packages/ECommerce.BL.1.0.17.nupkg ./Lib/Packages/ECommerce.BL.1.0.17.nupkg
# COPY ./Lib/Packages/ECommerce.1.0.17.nupkg ./Lib/Packages/ECommerce.1.0.17.nupkg
# COPY ./Lib/Packages/ECommerce.DL.1.0.17.nupkg ./Lib/Packages/ECommerce.DL.1.0.17.nupkg
# COPY ./Lib/Packages/ECommerce.Common.1.0.17.nupkg ./Lib/Packages/ECommerce.Common.1.0.17.nupkg
# COPY ./ProductService.sln .
# COPY ./ProductService.API/ProductService.API.csproj ./ProductService.API/ProductService.API.csproj

# COPY ./ProductService.BL/ProductService.BL.csproj ./ProductService.BL/ProductService.BL.csproj
# COPY ./ProductService.DL/ProductService.DL.csproj ./ProductService.DL/ProductService.DL.csproj
# COPY ./ProductService.Common/ProductService.Common.csproj ./ProductService.Common/ProductService.Common.csproj
COPY . .

RUN dotnet restore --configfile Nuget.Config
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "FileService.API.dll"]