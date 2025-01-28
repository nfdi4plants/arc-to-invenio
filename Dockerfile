FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base

## This should be the build steps used, but we need to hotfix existing enmtry point temporarily.
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY "src/arc-to-invenio" .
#RUN dotnet restore "./arc-to-invenio.fsproj"
#RUN dotnet build "./arc-to-invenio.fsproj" -c $BUILD_CONFIGURATION -o /build
#
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./arc-to-invenio.fsproj" -c $BUILD_CONFIGURATION -o /publish
#
#FROM base AS final
#COPY --from=publish /publish .

ENV ARC_PATH=/arc

COPY ./ /opt/arc-to-invenio
WORKDIR /opt/arc-to-invenio
RUN chmod +x build.sh
RUN ./build.sh runtests

ENV PATH="${PATH}:/opt/arc-to-invenio/src/arc-to-invenio/bin/Release/net8.0"

WORKDIR /arc