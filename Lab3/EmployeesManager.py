from Employee import Employee

class EmployeeManager(Employee):

    def __init__(self, name, surname, age, salary, listofEmployee):

        super().__init__(name,surname,  age, salary)
        self.listofEmployee = []

    def addEmployee(self, name, surname, age, salary):
        empl = Employee(name, surname, age, salary)
        self.listofEmployee.append([empl.name, empl.surname,  empl.age, empl.salary])

    def showEmployee(self):
        return f"{self.listofEmployee}"

    def checkAge(self, whatAge):
        self.listofEmployee = list(filter(lambda x: x[2] < whatAge, self.listofEmployee))

    def findEmployee(self, name, surname):
        lista = list(filter(lambda x: x[0] == name and x[1] == surname, self.listofEmployee))
        return f"Znalezione pracowniki: {lista}"

    def newSalary(self, name, surname, salary):
        for i in self.listofEmployee:
            if i == self.findEmployee(name, surname):
                i[3] = salary


