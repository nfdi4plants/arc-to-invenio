module TestObjects


module Programmatic =
    open ISADotNet

    let investigation_1 =
        Investigation.create(
            Title = "Programmatically created test investigation",
            Contacts = 
                [
                    Person.create(
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "yes@yes.yes",
                        Comments = 
                            [
                                Comment.create(
                                    Name = "Investigation Person ORCID",
                                    Value = "0000-0000-0000-0000"
                                )
                            ],
                        Affiliation = "Institute 1"
                    )
                ]
        )

    let investigation_invalid =
        Investigation.create(
            Title = "Programmatically created test investigation without persons"
        )

    let investigation_invalid_no_email =
        Investigation.create(
            Title = "Programmatically created test investigation",
            Contacts = 
                [
                    Person.create(
                        FirstName = "John",
                        LastName = "Doe",
                        Comments = 
                            [
                                Comment.create(
                                    Name = "Investigation Person ORCID",
                                    Value = "0000-0000-0000-0000"
                                )
                            ]
                    )
                ]
            
        )

    let investigation_invalid_no_affiliation =
        Investigation.create(
            Title = "Programmatically created test investigation",
            Contacts = 
                [
                    Person.create(
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "yes@yes.yes",
                        Comments = 
                            [
                                Comment.create(
                                    Name = "Investigation Person ORCID",
                                    Value = "0000-0000-0000-0000"
                                )
                            ]
                    )
                ]
        )

module IO = 
    open arcIO.NET

    let investigation_1 = Investigation.fromArcFolder "fixtures/test-arc"
    let investigation_invalid = Investigation.fromArcFolder "fixtures/invalid-arc"