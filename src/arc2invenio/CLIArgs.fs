module CLIArgs

open Argu
open System.IO

type CliArguments =
    | [<Mandatory>][<AltCommandLine("-p")>] ARC_Directory of path:string
    | [<AltCommandLine("-o")>] Out_Directory of path:string

    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | ARC_Directory _ -> "specify a directory that contains the arc to convert."
            | Out_Directory _ -> "specify a output directory for the invenio metadata record."

let errorHandler = ProcessExiter(colorizer = function ErrorCode.HelpText -> None | _ -> Some System.ConsoleColor.Red)

let cliArgParser = ArgumentParser.Create<CliArguments>(programName = "arc2invenio", errorHandler = errorHandler)