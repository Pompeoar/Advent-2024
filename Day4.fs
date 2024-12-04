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

let getRowString (array: string[]) colIndex direction =
    if isInBounds array.Length (colIndex + 3 * direction) then
        [0..3]
        |> List.map (fun i -> array.[colIndex + i * direction])
        |> String.concat ""
    else
        ""

let getColumnString (arrays: string[][]) rowIndex colIndex direction =
    if isInBounds arrays.Length (rowIndex + 3 * direction) then
        [0..3]
        |> List.map (fun i -> arrays.[rowIndex + i * direction].[colIndex])
        |> String.concat ""
    else
        ""
let isDiagonalInBounds (arrays: string[][]) rowIndex colIndex verticalDirection horizontalDirection =
    isInBounds arrays.Length (rowIndex + 3 * verticalDirection) &&
    isInBounds arrays.[rowIndex].Length (colIndex + 3 * horizontalDirection)

let getDiagonalString (arrays: string[][]) rowIndex colIndex verticalDirection horizontalDirection =
    if isDiagonalInBounds arrays rowIndex colIndex verticalDirection horizontalDirection then
        [0..3]
        |> List.map (fun i -> arrays.[rowIndex + i * verticalDirection].[colIndex + i * horizontalDirection])
        |> String.concat ""
    else
        ""
let getAllDirectionStrings (arrays: string[][]) rowIndex colIndex =
    [
        getRowString arrays.[rowIndex] colIndex 1
        getRowString arrays.[rowIndex] colIndex -1
        getColumnString arrays rowIndex colIndex 1
        getColumnString arrays rowIndex colIndex -1
        getDiagonalString arrays rowIndex colIndex 1 1
        getDiagonalString arrays rowIndex colIndex -1 -1
        getDiagonalString arrays rowIndex colIndex 1 -1
        getDiagonalString arrays rowIndex colIndex -1 1
    ]


let countTargetMatchesInGrid (target: string) (arrays: string[][])  =
    let mutable count = 0

    for rowIndex in 0 .. arrays.Length - 1 do
        for colIndex in 0 .. arrays.[rowIndex].Length - 1 do
            if arrays.[rowIndex].[colIndex] = target.[0].ToString() then
                let directionStrings = getAllDirectionStrings arrays rowIndex colIndex
                count <- count + (directionStrings |> List.filter ((=) target) |> List.length)
    
    count

let splitLines (line: string) = line.ToCharArray() |> Array.map string

let run = 
    let target = "XMAS"
    let testCount = 
        testInput.Trim().Split('\n') 
        |> Array.map splitLines
        |> countTargetMatchesInGrid target
        
    let part1Count = 
        File.ReadAllLines("./input/day4.txt")
        |> Array.map splitLines
        |> countTargetMatchesInGrid target
        
    printfn "test case: %A" testCount
    printfn "test case: %A" part1Count