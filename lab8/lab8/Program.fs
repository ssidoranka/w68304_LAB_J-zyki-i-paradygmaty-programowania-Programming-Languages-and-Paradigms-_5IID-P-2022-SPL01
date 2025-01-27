
let sumList (lst: int list) =
    lst |> List.sum

let numbers = [1; 2; 3; 4; 5]
let sum = sumList numbers
printfn "Suma elementów: %d" sum


let minMax (lst: int list) =
    match lst with
    | [] -> None
    | _ ->
        let minElement = List.min lst
        let maxElement = List.max lst
        Some(minElement, maxElement)


match minMax numbers with
| Some(min, max) -> printfn "Min: %d, Max: %d" min max
| None -> printfn "Lista jest pusta."


let reverseList (lst: 'T list) =
    List.rev lst

let reversed = reverseList numbers
printfn "Odwrócona lista: %A" reversed



let containsElement (lst: 'T list) (element: 'T) =
    List.contains element lst

let exists = containsElement numbers 3
printfn "Czy element 3 znajduje się na liście? %b" exists


let findElementIndex (lst: 'T list) (element: 'T) =
    match List.tryFindIndex (fun x -> x = element) lst with
    | Some(index) -> Some(index)
    | None -> None


match findElementIndex numbers 3 with
| Some(index) -> printfn "Indeks elementu: %d" index
| None -> printfn "Element nie znajduje się na liście."


let joinLists (lst1: 'T list) (lst2: 'T list) =
    lst1 @ lst2

let list1 = [1; 2; 3]
let list2 = [4; 5; 6]
let joinedList = joinLists list1 list2
printfn "Połączona lista: %A" joinedList


let countElementOccurrences (lst: 'T list) (element: 'T) =
    List.filter (fun x -> x = element) lst |> List.length


let count = countElementOccurrences numbers 3
printfn "Element 3 występuje %d razy" count


let compareLists (lst1: int list) (lst2: int list) =
    if List.length lst1 <> List.length lst2 then
        raise (System.Exception("Listy muszą mieć tę samą długość"))
    else
        List.map2 (fun a b -> a > b) lst1 lst2

let list11 = [1; 3; 5; 7]
let list22 = [2; 2; 6; 4]
try
    let result = compareLists list11 list22
    printfn "Wyniki porównań: %A" result
with
| :? System.Exception as ex -> printfn "Błąd: %s" ex.Message
