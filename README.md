#  Praktyki

Repozytorium zawiera zadania wykonywane w trakcie praktyk.

---

##  Struktura projektu
- Zadanie1
- Zadanie2
- Zadanie3

---

##  Technologie
- C#
- Visual Studio

---

##  Zadanie 1 - Analiza logów

### Opis
Aplikacja wczytuje plik tekstowy z logami systemowymi i wykonuje ich analizę:
- zlicza logi (INFO, WARNING, ERROR, DEBUG)
- wyświetla podsumowanie w konsoli
- zapisuje błędy (ERROR) do pliku `errors.txt`
- generuje raport `raport.txt`

### Wprowadzone zmiany
- dodano obsługę błędów (`try-catch`)
- dodano pomiar czasu działania aplikacji

---

##  Zadanie 2 - Symulator walki RPG

### Opis
Aplikacja symuluje turową walkę bohatera z przeciwnikiem:
- abstrakcyjna klasa `Character`
- klasy dziedziczące: `Hero`, `Monster`
- różne typy przeciwników (np. Goblin, Orc)
- system walki turowej (`BattleArena`)
- logi walki w konsoli

### Wprowadzone zmiany
- Podawanie imienia bohatera w konosli przez gracza
- Wyswietlanie informacji o walce po kliknięciu dowolnego klawisza

---

##  Zadanie 3 - Automat vendingowy

### Opis
Aplikacja symuluje działanie automatu z produktami:
- abstrakcyjna klasa `Product`
- kategorie: `Beverage`, `Snack`
- zarządzanie stanem automatu (`VendingMachine`)
- obsługa płatności i wydawania reszty
- interakcja przez konsolę

### Wprowadzone zmiany
- dodano kod administracyjny `0000` do zarządzania produktami
- komunikat, gdy automat jest pusty
- dodano kategorie `Bread` dla pieczywa

---

##  Autor
Mateusz Bizoń
