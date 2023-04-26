module APITests
open Expecto

let testDate = System.DateTime.ParseExact("2023-04-25","yyyy-MM-dd",System.Globalization.CultureInfo.InvariantCulture)

[<Tests>]
let ``API Tests`` =
    testList "API tests" [
        testList "investigation via IO" [
            testList "investigation_1" [
                testCase "json output formatted" (fun _ -> 
                    Expect.equal 
                        (
                            TestObjects.IO.investigation_1
                            |> API.JSONCreation.CreateMetadataRecordFromInvestigation(testDate)
                            |> API.JSONCreation.SerializeMetadataRecord(Defaults.FormattedSerializerOptions)
                        )
                        ReferenceObjects.IO.investigation_1_formatted
                        "json output is incorrect"
                    )
                testCase "json output unformatted" (fun _ ->
                    Expect.equal 
                        (
                            TestObjects.IO.investigation_1
                            |> API.JSONCreation.CreateMetadataRecordFromInvestigation(testDate)
                            |> API.JSONCreation.SerializeMetadataRecord(Defaults.UnformattedSerializerOptions)
                        )
                        ReferenceObjects.IO.investigation_1_unformatted
                        "json output is incorrect"
                    )
            ]
            testList "invalid investigation" [
                testCase "investigation without persons should fail" (fun _ -> 
                    Expect.throws (fun () -> 
                        TestObjects.IO.investigation_invalid
                        |> API.JSONCreation.CreateMetadataRecordFromInvestigation(testDate)
                        |> ignore
                    )
                        "invalid metadata record was created but should have failed"
                )
            ]
        ]
        testList "progammatically created investigation" [
            testList "investigation_1" [
                testCase "json output formatted" (fun _ -> 
                    Expect.equal 
                        (
                            TestObjects.Programmatic.investigation_1
                            |> API.JSONCreation.CreateMetadataRecordFromInvestigation(testDate)
                            |> API.JSONCreation.SerializeMetadataRecord(Defaults.FormattedSerializerOptions)
                        )
                        ReferenceObjects.Programmatic.investigation_1_formatted
                        "json output is incorrect"
                    )
                testCase "json output unformatted" (fun _ ->
                    Expect.equal 
                        (
                            TestObjects.Programmatic.investigation_1
                            |> API.JSONCreation.CreateMetadataRecordFromInvestigation(testDate)
                            |> API.JSONCreation.SerializeMetadataRecord(Defaults.UnformattedSerializerOptions)
                        )
                        ReferenceObjects.Programmatic.investigation_1_unformatted
                        "json output is incorrect"
                    )
            ]
            testList "invalid investigation" [
                testCase "investigation without persons should fail" (fun _ -> 
                    Expect.throws (fun () -> 
                        TestObjects.Programmatic.investigation_invalid
                        |> API.JSONCreation.CreateMetadataRecordFromInvestigation(testDate)
                        |> ignore
                    )
                        "invalid metadata record was created but should have failed"
                )
            ]
        ]
    ]