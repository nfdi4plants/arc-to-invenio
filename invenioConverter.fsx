#r "nuget: arcIO.NET"
#r "nuget: JsonDSL"
#r "nuget: Argu"

open arcIO.NET
open JsonDSL
open Argu
open System.IO

type CliArguments =
    | [<Mandatory>][<AltCommandLine("-p")>] ARC_Directory of path:string

    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | ARC_Directory _ -> "specify a working directory."

let parser = ArgumentParser.Create<CliArguments>()
let results = parser.Parse (Array.skip 1 fsi.CommandLineArgs)

let arcPath = results.GetResult(ARC_Directory)
let outPath = Path.Combine(arcPath,"metadata.json")

let i = Investigation.read arcPath

let now = System.DateTime.Now

let metadata = 
    object {
        property "resource_type" (object {
            property "id" "ARC"
        })
        property "creators" (array {
            for p in i.Contacts.Value do
                object {
                    property "person_or_org" (object {
                        property "type" "personal"
                        property "name" $"{p.LastName.Value}, {p.FirstName.Value}"
                        property "given_name" p.FirstName.Value
                        property "family_name" p.LastName.Value
                    })
                }
        })
        property "title" i.Title.Value
        property "publication_date" $"{now.Year}-{now.Month}-{now.Day}"
    }


let options = System.Text.Json.JsonSerializerOptions(WriteIndented = true)
File.WriteAllText(outPath,metadata.ToJsonString(options))