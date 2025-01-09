//zad 1

//open System

//let rec fib n =
//    if n <= 1 then n
//    else fib (n - 1) + fib (n - 2)

//let fibTail n =
//    let rec aux n a b =
//        if n = 0 then a
//        elif n = 1 then b
//        else aux (n - 1) b (a + b)
//    aux n 0 1

//printf "n-ty wyraz ciągu Fibonacciego: "
//let n = int (Console.ReadLine())
//printfn "Prosta rekurencja: Fib(%d) = %d" n (fib n)
//printfn "Ogonowa rekurencja: Fib(%d) = %d" n (fibTail n)

// Zadanie 3: Generowanie permutacji listy
//open System

//let rec permute list =
//    match list with
//    | [] -> [[]] 
//    | head :: tail ->
//        let tailPermutations = permute tail 
//        let insertAtAllPositions perm =
//            [0..List.length perm] 
//            |> List.map (fun i ->
//                let prefix, suffix = List.splitAt i perm
//                prefix @ [head] @ suffix) 
//        tailPermutations |> List.collect insertAtAllPositions 

//printf "Podaj elementy listy oddzielone spacją: "
//let input = Console.ReadLine()
//let numbers = input.Split(' ') |> Array.map int |> Array.toList
//let permutations = permute numbers
//printfn "Wszystkie permutacje:"
//permutations |> List.iter (fun p -> printfn "%A" p)

//zad 2
//type Tree<'T> =
//    | Empty
//    | Node of 'T * Tree<'T> * Tree<'T>

//let rec searchTree value tree =
//    match tree with
//    | Empty -> false // Jeśli drzewo jest puste, zwróć false
//    | Node (v, left, right) ->
//        if v = value then true // Jeśli znajdziesz element, zwróć true
//        else
//            // Rekurencyjnie przeszukaj lewe i prawe poddrzewo
//            searchTree value left || searchTree value right

//let myTree = 
//    Node(10, 
//        Node(5, 
//            Node(2, Empty, Empty), 
//            Node(7, Empty, Empty)
//        ), 
//        Node(15, 
//            Empty, 
//            Node(99, Empty, Empty)
//        )
//    )

//let result1 = searchTree 9 myTree  
//let result2 = searchTree 99 myTree 

//printfn "Czy 9 jest w drzewie? %b" result1
//printfn "Czy 99 jest w drzewie? %b" result2
