open System
open System.Data.SQLite

type Appointment(patientName: string, doctorName: string, appointmentTime: DateTime) =
    member this.PatientName = patientName
    member this.DoctorName = doctorName
    member this.AppointmentTime = appointmentTime

    member this.GetInfo() =
        sprintf "Pacjent: %s, Lekarz: %s" this.PatientName this.DoctorName 

type Clinic(databasePath: string) =
    do

        let connection = new SQLiteConnection($"Data Source={databasePath}")
        connection.Open()
        let command = connection.CreateCommand()
        command.CommandText <- """
            CREATE TABLE IF NOT EXISTS Doctors (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL
            );
            CREATE TABLE IF NOT EXISTS Patients (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL
            );
            CREATE TABLE IF NOT EXISTS Appointments (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                PatientId INTEGER NOT NULL,
                DoctorId INTEGER NOT NULL,
                AppointmentTime TEXT NOT NULL,
                FOREIGN KEY(PatientId) REFERENCES Patients(Id),
                FOREIGN KEY(DoctorId) REFERENCES Doctors(Id)
            );
        """
        command.ExecuteNonQuery()
        connection.Close()

    member this.AddDoctor(name: string) =
        let connection = new SQLiteConnection($"Data Source={databasePath}")
        connection.Open()
        let command = connection.CreateCommand()
        command.CommandText <- "INSERT INTO Doctors (Name) VALUES (@Name)"
        command.Parameters.AddWithValue("@Name", name) |> ignore
        command.ExecuteNonQuery()
        connection.Close()
        printfn "Dodano lekarza: %s" name

    member this.AddPatient(name: string) =
        let connection = new SQLiteConnection($"Data Source={databasePath}")
        connection.Open()
        let command = connection.CreateCommand()
        command.CommandText <- "INSERT INTO Patients (Name) VALUES (@Name)"
        command.Parameters.AddWithValue("@Name", name) |> ignore
        command.ExecuteNonQuery()
        connection.Close()
        printfn "Dodano pacjenta: %s" name

    member this.ShowDoctors() =
        let connection = new SQLiteConnection($"Data Source={databasePath}")
        connection.Open()
        let command = connection.CreateCommand()
        command.CommandText <- "SELECT Name FROM Doctors"
        let reader = command.ExecuteReader()
        if reader.HasRows then
            printfn "Lista lekarzy:"
            while reader.Read() do
                printfn "- %s" (reader.GetString(0))
        else
            printfn "Brak lekarzy w bazie."
        reader.Close()
        connection.Close()

    member this.ShowPatients() =
        let connection = new SQLiteConnection($"Data Source={databasePath}")
        connection.Open()
        let command = connection.CreateCommand()
        command.CommandText <- "SELECT Name FROM Patients"
        let reader = command.ExecuteReader()
        if reader.HasRows then
            printfn "Lista pacjentów:"
            while reader.Read() do
                printfn "- %s" (reader.GetString(0))
        else
            printfn "Brak pacjentów w bazie."
        reader.Close()
        connection.Close()

    member this.BookAppointment(patientName: string, doctorName: string, appointmentTime: DateTime) =
        if appointmentTime.Hour < 8 || appointmentTime.Hour >= 17 then
            printfn "Nie można zarezerwować wizyty poza godzinami pracy lekarza (8:00-17:00)."
        else
            let connection = new SQLiteConnection($"Data Source={databasePath}")
            connection.Open()

            let command = connection.CreateCommand()
            command.CommandText <- "SELECT Id FROM Patients WHERE Name = @PatientName"
            command.Parameters.AddWithValue("@PatientName", patientName) |> ignore
            let patientId =
                match command.ExecuteScalar() with
                | :? int64 as id -> id
                | _ ->
                    printfn "Nie znaleziono pacjenta: %s" patientName
                    connection.Close()
                    -1L 

            if patientId = -1L then
                connection.Close()
            else
                command.CommandText <- "SELECT Id FROM Doctors WHERE Name = @DoctorName"
                command.Parameters.Clear()
                command.Parameters.AddWithValue("@DoctorName", doctorName) |> ignore
                let doctorId =
                    match command.ExecuteScalar() with
                    | :? int64 as id -> id
                    | _ ->
                        printfn "Nie znaleziono lekarza: %s" doctorName
                        connection.Close()
                        -1L 

                if doctorId = -1L then
                    connection.Close()
                else
                    command.CommandText <- "SELECT AppointmentTime FROM Appointments WHERE DoctorId = @DoctorId ORDER BY AppointmentTime DESC LIMIT 1"
                    command.Parameters.Clear()
                    command.Parameters.AddWithValue("@DoctorId", doctorId) |> ignore
                    let lastAppointmentTime =
                        match command.ExecuteScalar() with
                        | :? string as timeStr -> DateTime.Parse(timeStr)
                        | _ -> DateTime.MinValue

                    if lastAppointmentTime <> DateTime.MinValue && (appointmentTime - lastAppointmentTime).TotalMinutes < 30.0 then
                        printfn "Nie można zarezerwować wizyty. Ostatnia wizyta lekarza %s była mniej niz 30 minut temu, wizyty muszą być oddalone o co najmniej 30 minut." doctorName 
                    else
                        command.CommandText <- "INSERT INTO Appointments (PatientId, DoctorId, AppointmentTime) VALUES (@PatientId, @DoctorId, @AppointmentTime)"
                        command.Parameters.Clear()
                        command.Parameters.AddWithValue("@PatientId", patientId) |> ignore
                        command.Parameters.AddWithValue("@DoctorId", doctorId) |> ignore
                        command.Parameters.AddWithValue("@AppointmentTime", appointmentTime.ToString("yyyy-MM-dd HH:mm")) |> ignore
                        command.ExecuteNonQuery()
                        connection.Close()
                        printfn "Zarezerwowano wizytę: Pacjent: %s, Lekarz: %s" patientName doctorName 

    member this.UpdateAppointment(appointmentId: int, newAppointmentTime: DateTime) =
        let connection = new SQLiteConnection($"Data Source={databasePath}")
        connection.Open()
        let command = connection.CreateCommand()
        command.CommandText <- "SELECT Id FROM Appointments WHERE Id = @AppointmentId"
        command.Parameters.AddWithValue("@AppointmentId", appointmentId) |> ignore
        let exists =
            match command.ExecuteScalar() with
            | :? int64 -> true
            | _ -> false

        if exists then
            command.CommandText <- "UPDATE Appointments SET AppointmentTime = @NewAppointmentTime WHERE Id = @AppointmentId"
            command.Parameters.AddWithValue("@NewAppointmentTime", newAppointmentTime.ToString("yyyy-MM-dd HH:mm")) |> ignore
            command.ExecuteNonQuery()
            printfn "Zaktualizowano wizytę o ID %d " appointmentId 
        else
            printfn "Nie znaleziono wizyty o ID %d." appointmentId
        connection.Close()

    member this.DeleteAppointment(appointmentId: int) =
        let connection = new SQLiteConnection($"Data Source={databasePath}")
        connection.Open()
        let command = connection.CreateCommand()
        command.CommandText <- "DELETE FROM Appointments WHERE Id = @AppointmentId"
        command.Parameters.AddWithValue("@AppointmentId", appointmentId) |> ignore
        let rowsAffected = command.ExecuteNonQuery()
        if rowsAffected > 0 then
            printfn "Usunięto wizytę o ID %d." appointmentId
        else
            printfn "Nie znaleziono wizyty o ID %d." appointmentId
        connection.Close()

    member this.ShowAppointments() =
        let connection = new SQLiteConnection($"Data Source={databasePath}")
        connection.Open()
        let command = connection.CreateCommand()
        command.CommandText <- """
            SELECT a.Id, p.Name AS PatientName, d.Name AS DoctorName, a.AppointmentTime
            FROM Appointments a
            JOIN Patients p ON a.PatientId = p.Id
            JOIN Doctors d ON a.DoctorId = d.Id
            ORDER BY AppointmentTime
        """
        let reader = command.ExecuteReader()
        if reader.HasRows then
            while reader.Read() do
                printfn "ID: %d, Pacjent: %s, Lekarz: %s, Data: %s" (reader.GetInt32(0)) (reader.GetString(1)) (reader.GetString(2)) (reader.GetString(3))
        else
            printfn "Brak zapisanych wizyt."
        reader.Close()
        connection.Close()

    member this.InteractiveMenu() =
        let mutable isRunning = true
        while isRunning do
            printfn "\nMenu Główne"
            printfn "1. Dodaj lekarza"
            printfn "2. Dodaj pacjenta"
            printfn "3. Zarezerwuj wizytę"
            printfn "4. Wyświetl wszystkich lekarzy"
            printfn "5. Wyświetl wszystkich pacjentów"
            printfn "6. Wyświetl wszystkie wizyty"
            printfn "7. Zaktualizuj wizytę"
            printfn "8. Usuń wizytę"
            printfn "9. Wyjście"
            printf "Wybierz opcję: "
            let choice = Console.ReadLine()

            match choice with
            | "1" ->
                printf "Podaj imię i nazwisko lekarza: "
                let doctorName = Console.ReadLine()
                this.AddDoctor(doctorName)
            | "2" ->
                printf "Podaj imię i nazwisko pacjenta: "
                let patientName = Console.ReadLine()
                this.AddPatient(patientName)
            | "3" ->
                printf "Podaj imię i nazwisko pacjenta: "
                let patientName = Console.ReadLine()
                printf "Podaj imię i nazwisko lekarza: "
                let doctorName = Console.ReadLine()
                printf "Podaj datę i godzinę wizyty (yyyy-MM-dd HH:mm): "
                let appointmentTime = DateTime.Parse(Console.ReadLine())
                this.BookAppointment(patientName, doctorName, appointmentTime)
            | "4" -> this.ShowDoctors()
            | "5" -> this.ShowPatients()
            | "6" -> this.ShowAppointments()
            | "7" ->
                printf "Podaj ID wizyty do aktualizacji: "
                let appointmentId = int (Console.ReadLine())
                printf "Podaj nową datę i godzinę wizyty (yyyy-MM-dd HH:mm): "
                let newAppointmentTime = DateTime.Parse(Console.ReadLine())
                this.UpdateAppointment(appointmentId, newAppointmentTime)
            | "8" ->
                printf "Podaj ID wizyty do usunięcia: "
                let appointmentId = int (Console.ReadLine())
                this.DeleteAppointment(appointmentId)
            | "9" ->
                isRunning <- false
                printfn "Zakończono działanie programu."
            | _ -> printfn "Nieprawidłowa opcja, spróbuj ponownie."

let main() =
    let databasePath = "clinic.db"
    let clinic = Clinic(databasePath)
    clinic.InteractiveMenu()

main()
