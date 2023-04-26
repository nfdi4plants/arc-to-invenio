module CLITests

open Expecto
open System.IO
open System.Diagnostics

let testDate = "2023-04-25"

type CLIContext() =
    static member create 
        (
            p: string,
            ?o : string,
            ?pd : string,
            ?fmt: bool
        ) = 
            fun f ->
                let tool = $"arc2invenio.exe"
                let args = 
                    [
                        $"-p {p} "
                        o |> Option.map (fun o -> $"-o {o} ") |> Option.defaultValue ""
                        pd |> Option.map (fun pd -> $"-pd {pd} ") |> Option.defaultValue $"-pd {testDate} "
                        fmt |> Option.defaultValue false |> (fun fmt -> if fmt then "-fmt " else "")
                    ]
                    |> String.concat ""

                let outFile = 
                    o 
                    |> Option.map (fun o -> Path.Combine(o, "metadata.json"))
                    |> Option.defaultValue (Path.Combine(p, "metadata.json"))
                    

                let procStartInfo = 
                    ProcessStartInfo(
                        UseShellExecute = false,
                        FileName = tool,
                        Arguments = args
                    )
                let proc = Process.Start(procStartInfo)
                let result = 
                    try 
                        proc.WaitForExit()
                        File.ReadAllText outFile
                    with e as _ -> 
                        $"{tool} {args} failed:{System.Environment.NewLine}{e.Message}"
                f result

[<Tests>]
let ``CLI Tests`` =
    testList "CLI tests" [
        testList "fixtures/test-arc" [
            yield! testFixture (CLIContext.create(p = "fixtures/test-arc")) [
                "arc2invenio -p fixtures/test-arc -pd 2023-04-25", (fun json -> fun () -> Expect.equal json ReferenceObjects.IO.investigation_1_unformatted "json created by cli copmmand was incorrect")
            ]
            yield! testFixture (CLIContext.create(p = "fixtures/test-arc", fmt=true)) [
                "arc2invenio -p fixtures/test-arc -pd 2023-04-25 -fmt", (fun json -> fun () -> Expect.equal json ReferenceObjects.IO.investigation_1_formatted "json created by cli copmmand was incorrect")
            ]
        ]
    ]