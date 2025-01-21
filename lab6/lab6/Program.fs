open System

let countWordsAndCharacters (inputText: string) =
    let wordArray = inputText.Split([|' '; '\t'; '\n'; '\r'|], StringSplitOptions.RemoveEmptyEntries)
    let totalWords = wordArray.Length
    let totalCharacters = inputText.ToCharArray() |> Array.filter (fun character -> not (Char.IsWhiteSpace(character))) |> Array.length
    totalWords, totalCharacters

let isPalindrome (inputText: string) =
    let cleanedText = 
        inputText.ToLower() 
        |> Seq.filter (fun character -> Char.IsLetterOrDigit(character)) 
        |> Seq.toArray
    cleanedText = Array.rev cleanedText

let removeDuplicates (stringList: string list) =
    stringList |> List.distinct

let reformatEntries (entryList: string list) =
    entryList 
    |> List.map (fun entry -> 
        let entryParts = entry.Split(';') |> Array.map (fun part -> part.Trim())
        if entryParts.Length = 3 then
            let firstName = entryParts.[0]
            let lastName = entryParts.[1]
            let age = entryParts.[2]
            sprintf "%s, %s (%s lat)" lastName firstName age
        else
            sprintf "Nieprawidłowy format: %s" entry
    )
let findLongestWord (inputText: string) =
    let wordArray = inputText.Split([|' '; '\t'; '\n'; '\r'|], StringSplitOptions.RemoveEmptyEntries)
    let longestWord = wordArray |> Array.maxBy (fun word -> word.Length)
    longestWord, longestWord.Length
let replaceWordInText (inputText: string) (targetWord: string) (replacementWord: string) =
    inputText.Replace(targetWord, replacementWord)
[<EntryPoint>]
let main args =
    printfn "Podaj tekst:"
    let userInput = Console.ReadLine()
    match userInput with
    | null -> 
        printfn "Nie podano tekstu."
    | _ ->
        // Zad 1
        let totalWords, totalCharacters = countWordsAndCharacters userInput
        printfn "Liczba słów: %d" totalWords
        printfn "Liczba znaków (bez spacji): %d" totalCharacters

        // Zad 2
        if isPalindrome userInput then
            printfn "Podany tekst jest palindromem."
        else
            printfn "Podany tekst nie jest palindromem."

        // Zad 3
        let wordList = userInput.Split([|' '; '\t'; '\n'; '\r'|], StringSplitOptions.RemoveEmptyEntries) |> Array.toList
        let uniqueWords = removeDuplicates wordList
        printfn "Lista unikalnych słów: %A" uniqueWords

    0

