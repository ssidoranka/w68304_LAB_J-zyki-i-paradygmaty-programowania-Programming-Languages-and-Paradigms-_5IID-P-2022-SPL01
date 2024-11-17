# class Pojazd:
#     def __init__(self, marka):
#         self.marka = marka
#
#     def opis(self):
#         return f"POjazd marki {self.marka}"
#
# class Samochod(Pojazd):
#     def __init__(self,marka,  model, rok):
#         super().__init__(marka)
#         self.model = model
#         self.rok = rok
#
#     def opis(self):
#         return f"Samochod {self.marka} {self.model} {self.rok}"
#
# samochod1 = Samochod("toyota", "corolla", "2024")
#
# print(samochod1.opis())

# from abc import ABC, abstractmethod
#
# class Zwierze(ABC):
#     @abstractmethod
#     def dzwiek(self):
#         pass
#
# class Lew(Zwierze):
#     def dzwiek(self):
#         return f"Lew wydaje glos"
#
# lew = Lew()
# print(lew.dzwiek())


from Employee import Employee
from EmployeesManager import EmployeeManager
from FrontendManager import FrontendManager
siemion = EmployeeManager("Siamion ","asd", 19, 200, [])
siemion.addEmployee("Pavel","asd", 100, 100)
siemion.addEmployee("Pavel1", "asd",30, 100)
siemion.addEmployee("Pavel2", "asd",44, 100)
siemion.addEmployee("Pavel3", "asd",64, 100)
print(siemion.findEmployee("Pavel2", "asd"))
# siemion.checkAge(53)
# print(siemion.showEmployee())
if __name__ == "__main__":
    frontend = FrontendManager()
    frontend.menu()
