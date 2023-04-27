FROM mcr.microsoft.com/dotnet/sdk:6.0

ENV ARC_PATH=/arc

COPY ./ /opt/arc-to-invenio
WORKDIR /opt/arc-to-invenio
RUN chmod +x build.sh
RUN ./build.sh runtests

ENV PATH="${PATH}:/opt/arc-to-invenio/src/arc-to-invenio/bin/Release/net6.0"

WORKDIR /arc