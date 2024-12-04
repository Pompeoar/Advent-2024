module Day3
open System.IO
open System.Text.RegularExpressions
let testInputPart1 = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"
let testInputPart2 = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"
let inputData = File.ReadAllLines("./input/day3.txt")

let multiply (x, y) = x * y

let extractNumbers (input: string) =
    let regex = Regex(@"mul\((\d+),(\d+)\)")
    regex.Matches(input)
    |> Seq.cast<Match>
    |> Seq.map (fun m -> int m.Groups.[1].Value, int m.Groups.[2].Value)
    |> Seq.toList

let extractDisabledProducts (input: string) = 
    let regex = Regex(@"don't\(\)(mul\((\d+),(\d+)\))+|mul\((\d+),(\d+)\)")
    regex.Matches(input)
    |> Seq.cast<Match>
    |> Seq.map (fun m ->
        if m.Value.StartsWith("don't()") then "" 
        else m.Value 
    )
    |> String.concat "" 


let extractConditionalNumbers (input: string) =
    let regex = Regex(@"mul\((\d+),(\d+)\)|do\(\)|don't\(\)")
    regex.Matches(input)
    |> Seq.cast<Match>
    |> Seq.map (fun m -> m.Value) 
    |> String.concat ""

let run = 
    let part1 = 
        inputData
        |> String.concat ""
        |> extractNumbers
        |> List.map multiply
        |> List.sum

    let part2 = 
        inputData
        |> String.concat ""
        |> extractConditionalNumbers 
        |> extractDisabledProducts
        |> extractNumbers
        |> List.map multiply
        |> List.sum

    printfn "part 1: %d" part1 
    printfn "part 2: %d" part2 