open System
open Day1 

[<EntryPoint>]
let main argv =
    let filePath = "./input/day1.txt"
    let totalDifference = FindTotalDifference filePath
    printfn "Total Differences: %d" totalDifference

    let totalSimilarity = FindSimilitary filePath
    printfn "Total Similarity: %d" totalSimilarity

    0 
