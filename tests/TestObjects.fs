module TestObjects


module Programmatic =
    open ARCtrl

    

    let investigation_1 =
        ArcInvestigation(
            ARCtrl.Helper.Identifier.createMissingIdentifier(),
            title = "Programmatically created test investigation",
            description = "This is a test investigation created programmatically",
            contacts = 
                ResizeArray [|
                    Person.create(
                        firstName = "John",
                        lastName = "Doe",
                        email = "yes@yes.yes",
                        orcid =  "0000-0000-0000-0000",
                        affiliation = "Institute 1"
                    )
                |]
        )

    let investigation_invalid =
        ArcInvestigation(
            ARCtrl.Helper.Identifier.createMissingIdentifier(),
            title = "Programmatically created test investigation without persons"
        )

    let investigation_invalid_no_email =
        ArcInvestigation(
            ARCtrl.Helper.Identifier.createMissingIdentifier(),
            title = "Programmatically created test investigation",
            contacts = 
                ResizeArray [|
                    Person.create(
                        firstName = "John",
                        lastName = "Doe",
                        orcid =  "0000-0000-0000-0000"
                    )
                |]
            
        )

    let investigation_invalid_no_affiliation =
        ArcInvestigation(
            ARCtrl.Helper.Identifier.createMissingIdentifier(),
            title = "Programmatically created test investigation",
            contacts = 
                ResizeArray [|
                    Person.create(
                        firstName = "John",
                        lastName = "Doe",
                        email = "yes@yes.yes",
                        orcid =  "0000-0000-0000-0000"
                    )
                |]
        )

    let investigation_invalid_no_description =
        ArcInvestigation(
            ARCtrl.Helper.Identifier.createMissingIdentifier(),
            title = "Programmatically created test investigation",
            contacts = 
                ResizeArray [|
                    Person.create(
                        firstName = "John",
                        lastName = "Doe",
                        email = "yes@yes.yes",
                        orcid =  "0000-0000-0000-0000",
                        affiliation = "Institute 1"
                    )
                |]
        )

module IO = 
    open ARCtrl

    let investigation_1 = ARC.load("fixtures/test-arc").ISA.Value
    let investigation_invalid = ARC.load("fixtures/invalid-arc").ISA.Value