# arc-to-invenio

CLI tool for creating invenio-rdm metadata records from ARCs.

- [arc-to-invenio](#arc-to-invenio)
  - [Supported fields](#supported-fields)
  - [Development](#development)
    - [Building](#building)
    - [Testing](#testing)
    - [Building the docker container](#building-the-docker-container)
  - [Usage](#usage)
    - [Command line](#command-line)
    - [Docker](#docker)

## Supported fields

| Invenio metadata record | ARC metadata | Notes |
|---|---|---|
| `resource_type` | - | set to `{"id":"dataset"}` per default|
| `creators` | `Investigation.Contacts` | `"type":"personal"`, `name`, `given_name`, `family_name` created from individual `Person` entries in `Investigation.Contacts`|
| `title` | `Investigation.Title` | |
| `publication_date` | - | settable via `--publication-date` flag, defaults to current date|

## Development

Prerequisites:
- .NET 6.0 SDK
- For building/testing the container: docker

### Building

in the repo root:


`./build.cmd` or `./build.sh`

### Testing

in the repo root:


`./build.cmd runtests` or `./build.sh runtests`

### Building the docker container

in the repo root:

`docker build . -t arc-to-invenio`

## Usage

### Command line

```
arc-to-invenio [--help] --arc-directory <path> [--out-directory <path>] [--publication-date <publication date>]
                      [--format-output]

OPTIONS:

    --arc-directory, -p <path>
                          Specify a directory that contains the arc to convert.
    --out-directory, -o <path>
                          Optional. Specify a output directory for the invenio metadata record.
    --publication-date, -pd <publication date>
                          Optional. ISO 8601 formatted (yyyy-MM-dd) publication date to set on the record
    --format-output, -fmt Optional. Wether or not to format the output json document (e.g. use identations and
                          mutliline)
    --help                display this list of options.
```

### Docker

The CLI tool is built and added to path inside the container, so you can either:
1. Mount a directory containing the ARC to convert into the container and specify CLI args directly, e.g. 
   
    ```
    docker run --mount type=bind,source=path/to/your/arc,target=/arc arc-to-invenio arc-to-invenio <arguments here>
    ```

2. Running conversion inside the container, e.g. 
    ```
    docker run -it arc-to-invenio
    ```
    then - **inside the container** -  
    ```
    arc-to-invenio <arguments here>
3. or combine these approaches, e.g 
    ```
    docker run -it --mount type=bind,source=path/to/your/arc,target=/arc
    ```
    then - **inside the container** -  
    ```
    arc-to-invenio <arguments here>
    ```