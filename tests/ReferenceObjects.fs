module ReferenceObjects

open System.IO

module Programmatic =
    let investigation_1_formatted = File.ReadAllText("fixtures/json/programmatic/investigation_1_formatted.json").ReplaceLineEndings()
    let investigation_1_unformatted = File.ReadAllText("fixtures/json/programmatic/investigation_1_unformatted.json").ReplaceLineEndings()

module IO = 
    let investigation_1_formatted = File.ReadAllText("fixtures/json/io/investigation_1_formatted.json").ReplaceLineEndings()
    let investigation_1_unformatted = File.ReadAllText("fixtures/json/io/investigation_1_unformatted.json").ReplaceLineEndings()