module Day1

open System.IO

let splitIntoSeparateNumbers(line: string) = 
    let parts = line.Split("   ")
    let num1 = int parts.[0]
    let num2 = int parts.[1]
    (num1, num2)

let sortLists(list1: list<int>, list2: list<int>) =
    let sortedList1 = List.sort list1
    let sortedList2 = List.sort list2
    List.zip sortedList1 sortedList2

let findDifference a b =
    abs (a - b)

let findSimilitaryScore (target: int) (inputList: list<int>) =
    inputList
    |> List.filter (fun x -> x = target) 
    |> List.length 
    |> fun count -> count * target

let FindTotalDifference filePath =
    File.ReadAllLines(path = filePath)
    |> Array.map splitIntoSeparateNumbers
    |> Array.fold (fun (list1, list2) (num1, num2) -> 
        (num1 :: list1, num2 :: list2)
    ) ([], []) 
    |> fun lists -> sortLists lists
    |> List.map (fun (a, b) -> findDifference a b)
    |> List.sum

let FindSimilitary filePath =
    File.ReadAllLines(path = filePath)
    |> Array.map splitIntoSeparateNumbers
    |> Array.fold (fun (list1, list2) (num1, num2) -> 
        (num1 :: list1, num2 :: list2)
    ) ([], []) 
    |> fun (list1, list2) -> 
        list1
        |> List.map (fun target -> findSimilitaryScore target list2)
        |> List.sum

let runDay1 = 
    let filePath = "./input/day1.txt"
    let totalDifference = FindTotalDifference filePath
    printfn "Total Differences: %d" totalDifference

    let totalSimilarity = FindSimilitary filePath
    printfn "Total Similarity: %d" totalSimilarity