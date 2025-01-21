// type Person(name: string, age: int) = 
//     //pola prywatne
//     let mutable privateAge = age

//     //wlasciwosci
//     member this.Name = name

//     //get i set
//     member this.Age
//             with get() = privateAge
//             and set(value) =
//                 if value > 0 then
//                     privateAge <- value
//                 else
//                     printfn " Wiek musi byc liczbą dodatnią"

//     member this.View() =
//         printfn "Witaj %s masz %d lat" this.Name this.Age


// //klasa pochodna
// type Student(name:string, age:int, nrAlbumu: string) =
//     inherit Person(name, age)

//     //wlasciwosc
//     member this.NrAlbumu = nrAlbumu

//     override this.View() =
//         printfn "Witaj %s masz %d lat, numer albumu %s" this.Name this.Age this.NrAlbumu

// //obiekt klasy

// let person = Person("Jan", 23)
// person.View()


// [<AbstractClass>]
// type Shape()=
//     abstract member Area: unit -> float

//     member this.View()=
//         printfn " to jest zwykla metoda klasy abstrakcyjnej"

// type Circle(radius: float)=
//     inherit Shape()

//     override this.Area ()=
//         System.Math.PI * radius * radius


// type IShape =

//     abstract member Area: float
//     abstract member Area1: unit -> float

// type Circle1(radius: float) =
//     interface IShape with
//         //właśiwości
//         member this.Area = System.Math.PI * radius * radius

//         //metoda
//         member this.Area1 (): float = 
//             System.Math.PI * radius * radius

open System
type Book(title:string, autor:string, pages:int) = 
    member this.Title = title
    member this.Autor = autor
    member this.Pages = pages


    member this.GetInfo() =
        sprintf "Tytuł %s, Autor %s, liczba stron %d " this.Title this.Autor this.Pages

type User(name: string) = 

    let borrowBooks = System.Collections.Generic.List<Book>()
    member this.Name = name

    member this.BorrowBook(book:Book) = 
        borrowBooks.Add(book)
        printfn "%s wypozyczyl ksiazke:  \"%s\"" this.Name book.Title

    member this.ReturnBook(book:Book) = 
        if borrowBooks.Contains(book) then
            borrowBooks.Remove(book)
            printfn "%s zwrócił ksiazke \"%s\"" this.Name book.Title

        else    
            printfn "%s nie ma ksiazki do wrócenia \"%s\"" this.Name book.Title
            
    member this.ListBorrowBooks() = 
        if borrowBooks.Count > 0 then
            borrowBooks
            |>  Seq.map(fun book -> book.GetInfo())
            |>  String.concat "\n"
            |>  printfn "Książki wypożyczone przez %s: \n%s" this.Name 
        else
            printfn "%s nie ma wypozyczionych książek" this.Name



type BankAccount(accountNumber: string, initialBalance: float) =
    // Pola prywatne
    let mutable balance = initialBalance

    // Właściwości
    member this.AccountNumber = accountNumber
    member this.Balance = balance

    // Metoda do wpłaty
    member this.Deposit(amount: float) =
        if amount > 0.0 then
            balance <- balance + amount
            printfn "Wpłacono %.2f na konto %s. Nowe saldo: %.2f" amount this.AccountNumber balance
        else
            printfn "Błąd: Kwota wpłaty musi być dodatnia."

    // Metoda do wypłaty
    member this.Withdraw(amount: float) =
        if amount > 0.0 then
            if balance >= amount then
                balance <- balance - amount
                printfn "Wypłacono %.2f z konta %s. Nowe saldo: %.2f" amount this.AccountNumber balance
            else
                printfn "Błąd: Niedostateczne środki na koncie."
        else
            printfn "Błąd: Kwota wypłaty musi być dodatnia."

type Library() = 
    let mutable books = System.Collections.Generic.List<Book>()

    member this.AddBook(book: Book) = 
        books.Add(book)
        printf "Książka \%s\" została dodna" book.Title

    member this.RemoveBook(book: Book) =
        if books.Contains(book) then
            books.Remove(book) 
            printf "Książka \"%s\" została usunięta" book.Title
        else
            printfn "NIe znaleziono ksiazki"

    member this.ListBooks() = 
        if books.Count > 0 then
            books
            |>  Seq.map(fun book -> book.GetInfo())
            |>  String.concat "\n"
            |>  printfn "Ksiazki w biblioteceL: \n%s" 
        else
            printfn "W bibliotece nie ma ksiazek"

let main() = 
    // let library = Library()
    // let user = User("Kobyla")

    // let book1 = Book("ksiazka1", "Autor1", 321)
    // let book2 = Book("ksiazka2", "Autor3", 321)
    // let book3 = Book("ksiazka3", "Autor3", 321)

    // library.AddBook(book1)
    // library.AddBook(book2)
    // library.AddBook(book3)

    // library.ListBooks()

    // user.BorrowBook(book1)
    // user.BorrowBook(book2)

    // library.ListBooks()

    // user.ListBorrowBooks()

    // user.ReturnBook(book1)
    // user.ListBorrowBooks()

    let account = BankAccount("123456789", 1000.0)
    account.Deposit(500.0)
    account.Withdraw(200.0)
    account.Withdraw(1500.0)

main()