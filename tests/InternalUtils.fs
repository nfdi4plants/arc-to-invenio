module internal InternalUtils

open ARCtrl
open ARCtrl.NET.Contract
open ARCtrl.NET
open System.IO

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
