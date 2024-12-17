open System
//zadanie 1
//type UserData = {
//    Weight: float
//    Height: float
//}


//let calculateBMI weight height =
//    let heightInMeters = height / 100.0
//    weight / (heightInMeters ** 2.0)

//let getBMICategory bmi =
//    if bmi < 18.5 then "Niedowaga"
//    elif bmi < 24.9 then "Waga prawidłowa"
//    elif bmi < 29.9 then "Nadwaga"
//    else "Otyłość"

//[<EntryPoint>]
//let main argv =
//    printfn "Obliczanie wskaźnika BMI"

//    printf "Podaj swoją wagę (w kg): "
//    let weightInput = Console.ReadLine()
//    let weight = float weightInput 

//    printf "Podaj swój wzrost (w cm): "
//    let heightInput = Console.ReadLine()
//    let height = float heightInput 

//    let user = { Weight = weight; Height = height }

//    let bmi = calculateBMI user.Weight user.Height
//    let category = getBMICategory bmi

//    printfn "Twoje BMI wynosi: %.2f" bmi
//    printfn "Kategoria BMI: %s" category
    
//    0 
//Zadanie 2
//let exchange = 
//    Map.ofList [
//        ("USD", 1.10) 
//        ("GBP", 0.85) 
//        ("EUR", 1.00) 
//        ("PLN", 4.50) 
//    ]

//let convertCurrency amount sourceCurrency targetCurrency rates =
//    if Map.containsKey sourceCurrency rates && Map.containsKey targetCurrency rates then
//        let sourceRate = Map.find sourceCurrency rates
//        let targetRate = Map.find targetCurrency rates
//        let amountInEUR = amount / sourceRate
//        amountInEUR * targetRate
//    else
//        raise (ArgumentException "Nieobsługiwana waluta")

//[<EntryPoint>]
//let main argv =
//    printfn "Konwerter walut"
    
//    try
//        printf "Podaj kwotę do przeliczenia: "
//        let amountInput = Console.ReadLine()
//        let amount = float amountInput

//        printf "Podaj walutę źródłową (np. USD, EUR, GBP, PLN): "
//        let sourceCurrency = Console.ReadLine().ToUpper()

//        printf "Podaj walutę docelową (np. USD, EUR, GBP, PLN): "
//        let targetCurrency = Console.ReadLine().ToUpper()
        
//        // Obliczenie przeliczonej kwoty
//        let convertedAmount = convertCurrency amount sourceCurrency targetCurrency exchange
        

//        printfn "Przeliczona kwota: %.2f %s" convertedAmount targetCurrency
//    with
//    | :? FormatException ->
//        printfn "Błąd: Wprowadź poprawną liczbę jako kwotę."
//    | :? ArgumentException as ex ->
//        printfn "Błąd: %s" ex.Message
//    | ex ->
//        printfn "Nieoczekiwany błąd: %s" ex.Message
    
//    0 


//Zadanie 3
//let countWords (text: string) =
//    text.Split([|' '; '\t'; '\n'; '\r'|], StringSplitOptions.RemoveEmptyEntries)
//    |> Array.length

//let countCharacters (text: string) =
//    text |> Seq.filter (fun c -> not (Char.IsWhiteSpace c)) |> Seq.length

//let mostFrequentWord (text: string) =
//    text.Split([|' '; '\t'; '\n'; '\r'; '.'; ','; ';'; ':'; '!'; '?'|], StringSplitOptions.RemoveEmptyEntries)
//    |> Array.map (fun word -> word.ToLowerInvariant()) 
//    |> Array.groupBy id 
//    |> Array.map (fun (word, occurrences) -> word, Array.length occurrences) 
//    |> Array.maxBy snd 

//[<EntryPoint>]
//let main argv =
//    printfn "Analiza tekstu"
//    printf "Wprowadź tekst do analizy: "
//    let text = Console.ReadLine()

//    let wordCount = countWords text

//    let charCount = countCharacters text

//    let (mostFrequent, frequency) = mostFrequentWord text

//    printfn "Liczba słów: %d" wordCount
//    printfn "Liczba znaków (bez spacji): %d" charCount
//    printfn "Najczęściej występujące słowo: '%s' (wystąpiło %d razy)" mostFrequent frequency
    
//    0
//Zadanie 4


type BankAccount = {
    AccountNumber: string
    Balance: decimal
}

type Bank = Map<string, BankAccount>

let createAccount (bank: Bank) =
    printf "Podaj numer konta: "
    let accountNumber = Console.ReadLine()
    
    if bank.ContainsKey accountNumber then
        printfn "Konto o numerze '%s' już istnieje!" accountNumber
        bank
    else
        let newAccount = { AccountNumber = accountNumber; Balance = 0m }
        printfn "Konto o numerze '%s' zostało utworzone." accountNumber
        bank.Add(accountNumber, newAccount)

let deposit (bank: Bank) =
    printf "Podaj numer konta: "
    let accountNumber = Console.ReadLine()
    
    match bank.TryFind accountNumber with
    | Some account ->
        printf "Podaj kwotę do wpłaty: "
        let amount = decimal (Console.ReadLine())
        if amount <= 0m then
            printfn "Kwota musi być większa od 0."
            bank
        else
            let updatedAccount = { account with Balance = account.Balance + amount }
            printfn "Wpłacono %.2f. Nowe saldo: %.2f" amount updatedAccount.Balance
            bank.Add(accountNumber, updatedAccount)
    | None ->
        printfn "Konto o numerze '%s' nie istnieje." accountNumber
        bank

let withdraw (bank: Bank) =
    printf "Podaj numer konta: "
    let accountNumber = Console.ReadLine()
    
    match bank.TryFind accountNumber with
    | Some account ->
        printf "Podaj kwotę do wypłaty: "
        let amount = decimal (Console.ReadLine())
        if amount <= 0m then
            printfn "Kwota musi być większa od 0."
            bank
        elif amount > account.Balance then
            printfn "Niewystarczające środki na koncie. Saldo: %.2f" account.Balance
            bank
        else
            let updatedAccount = { account with Balance = account.Balance - amount }
            printfn "Wypłacono %.2f. Nowe saldo: %.2f" amount updatedAccount.Balance
            bank.Add(accountNumber, updatedAccount)
    | None ->
        printfn "Konto o numerze '%s' nie istnieje." accountNumber
        bank

let displayBalance (bank: Bank) =
    printf "Podaj numer konta: "
    let accountNumber = Console.ReadLine()
    
    match bank.TryFind accountNumber with
    | Some account ->
        printfn "Saldo konta '%s': %.2f" account.AccountNumber account.Balance
    | None ->
        printfn "Konto o numerze '%s' nie istnieje." accountNumber

let rec mainMenu (bank: Bank) =
    printfn "\n Bank Menu "
    printfn "1. Utwórz nowe konto"
    printfn "2. Wpłata"
    printfn "3. Wypłata"
    printfn "4. Wyświetl saldo"
    printfn "5. Wyjście"
    printf "Wybierz opcję: "
    
    match Console.ReadLine() with
    | "1" -> mainMenu (createAccount bank)
    | "2" -> mainMenu (deposit bank)
    | "3" -> mainMenu (withdraw bank)
    | "4" ->
        displayBalance bank
        mainMenu bank
    | "5" ->
        printfn "Do widzenia"
    | _ ->
        printfn "Nieprawidłowa opcja. Spróbuj ponownie."
        mainMenu bank

[<EntryPoint>]
let main argv =
    printfn "Witam!"
    let initialBank: Bank = Map.empty 
    mainMenu initialBank
    0 
