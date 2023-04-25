#r "nuget: arcIO.NET"
#r "nuget: JsonDSL"
#r "nuget: Argu"

open arcIO.NET
open JsonDSL
open Argu
open System.IO


module Investigation =
    let title (i: ISADotNet.Investigation) = i.Title.Value

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

let i = Investigation.read @"C:\Users\schne\source\repos\nfdi4plants\invenio-converter\tests\fixtures\test-arc"

let now = System.DateTime.Now

let metadata = 
    object {
        property "resource_type" (object {
            property "id" "dataset"
        })
        property "creators" (array {
            let ps = 
                [
                for p in i.Contacts.Value do
                    object {
                        property "person_or_org" (object {
                            property "type" "personal"
                            property "name" $"{p.LastName.Value}, {p.FirstName.Value}"
                            property "given_name" p.FirstName.Value
                            property "family_name" p.LastName.Value
                        })
                    }
                ]
            yield! ps
        })
        property "title" (-. None)
        property "publication_date" $"""{System.DateTime.Now.ToString("yyyy-MM-dd")}"""
    }

open System.Text.Json;

let options = System.Text.Json.JsonSerializerOptions(WriteIndented = true)

File.WriteAllText(@"C:\Users\schne\source\repos\nfdi4plants\invenio-converter\tests\fixtures\test-arc\metadata.json",metadata.ToJsonString(options))