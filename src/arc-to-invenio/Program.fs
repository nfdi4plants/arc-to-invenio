open System.IO
open Argu

open ARCtrl
open ARCtrl.FileSystem
open ARCtrl.ISA
open ARCtrl.NET
open Argu
open ARCtrl.NET.Contract

let getAllFilePaths (directoryPath : string) =
    let directoryPath = System.IO.Path.GetFullPath(directoryPath)
    let rec allFiles dirs =
        if Seq.isEmpty dirs then Seq.empty else
            seq { yield! dirs |> Seq.collect Directory.EnumerateFiles
                  yield! dirs |> Seq.collect Directory.EnumerateDirectories |> allFiles }

    allFiles [directoryPath] 
    |> Seq.toArray
    |> Array.map System.IO.Path.GetFullPath
    |> Array.map (fun p -> p.Replace(directoryPath, "").Replace("\\","/"))

let loadARCCustom (arcPath : string) =
                
    //// EINFACH DIESE ZEIELE AUSTAUSCHEN
           
    let paths = getAllFilePaths arcPath
            
    let arc = ARC.fromFilePaths paths

    let contracts = arc.GetReadContracts()

    let fulFilledContracts = 
        contracts 
        |> Array.map (fulfillReadContract arcPath)

    arc.SetISAFromContracts(fulFilledContracts,true)
    arc

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

    (loadARCCustom arcPath)
        .ISA
        .Value
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