FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY "src/arc-to-invenio" .
RUN dotnet restore "./arc-to-invenio.fsproj"
RUN dotnet build "./arc-to-invenio.fsproj" -c $BUILD_CONFIGURATION -o /build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./arc-to-invenio.fsproj" -c $BUILD_CONFIGURATION -o /publish

FROM base AS final
COPY --from=publish /publish .
