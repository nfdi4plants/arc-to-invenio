module API

open arcIO.NET
open System.Text.Json
open JsonDSL

type JSONCreation() =

    static member tryGetOrcid (p: ISADotNet.Person) =
        match p.Comments with
        | Option.Some comments -> 
            let orcid = 
                comments |> List.tryFind (fun c -> 
                    match c.Name with
                    | Option.Some n -> n = "Investigation Person ORCID"
                    | _ -> false
                )
                |> Option.map (fun c -> 
                    match c.Value with
                    | Option.Some v ->  Option.Some v
                    | _ -> None
                )
            match orcid with
            | Option.Some (Option.Some o) -> 
                Option.Some(
                    object {
                        property "scheme" "orcid"
                        property "identifier" o
                    }
                )
            | _ -> None
        | _ -> None

    static member CreateMetadataRecordFromInvestigation (?PublicationDate:System.DateTime) =
        fun (i:ISADotNet.Investigation) -> 
            //currently targets this undocumented python class:
            //
            //class Metadata(BaseModel):
            //    creators: List[Creator]
            //    publication_date: Optional[str] = None
            //    resource_type: Optional[ResourceType] = None
            //    title: Optional[str] = None
            let publicationDate = defaultArg PublicationDate System.DateTime.Now
            let publication_date_ISO = publicationDate.ToString("yyyy-MM-dd")

            object {
                property "resource_type" (object {
                    property "id" "dataset"
                })
                property "creators" (array {
                    for p in i.Contacts |> Option.defaultValue [] do
                        yield object {
                            property "person_or_org" (object {
                                property "type" "personal"
                                property "name" $"{p.LastName.Value}, {p.FirstName.Value}"
                                property "given_name" p.FirstName.Value
                                property "family_name" p.LastName.Value
                                property "identifiers" (array {
                                    yield object {
                                        property "scheme" "email"
                                        property "identifier" (+. p.EMail)
                                    }
                                    if (JSONCreation.tryGetOrcid p).IsSome then 
                                       yield (JSONCreation.tryGetOrcid p).Value
                                })
                            })
                            property "affiliations" (array {
                                object {
                                    property "name" (+. p.Affiliation)
                                }
                            })
                        }
                })
                property "title" (-. i.Title)
                property "publication_date" publication_date_ISO
            }

    static member SerializeMetadataRecord (
        ?SerializerOptions: JsonSerializerOptions
    ) =
        fun (record: Nodes.JsonObject) ->
            let options = defaultArg SerializerOptions Defaults.FormattedSerializerOptions
            record.ToJsonString(options)

module JSONValidation = ()  // to-do once we have a common schema, implement as optional flag to validate all generated json via schema