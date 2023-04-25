module TestObjects


module Programmatic =
    open ISADotNet

    let investigation =
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

module IO = 
    open arcIO.NET

    let investigation = Investigation.fromArcFolder "fixtures/test-arc"