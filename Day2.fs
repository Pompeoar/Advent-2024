module Day2

open System.IO

let splitIntoSeparateNumbers (line: string) = 
    line.Split(" ")
    |> Array.map int

let isWithinRange min max (a, b) = 
    abs (a - b) >= min && abs (a - b) <= max

let countOutOfRange min max numbers = 
    numbers
    |> Array.pairwise
    |> Array.filter (fun (a, b) -> not (isWithinRange min max (a, b)))
    |> Array.length

let isWithinTolerance numbers = 
    let outOfRangeCount = countOutOfRange 0 3 numbers
    outOfRangeCount <= 0

let getBadLevelCount compare numbers = 
    numbers
    |> Array.pairwise
        |> Array.filter (fun pair -> not (compare pair)) 
        |> Array.length

let isOrdered compare maxTolerance numbers = 
    let badCount = getBadLevelCount compare numbers
    badCount <= maxTolerance

let isAscending = isOrdered (fun (a, b) -> a < b) 
let isDescending = isOrdered (fun (a, b) -> a > b) 

let isSafeLevel maxTolerance numbers =
    let withinTolerance = isWithinTolerance numbers
    (isAscending maxTolerance numbers && withinTolerance) || (isDescending maxTolerance numbers && withinTolerance)

let isStrictSafeLevel = isSafeLevel 0
let isTolerableSafeLevel = isSafeLevel 1

let sanityCheck () =
    let testCases = 
        [
            ([| 7; 6; 4; 2; 1 |], "Safe without removing any level") // Safe
            ([| 1; 2; 7; 8; 9 |], "Unsafe regardless of which level is removed") // Unsafe
            ([| 9; 7; 6; 2; 1 |], "Unsafe regardless of which level is removed") // Unsafe
            ([| 1; 3; 2; 4; 5 |], "Safe by removing the second level, 3") // Safe
            ([| 8; 6; 4; 4; 1 |], "Safe by removing the third level, 4") // Safe
            ([| 1; 3; 6; 7; 9 |], "Safe without removing any level") // Safe
        ]

    testCases
    |> List.iter (fun (numbers, description) ->
        let result = 
            if isTolerableSafeLevel numbers then "Safe" else "Unsafe"
        printfn "Input: %A | Expected: %s | Result: %s" numbers description result
    )

let runDay2 = 
    let data = File.ReadAllLines(path = "./input/day2.txt")
    let levels = data |> Array.map splitIntoSeparateNumbers
    let strictlySafeLevels = levels |> Array.filter isStrictSafeLevel |> Array.length
    let tolerableSafeLevels = levels |> Array.filter isTolerableSafeLevel |> Array.length
    printfn "Number of safe levels: %d" strictlySafeLevels
    printfn "Number of tolerable safe levels: %d"  tolerableSafeLevels
    sanityCheck()

