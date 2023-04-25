namespace APITests
open Expecto



module API =

    [<Tests>]
    let ``tests for investigation read via IO`` =
        testList "investigation via IO" [
            testCase "json output formatted 1" (fun _ -> 
                Expect.equal 
                    (
                        TestObjects.IO.investigation
                        |> API.JSONCreation.CreateMetadataRecordFromInvestigation()
                        |> API.JSONCreation.SerializeMetadataRecord(Defaults.FormattedSerializerOptions)
                    )
                    ReferenceObjects.IO.investigation_1_formatted
                    "json output is incorrect"
                )
            testCase "json output unformatted 1" (fun _ ->
                Expect.equal 
                    (
                        TestObjects.IO.investigation
                        |> API.JSONCreation.CreateMetadataRecordFromInvestigation()
                        |> API.JSONCreation.SerializeMetadataRecord(Defaults.UnformattedSerializerOptions)
                    )
                    ReferenceObjects.IO.investigation_1_unformatted
                    "json output is incorrect"
                )
        ]

    [<Tests>]
    let ``tests for investigation created programmatically`` =
        testList "progammatically created investigation" [
            testCase "json output formatted 1" (fun _ -> 
                Expect.equal 
                    (
                        TestObjects.Programmatic.investigation
                        |> API.JSONCreation.CreateMetadataRecordFromInvestigation()
                        |> API.JSONCreation.SerializeMetadataRecord(Defaults.FormattedSerializerOptions)
                    )
                    ReferenceObjects.Programmatic.investigation_1_formatted
                    "json output is incorrect"
                )
            testCase "json output unformatted 1" (fun _ ->
                Expect.equal 
                    (
                        TestObjects.Programmatic.investigation
                        |> API.JSONCreation.CreateMetadataRecordFromInvestigation()
                        |> API.JSONCreation.SerializeMetadataRecord(Defaults.UnformattedSerializerOptions)
                    )
                    ReferenceObjects.Programmatic.investigation_1_unformatted
                    "json output is incorrect"
                )
        ]