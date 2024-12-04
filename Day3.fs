module Day3
open System.IO
open System.Text.RegularExpressions
let testInput = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"
let inputData = File.ReadAllLines("./input/day3.txt")

let multiply (x, y) = x * y

let extractWithRegex (input: string) =
    let regex = Regex(@"mul\((\d+),(\d+)\)")
    regex.Matches(input)
    |> Seq.cast<Match>
    |> Seq.map (fun m -> int m.Groups.[1].Value, int m.Groups.[2].Value)
    |> Seq.toList


let run = 
    let result = 
        inputData
        |> String.concat ""
        |> extractWithRegex 
        |> List.map multiply
        |> List.sum

    printfn "part 1: %d" result 