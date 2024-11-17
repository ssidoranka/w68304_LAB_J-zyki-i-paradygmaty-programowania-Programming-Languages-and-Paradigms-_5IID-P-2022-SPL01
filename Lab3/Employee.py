class Employee:
    def __init__(self, name,surname, age, salary):
        self.name = name
        self.surname = surname
        self.age = age
        self.salary = salary

    def view(self):
        return f"Pracownik: \t{self.name} {self.surname} Wiek: \t{self.age} Wynagrodzenie: \t{self.salary}"