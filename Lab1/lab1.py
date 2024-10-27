#zad1

# def podzielPaczki(wagi, max_waga):
#     for waga in wagi:
#         if waga > max_waga:
#             raise ValueError(f"Paczka o wadze {waga} przekracza max mdozwoloną wagękursu ({max_waga} kg).")
#     wagi_sort = sorted(wagi, reverse = True)
#     kursy = []
#     for waga in wagi_sort:
#         dodano = False
#         for kurs in kursy:
#             if sum(kurs) + waga <= max_waga:
#                 kurs.append(waga)
#                 dodano = True
#                 break
#         if not dodano:
#             kursy.append([waga])
#     return len(kursy), kursy
#
# wagi = [21,5,8,15,10,10,7]
# max_wag = 25
#
# #print(podzielPaczki(wagi, max_wag))
#
# liczba_kursow, kursy = podzielPaczki(wagi, max_wag)
# for i, kurs in enumerate(kursy, 1):
#     print(f"Kurs {i} : {kurs} - suma wagi : {sum(kurs)} kg")


#Zad2
# from collections import deque
# def bfsTask(graph, start, end):
#     queue = deque(start)
#     while queue:
#         path = queue.popleft()
#         node = path[-1]
#         if node == end:
#             return path, len(path)
#         else:
#             for adjacent in graph.get(node, []):
#                 queue.append(list(path) + [adjacent])
#
# graph = {
#     '1' : ['2','3','5'],
#     '2' : ['1','4'],
#     '3': ['1', '5', '6'],
#     '4': ['2', '7'],
#     '5': ['1', '3','7'],
#     '6': ['3', '8'],
#     '7': ['4', '5', '8'],
#     '8': ['6', '7'],
# }
# print(bfsTask(graph, '1', '4'))

#zad3

# zadania = [
#     {'czas': 4, 'nagroda': 10},
#     {'czas': 2, 'nagroda': 5},
#     {'czas': 1, 'nagroda': 3},
#     {'czas': 3, 'nagroda': 8},
# ]
#
#
# def procedur(zadania):
#
#     zadania.sort(key=lambda x: x['czas'])
#
#     czas_oczekiwania = 0
#     aktualny_czas = 0
#     kolejnosc = []
#
#     for zadanie in zadania:
#         kolejnosc.append(zadanie)
#         czas_oczekiwania += aktualny_czas
#         aktualny_czas += zadanie['czas']
#
#     return kolejnosc, czas_oczekiwania
#
#
# kolejnosc, czas_oczekiwania = procedur(zadania)
# print("Kolejność zadań:", kolejnosc)
# print("Czas oczekiwania:", czas_oczekiwania)

# Zad 5
# Zadania: (czas rozpocz-ęcia, czas zakończenia, nagroda)
# zadania = [
#     (1, 4, 5),
#     (3, 5, 1),
#     (0, 6, 8),
#     (5, 7, 8),
#     (3, 9, 6),
#     (5, 9, 4),
#     (6, 10, 2),
#     (8, 11, 4)
# ]
#
# def procedur(zadania):
#
#     zadania.sort(key=lambda x: x[1])
#     wybrane_zadania = []
#     maks_nagroda = 0
#     ostatnie_zakonczenie = 0
#
#     for zadanie in zadania:
#         start, koniec, nagroda = zadanie
#         if start >= ostatnie_zakonczenie:
#             wybrane_zadania.append(zadanie)
#             maks_nagroda += nagroda
#             ostatnie_zakonczenie = koniec
#
#     return maks_nagroda, wybrane_zadania
#
# maks_nagroda, wybrane_zadania = procedur(zadania)
# print("Maksymalna nagroda:", maks_nagroda)
# print("Wybrane zadania:", wybrane_zadania)

