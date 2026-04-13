# Praktyki

Repozytorium zawiera zadania wykonywane w trakcie praktyk.

## Struktura
- Zadanie1
- Zadanie2
- Zadanie3

## Technologie
- C#
- Visual Studio

## Zadanie1
- Opis:  Zadaniem jest stworzenie aplikacji, która wczytuje plik tekstowy z logami systemowymi i wykonuje prostą analizę tych danych. 
Program powinien zliczyć, ile występuje logów każdego typu (INFO, WARNING, ERROR, DEBUG) i zapisać raport do nowego pliku. 
Plik z logami macie w załączniku. 

Wczytaj plik tekstowy (każda linia to jeden wpis logu). 
Policz wystąpienia różnych poziomów logów. 
Wyświetl w konsoli podsumowanie. 
Wypisz wszystkie błędy (ERROR) osobno do pliku (errors.txt). 
Zapisz raport do nowego pliku tekstowego (raport.txt). 

- Wprowadznone Zmiany: Dodałem try catch przy otwieraniu pliku. Dodałem mierzenie czasu działania aplikacji.

## Zadanie1
- Opis: Zadaniem jest stworzenie w aplikacji konsolowej symulatora prostej, turowej walki rodem z gier RPG. Gracz będzie kontrolował bohatera, który walczy z losowo wybranym przeciwnikiem.  
Główne cele: 
Stwórz abstrakcyjną klasę bazową Character, która będzie zawierać wspólne cechy dla wszystkich postaci w grze: 
Właściwości: Name (string), HealthPoints (int), MaxHealthPoints (int), AttackPower (int). 
Metody: TakeDamage(int damage) (obniża HealthPoints), IsAlive() (zwraca true, jeśli HealthPoints > 0). 
Metodę abstrakcyjną: Attack(Character target). 

Stwórz klasy dziedziczące po klasie Character: 
Hero: Reprezentuje postać gracza. 
Monster: Reprezentuje przeciwnika. Możesz stworzyć kilka różnych potworów, które również dziedziczą po Monster i mają różne statystyki. 

Zaimplementuj logikę ataku: Każda klasa (Hero, Monster) musi implementować metodę Attack w unikalny sposób. Na przykład: 
Atak Hero może mieć szansę na cios krytyczny (zadaje podwójne obrażenia). 
Atak Goblin może być szybki, ale słaby. 
Atak Orc może być powolny, ale bardzo silny. 

Stwórz klasę BattleArena, która będzie zarządzać przebiegiem pojedynku. 
W konstruktorze powinna przyjmować dwie postacie (Character player, Character opponent). 
Powinna zawierać metodę StartBattle(), która w pętli while będzie kontynuować walkę, dopóki jedna z postaci nie zginie. W każdej turze gracz atakuje przeciwnika, a następnie przeciwnik atakuje gracza. 
Po każdej akcji, na konsoli powinien pojawić się czytelny log, np. "Hero 'Arthur' attacks and deals 15 damage to the Goblin! The Goblin now has 35/50 HP." 

Główna część programu (Main): Stwórz obiekt Hero i losowego Monster, a następnie przekażcie ich do BattleArena i rozpocznij pojedynek. 

- Wprowadzone zmiany: 
## Autor
Mateusz Bizoń
