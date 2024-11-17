import json
from EmployeesManager import EmployeeManager

class FrontendManager:
    def __init__(self):
        self.employee_managers = []
        self.data_file = "employees.json"  # Ścieżka do pliku z danymi
        self.load_data()

    def menu(self):
        if not self.login():
            print("Nieprawidłowe dane logowania. Zamykam system.")
            return

        while True:
            print("\nMenu:")
            print("1. Dodaj nowego pracownika")
            print("2. Wyświetl listę pracowników")
            print("3. Usuń pracowników według przedziału wiekowego")
            print("4. Zaktualizuj wynagrodzenie pracownika")
            print("5. Wyjdź")

            choice = input("Wybierz opcję: ")

            if choice == "1":
                self.add_employee()
            elif choice == "2":
                self.show_employees()
            elif choice == "3":
                self.remove_employees_by_age()
            elif choice == "4":
                self.update_employee_salary()
            elif choice == "5":
                self.save_data()
                print("Do widzenia!")
                break
            else:
                print("Nieprawidłowy wybór. Spróbuj ponownie.")

    def login(self):
        print("Logowanie do systemu:")
        username = input("Nazwa użytkownika: ")
        password = input("Hasło: ")
        return username == "admin" and password == "admin"

    def load_data(self):
        try:
            with open(self.data_file, "r") as file:
                data = json.load(file)
                for item in data:
                    manager = EmployeeManager(
                        item["name"], item["surname"], item["age"], item["salary"], []
                    )
                    self.employee_managers.append(manager)
            print("Dane zostały wczytane z pliku.")
        except FileNotFoundError:
            print("Plik z danymi nie istnieje. Tworzenie nowej bazy danych.")
        except json.JSONDecodeError:
            print("Plik z danymi jest uszkodzony. Rozpoczynam z pustą listą.")

    def save_data(self):
        data = [
            {
                "name": manager.name,
                "surname": manager.surname,
                "age": manager.age,
                "salary": manager.salary,
            }
            for manager in self.employee_managers
        ]
        with open(self.data_file, "w") as file:
            json.dump(data, file, indent=4)
        print("Dane zostały zapisane do pliku.")

    def add_employee(self):
        name = input("Podaj imię: ")
        surname = input("Podaj nazwisko: ")
        age = int(input("Podaj wiek: "))
        salary = float(input("Podaj wynagrodzenie: "))

        new_employee = EmployeeManager(name, surname, age, salary, [])
        self.employee_managers.append(new_employee)
        print("Pracownik został dodany.")
        self.save_data()

    def show_employees(self):
        print("Lista pracowników:")
        if self.employee_managers:  # Sprawdź, czy lista nie jest pusta
            for manager in self.employee_managers:
                print(f"{manager.name} {manager.surname} - Wiek: {manager.age}, Wynagrodzenie: {manager.salary}")
        else:
            print("Brak pracowników w systemie.")

    def remove_employees_by_age(self):
        max_age = int(input("Podaj maksymalny wiek: "))
        self.employee_managers = [manager for manager in self.employee_managers if manager.age < max_age]
        print(f"Pracownicy starsi niż {max_age} zostali usunięci.")
        self.save_data()  # Aktualizacja pliku

    def update_employee_salary(self):
        name = input("Podaj imię pracownika: ")
        surname = input("Podaj nazwisko pracownika: ")
        new_salary = float(input("Podaj nowe wynagrodzenie: "))

        for manager in self.employee_managers:
            if manager.name == name and manager.surname == surname:
                manager.salary = new_salary
                print(f"Wynagrodzenie pracownika {name} {surname} zostało zaktualizowane.")
                self.save_data()
                return

        print(f"Pracownik {name} {surname} nie został znaleziony.")
