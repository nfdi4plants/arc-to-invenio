module TestObjects

open InternalUtils

module Programmatic =
    open ARCtrl
    open ARCtrl.NET
    open ARCtrl.ISA

    let investigation_1 =
        ArcInvestigation(
            Identifier.createMissingIdentifier(),
            title = "Programmatically created test investigation",
            description = "This is a test investigation created programmatically",
            contacts = 
                [|
                    Person.create(
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "yes@yes.yes",
                        ORCID =  "0000-0000-0000-0000",
                        Affiliation = "Institute 1"
                    )
                |]
        )

    let investigation_invalid =
        ArcInvestigation(
            Identifier.createMissingIdentifier(),
            title = "Programmatically created test investigation without persons"
        )

    let investigation_invalid_no_email =
        ArcInvestigation(
            Identifier.createMissingIdentifier(),
            title = "Programmatically created test investigation",
            contacts = 
                [|
                    Person.create(
                        FirstName = "John",
                        LastName = "Doe",
                        ORCID =  "0000-0000-0000-0000"
                    )
                |]
            
        )

    let investigation_invalid_no_affiliation =
        ArcInvestigation(
            Identifier.createMissingIdentifier(),
            title = "Programmatically created test investigation",
            contacts = 
                [|
                    Person.create(
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "yes@yes.yes",
                        ORCID =  "0000-0000-0000-0000"
                    )
                |]
        )

    let investigation_invalid_no_description =
        ArcInvestigation(
            Identifier.createMissingIdentifier(),
            title = "Programmatically created test investigation",
            contacts = 
                [|
                    Person.create(
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "yes@yes.yes",
                        ORCID =  "0000-0000-0000-0000",
                        Affiliation = "Institute 1"
                    )
                |]
        )

module IO = 
    open ARCtrl
    open ARCtrl.NET

    let investigation_1 = (loadARCCustom "fixtures/test-arc").ISA.Value
    let investigation_invalid = (loadARCCustom "fixtures/invalid-arc").ISA.Value