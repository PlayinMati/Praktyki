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

## Zadanie2
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

## Zadanie 3 
 
- Opis: Aplikacja powinna zarządzać asortymentem produktów, przyjmować "wirtualne" pieniądze i pozwalać na zakup. 
 
Główne cele: 
Stwórz abstrakcyjną klasę Product z właściwościami: Name (string) i Price (decimal). 
Stwórz klasy konkretnych produktów, które dziedziczą po Product, np.: 
Beverage (z dodatkową właściwością Volume w ml). 
Snack (z dodatkową właściwością Weight w gramach). 
Stwórz klasę VendingMachine, która będzie sercem aplikacji. Powinna ona zarządzać swoim stanem wewnętrznym: 
Przechowywać listę dostępnych produktów (np. w Dictionary<string, ProductSlot>, gdzie klucz to kod produktu, np. "A1"). 
Przechowywać aktualnie wrzuconą przez użytkownika kwotę (decimal jako CurrentCredit). 
Zaimplementuj metody publiczne w VendingMachine (interfejs automatu): 
InsertCoin(decimal amount): Dodaje pieniądze do puli kredytu. 
DisplayProducts(): Pokazuje listę dostępnych produktów, ich kody, ceny i ilość. 
SelectProduct(string productCode): Obsługuje logikę zakupu. Metoda musi sprawdzić: 
Czy produkt o danym kodzie istnieje. 
Czy jest dostępny (ilość > 0). 
Czy użytkownik wrzucił wystarczającą ilość pieniędzy. 
Jeśli wszystko się zgadza, metoda "wydaje" produkt (wyświetla komunikat), zmniejsza jego ilość w automacie i zwraca resztę. 
ReturnChange(): Pozwala użytkownikowi odebrać wrzucone pieniądze bez dokonywania zakupu. 
Główna część programu (Main): Stwórz instancję VendingMachine, "załaduj" go kilkoma produktami, a następnie w pętli stwórz menu, które pozwoli użytkownikowi na interakcję z automatem. 

- Wprowadzone zmiany: 
 
## Autor
Mateusz Bizoń
