# from functools import reduce
#
# listaA = [1, 2, 3, 4, 5,6 , 7, 8, 9, 10]
# listaB = [x ** 2 for x in listaA if x % 2 ==0]
# print(listaB)
#
# #map
#
# listaC = list(map(lambda x: x ** 2, listaA))
#
# #filter
#
# listaD = list(filter(lambda x: x % 2 == 0, listaC))
# print(listaD)
#
# #reduce
#
# tekst = """listaE = reduce(lambda x, y: x+y, listaD)
# print(listaE)"""
#
# exec(tekst)
#
# print(eval("1 + 1"))

import re
from collections import Counter


# zad 1

# def liczenie(text):
#     paragraf = len(list(filter(lambda x: x == "\n", text)))
#     paragraf1 = [p for p in text.strip().split("\n") if p]
#     iloscParag = len(paragraf1)
#     zdanie1 = len(list(filter(lambda x: x == "." or x == "!" or x == "?", text)))
#     zdanie2 = re.split(r'[.!?]+', text)
#     zdanie2 = [tekst.strip() for tekst in zdanie2 if tekst.strip()]
#     iloscZdanie2 = len(zdanie2)
#
#     stop_words = ["i", "a", "w", "z", "na"]
#     usow_znaki = re.sub(r'[^\w\s]', '', text.lower())
#
#     return paragraf, iloscParag, zdanie1, iloscZdanie2
#
#
# text = """Napisz program, który przyjmuje długi tekst (np. artykuł, książkę) i wykonuje kilka złożonych operacji analizy tekstu:
# Zlicza liczbę słów, zdań, i akapitów w tekście.
# Wyszukuje najczęściej występujące słowa, wykluczając tzw stop words (np "i", "a", "the").
# Transformuje wszystkie wyrazy rozpoczynające się na literę "a" lub "A" do ich odwrotności (np.
# "apple" → "elppa").
# Wskazówka: Użyj map(), filter(), i list składanych, aby przeprowadzać transformacje na tekście.
# """
#
# p1, p2, p3, p4 = liczenie(text)
# print(p1)
# print(p2)
# print(p3)
# print(p4)


# import numpy as np
#Zad 2
#
# matrices = {}
# def add_matr(nazwa, macierz):
#     matrices[nazwa] = np.array(macierz)
#
# def check_sum(lewa, prawa):
#
#     if lewa not in matrices or prawa not in matrices:
#         raise ValueError("Macierz nie została zdefiniowana.")
#     if matrices[lewa].shape != matrices[prawa].shape:
#         raise ValueError("Macierze mają niezgodne wymiary do dodawania.")
#     return True
#
#
# def check_multiplic(lewa, prawa):
#
#     if lewa not in matrices or prawa not in matrices:
#         raise ValueError("Macierz nie została zdefiniowana.")
#     if matrices[lewa].shape[1] != matrices[prawa].shape[0]:
#         raise ValueError("Macierze mają niezgodne wymiary do mnożenia.")
#     return True
#
# def check_transp(macierz_nazwa):
#     if macierz_nazwa not in matrices:
#         raise ValueError("Macierz nie została zdefiniowana.")
#     return True
#
#
# def check_operation(operacja):
#     tokens = operacja.replace(" ", "").split('=')
#     if len(tokens) < 2:
#         raise ValueError("Niepoprawne wyrażenie operacji.")
#
#     wyrazenie = tokens[1]
#
#     if '+' in wyrazenie:
#         lewa, prawa = wyrazenie.split('+')
#         return check_sum(lewa, prawa)
#     elif '*' in wyrazenie:
#         lewa, prawa = wyrazenie.split('*')
#         return check_multiplic(lewa, prawa)
#     elif '.T' in wyrazenie:
#         macierz_nazwa = wyrazenie.replace('.T', '')
#         return check_transp(macierz_nazwa)
#     else:
#         raise ValueError("Nieobsługiwana operacja.")
#
#
# def do_oper(operacja):
#     if check_operation(operacja):
#         lewa_strona, wyrazenie = operacja.split('=')
#         lewa_strona = lewa_strona.strip()
#
#         wynik = eval(wyrazenie, {"__builtins__": None}, matrices)
#         matrices[lewa_strona] = wynik
#         return wynik
#
# add_matr('A', [[1, 2], [3, 4]])
# add_matr('B', [[5, 6], [7, 8]])
#
#
# print("C =", do_oper("C = A + B"))

# Zad 3
# def analyze_data(data):
#     numbers = list(filter(lambda x: isinstance(x, (int, float)), data))
#     max_number = max(numbers) if numbers else None
#
#     strings = list(filter(lambda x: isinstance(x, str), data))
#     longest_string = max(strings, key=len) if strings else None
#
#     tuples = list(filter(lambda x: isinstance(x, tuple), data))
#     largest_tuple = max(tuples, key=len) if tuples else None
#
#     return max_number, longest_string, largest_tuple
#
# data1 = [42, "apple", (1,), [10, 20], "banana", (1, 2, 3, 4), "a longer string", 85.5, "short"]
#
# max_number, longest_string, largest_tuple = analyze_data(data1)
#
# print("Largest number:", max_number)
# print("Longest string:", longest_string)
# print("Tuple with the most elements:", largest_tuple)

# Zad 4
# import numpy as np
# from functools import reduce
#
#
# def combine_matrices(matrices, operation):
#     if not matrices:
#         raise ValueError("Lista macierzy jest pusta.")
#
#     def apply_operation(a, b):
#         return eval(operation, {"a": a, "b": b, "__builtins__": None})
#
#     return reduce(apply_operation, matrices)
#
# matrices = [
#     np.array([[15, 2], [73, 48]]),
#     np.array([[45, 16], [27, 81]]),
#     np.array([[92, 10], [11, 12]])
# ]
#
# result_sum = combine_matrices(matrices, "a + b")
# print("Suma macierzy:\n", result_sum)
