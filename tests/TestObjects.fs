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
                        LastName = "Doe"
                    )
                ]
            
        )

    let investigation_invalid =
        Investigation.create(
            Title = "Programmatically created test investigation without persons"
        )

module IO = 
    open arcIO.NET

    let investigation_1 = Investigation.fromArcFolder "fixtures/test-arc"
    let investigation_invalid = Investigation.fromArcFolder "fixtures/invalid-arc"