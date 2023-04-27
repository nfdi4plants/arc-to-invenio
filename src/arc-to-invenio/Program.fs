open System.IO
open arcIO.NET
open Argu

try
    let args = CLIArgs.cliArgParser.ParseCommandLine()

    let arcPath = args.GetResult(CLIArgs.ARC_Directory)

    let outPath = 
        args.TryGetResult(CLIArgs.Out_Directory)
        |>Option.defaultValue arcPath

    let outFile = Path.Combine(outPath,"metadata.json")

    let publicationDate = 
        args.TryGetResult(CLIArgs.Publication_Date)
        |> Option.map (fun d -> System.DateTime.ParseExact(d, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture))

    let serializerOptions = 
        if args.TryGetResult(CLIArgs.Format_Output).IsSome then
            Defaults.FormattedSerializerOptions
        else
            Defaults.UnformattedSerializerOptions

    Investigation.read arcPath
    |> API.JSONCreation.CreateMetadataRecordFromInvestigation(?PublicationDate = publicationDate)
    |> API.JSONCreation.SerializeMetadataRecord(SerializerOptions = serializerOptions)
    |> fun metadataJson -> File.WriteAllText(outFile, metadataJson)

with
    | :? ArguParseException as ex ->
        match ex.ErrorCode with
        | ErrorCode.HelpText  -> printfn "%s" (CLIArgs.cliArgParser.PrintUsage())
        | _ -> printfn "%s" ex.Message

    | ex ->
        printfn "Internal Error:"
        printfn "%s" ex.Message