module API


let test() = 42

module JSONCreation =

    open arcIO.NET

    open JsonDSL

    let createMetadataRecordFromInvestigation (i:ISADotNet.Investigation) =
        //currently targets this undocumented python class:
        //
        //class Metadata(BaseModel):
        //    creators: List[Creator]
        //    publication_date: Optional[str] = None
        //    resource_type: Optional[ResourceType] = None
        //    title: Optional[str] = None

        object {
            property "resource_type" (object {
                property "id" "dataset"
            })
            property "creators" (array {
                let ps = 
                    [
                    for p in i.Contacts.Value do
                        object {
                            property "person_or_org" (object {
                                property "type" "personal"
                                property "name" $"{p.LastName.Value}, {p.FirstName.Value}"
                                property "given_name" p.FirstName.Value
                                property "family_name" p.LastName.Value
                            })
                        }
                    ]
                yield! ps
            })
            property "title" i.Title.Value
            property "publication_date" $"{System.DateTime.Now.Year}" //$"{now.Year}-{now.Month}-{now.Day}"
        }