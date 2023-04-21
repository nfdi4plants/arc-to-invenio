open System.IO
open arcIO.NET

let args = CLIArgs.cliArgParser.ParseCommandLine()

let arcPath = args.GetResult(CLIArgs.ARC_Directory)

let outPath = 
    args.TryGetResult(CLIArgs.Out_Directory)
    |>Option.defaultValue arcPath

let outFile = Path.Combine(outPath,"metadata.json")


let i = Investigation.read arcPath
let now = System.DateTime.Now

let metadata = API.JSONCreation.createMetadataRecordFromInvestigation i

let options = System.Text.Json.JsonSerializerOptions(WriteIndented = true)

File.WriteAllText(outFile,metadata.ToJsonString(options))