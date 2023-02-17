# invenio-converter
Automatic building of a Docker container for converting ARCs to Invenio Metadata

## local use

docker build . -t inv
docker run -v C:\Users\HLWei\OneDrive\NFDI\ISAConverterDSL\Invenio:/arc -it 13810bfaf675 dotnet fsi /invenioConverter.fsx -p /arc/InvenioArc