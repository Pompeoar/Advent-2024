module Day4
open System.IO

let testInput = """
MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX"""

let isInBounds (length: int) (index: int) =
    index >= 0 && index < length

let getRowString (array: string[]) colIndex direction wordLength =
    if isInBounds array.Length (colIndex + wordLength * direction) then
        [0..wordLength]
        |> List.map (fun i -> array.[colIndex + i * direction])
        |> String.concat ""
    else
        ""

let getColumnString (arrays: string[][]) rowIndex colIndex direction wordLength =
    if isInBounds arrays.Length (rowIndex + wordLength * direction) then
        [0..wordLength]
        |> List.map (fun i -> arrays.[rowIndex + i * direction].[colIndex])
        |> String.concat ""
    else
        ""
let isDiagonalInBounds (arrays: string[][]) rowIndex colIndex verticalDirection horizontalDirection wordLength =
    isInBounds arrays.Length (rowIndex + wordLength * verticalDirection) &&
    isInBounds arrays.Length rowIndex &&
    isInBounds arrays.[rowIndex].Length (colIndex + wordLength * horizontalDirection)

let getDiagonalString (arrays: string[][]) rowIndex colIndex verticalDirection horizontalDirection wordLength =
    if isDiagonalInBounds arrays rowIndex colIndex verticalDirection horizontalDirection wordLength then
        [0..wordLength]
        |> List.map (fun i -> arrays.[rowIndex + i * verticalDirection].[colIndex + i * horizontalDirection])
        |> String.concat ""
    else
        ""

let getAllDirectionStrings (arrays: string[][]) rowIndex colIndex wordLength =
    [
        getRowString arrays.[rowIndex] colIndex 1 wordLength
        getRowString arrays.[rowIndex] colIndex -1 wordLength
        getColumnString arrays rowIndex colIndex 1 wordLength
        getColumnString arrays rowIndex colIndex -1 wordLength
        getDiagonalString arrays rowIndex colIndex 1 1 wordLength
        getDiagonalString arrays rowIndex colIndex -1 -1 wordLength
        getDiagonalString arrays rowIndex colIndex 1 -1 wordLength
        getDiagonalString arrays rowIndex colIndex -1 1 wordLength
    ]

let getDiagonalDirections (arrays: string[][]) rowIndex colIndex wordLength =
    [
        getDiagonalString arrays rowIndex colIndex 1 1 wordLength
        getDiagonalString arrays (rowIndex + 2) colIndex -1 1 wordLength
    ]

let incrementIfAllMatch (target: string) (strings: list<string>) =
    let reversedTarget = target |> Seq.rev |> System.String.Concat
    if strings |> List.forall (fun str -> str = target || str = reversedTarget) then 1 else 0

let incrementForEachMatch (target: string) (strings: list<string>) =
    strings |> List.filter ((=) target) |> List.length

let countTargetMatchesInGrid (target: string) (wordFinderToUse: string[][] -> int -> int -> int -> list<string>) (matchValidator: string -> list<string> -> int) (arrays: string[][]) =
    let mutable count = 0

    for rowIndex in 0 .. arrays.Length - 1 do
        for colIndex in 0 .. arrays.[rowIndex].Length - 1 do
            let directionStrings = wordFinderToUse arrays rowIndex colIndex (target.Length - 1)
            count <- count + matchValidator target directionStrings
    
    count

let splitLines (line: string) = line.ToCharArray() |> Array.map string

let run = 
    let testCountPart1 = 
        testInput.Trim().Split('\n') 
        |> Array.map splitLines
        |> countTargetMatchesInGrid "XMAS" getAllDirectionStrings incrementForEachMatch

    printfn "test case: %A" testCountPart1

    let part1Count = 
        File.ReadAllLines("./input/day4.txt")
        |> Array.map splitLines
        |> countTargetMatchesInGrid "XMAS" getAllDirectionStrings incrementForEachMatch 
        
    printfn "test case: %A" part1Count
    
    let testCountPart2 = 
        testInput.Trim().Split('\n') 
        |> Array.map splitLines
        |> countTargetMatchesInGrid "MAS" getDiagonalDirections incrementIfAllMatch 
        
    printfn "test case: %A" testCountPart2

    let testCountPart2 = 
         File.ReadAllLines("./input/day4.txt")
        |> Array.map splitLines
        |> countTargetMatchesInGrid "MAS" getDiagonalDirections incrementIfAllMatch 
        
    printfn "test case: %A" testCountPart2