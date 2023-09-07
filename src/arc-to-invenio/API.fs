module API

open ARCtrl
open ARCtrl.ISA
open ARCtrl.NET
open System.Text.Json
open JsonDSL

type JSONCreation() =

    static member tryGetOrcid (p: ISA.Person) =
        match p.ORCID with
        | Option.Some orcid -> 
            Option.Some(
                object {
                    property "scheme" "orcid"
                    property "identifier" orcid
                }
            )
        | _ -> None

    static member CreateMetadataRecordFromInvestigation (?PublicationDate:System.DateTime) =
        fun (i:ArcInvestigation) -> 
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
                    for p in i.Contacts do
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
                property "description" (+. i.Description)
                property "publication_date" publication_date_ISO
            }

    static member SerializeMetadataRecord (
        ?SerializerOptions: JsonSerializerOptions
    ) =
        fun (record: Nodes.JsonObject) ->
            let options = defaultArg SerializerOptions Defaults.FormattedSerializerOptions
            record.ToJsonString(options)

module JSONValidation = ()  // to-do once we have a common schema, implement as optional flag to validate all generated json via schema