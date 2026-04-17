using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    // --- MODELE DANYCH ---

    public abstract class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Beverage : Product
    {
        public int Volume;
    }

    public class Snack : Product
    {
        public int Weight;
    }

    public class Bread : Product
    {
        public int ExpirationDays;
    }

    // Klasa reprezentująca "półkę" w automacie
    public class ProductSlot
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    // --- LOGIKA AUTOMATU ---

    public class VendingMachine
    {
        // Słownik przechowujący produkty pod kluczami (np. "A1")
        public Dictionary<string, ProductSlot> ProductList { get; set; } = new Dictionary<string, ProductSlot>();

        public decimal CurrentCredit { get; set; }

        public void InsertCoin(decimal amount)
        {
            CurrentCredit = amount;
        }

        public void AddProduct(string code, Product product, int quantity)
        {
            ProductList[code] = new ProductSlot { Product = product, Quantity = quantity };
        }

        public void DisplayProducts()
        {
            Console.WriteLine("\n======= AUTOMAT Z PRZEKĄSKAMI =======");
            if (ProductList.Count == 0)
            {
                Console.WriteLine("Maszyna jest obecnie pusta.");
            }

            foreach (var pair in ProductList)
            {
                var slot = pair.Value;
                if(slot.Quantity <= 0)
                {
                    Console.WriteLine($"[{pair.Key}] {slot.Product.Name.PadRight(12)} | WYPRZEDANY");
                }
                else
                Console.WriteLine($"[{pair.Key}] {slot.Product.Name.PadRight(12)} | Cena: {slot.Product.Price:F2} PLN | Sztuk: {slot.Quantity}");
            }
            Console.WriteLine("==================================\n");
        }

        public void SelectProduct(string productsCode)
        {
            if (ProductList.ContainsKey(productsCode))
            {
                ProductSlot slot = ProductList[productsCode];
                if (slot.Quantity > 0)
                {
                    if (CurrentCredit >= slot.Product.Price)
                    {
                        Console.WriteLine($"\n[WYDANO]: {slot.Product.Name}");
                        slot.Quantity--;
                        CurrentCredit -= slot.Product.Price;
                        Console.WriteLine($"Pozostały kredyt: {CurrentCredit} PLN");
                    }
                    else
                    {
                        Console.WriteLine("\nBłąd: Niewystarczające środki (kredyt musi być wyższy niż cena).");
                    }
                }
                else
                {
                    Console.WriteLine("\nBłąd: Produkt wyprzedany.");
                }
            }
            else
            {
                Console.WriteLine("\nBłąd: Niepoprawny kod produktu.");
            }
        }
    }

    // --- PROGRAM GŁÓWNY ---

    internal class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vendingMachine = new VendingMachine();

            // Produkty startowe
            vendingMachine.AddProduct("A1", new Beverage { Name = "Coca-Cola", Price = 5.50m, Volume = 500 }, 5);
            vendingMachine.AddProduct("A2", new Snack { Name = "Lay's", Price = 7.00m, Weight = 150 }, 3);
            vendingMachine.AddProduct("A3", new Bread { Name = "Rogalik", Price = 3.50m, ExpirationDays = 2 }, 4);

            vendingMachine.DisplayProducts();

            Console.Write("Wprowadź kwotę doładowania (PLN): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal initialCredit))
            {
                vendingMachine.InsertCoin(initialCredit);
            }

            while (true)
            {
                Console.WriteLine($"\nAktualny kredyt: {vendingMachine.CurrentCredit} PLN");
                Console.Write("Wprowadź kod produktu (lub 'exit'): ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;
                if (input.ToLower() == "exit") break;

                // TRYB ADMINISTRATORA
                if (input == "0000")
                {
                    bool inAdminMenu = true;
                    while (inAdminMenu)
                    {
                        Console.WriteLine("\n--- PANEL ADMINISTRATORA ---");
                        Console.WriteLine("1. Uzupełnij zapas (istniejący produkt)");
                        Console.WriteLine("2. Dodaj całkowicie nowy produkt");
                        Console.WriteLine("3. Wyjdź z panelu");
                        Console.Write("Wybór: ");

                        string adminChoice = Console.ReadLine();

                        switch (adminChoice)
                        {
                            case "1": // UZUPEŁNIANIE
                                Console.WriteLine("\nFormat: [kod] [ilość_do_dodania]");
                                string restockInput = Console.ReadLine();
                                string[] restockParts = restockInput?.Split(' ');

                                if (restockParts?.Length == 2 && vendingMachine.ProductList.ContainsKey(restockParts[0]))
                                {
                                    if (int.TryParse(restockParts[1], out int additional))
                                    {
                                        vendingMachine.ProductList[restockParts[0]].Quantity += additional;
                                        Console.WriteLine("Zapas uzupełniony.");
                                    }
                                }
                                else { Console.WriteLine("Błąd: Nieprawidłowy kod lub dane."); }
                                break;
                            // Fragment kodu wewnątrz menu administratora (case "2")
                            case "2": // NOWY PRODUKT
                                Console.WriteLine("\nKategorie: 1-Napój, 2-Przekąska, 3-Pieczywo");
                                Console.Write("Wybierz typ: ");
                                string type = Console.ReadLine();

                                string specName = "";
                                if (type == "1") specName = "objętość";
                                else if (type == "2") specName = "waga";
                                else specName = "ważność";

                                Console.WriteLine($"Format: [kod] [nazwa] [cena] [ilość] [{specName}]");

                                string inputLine = Console.ReadLine();
                                string[] p = inputLine != null ? inputLine.Split(' ') : null;

                                if (p != null && p.Length >= 5)
                                {
                                    try
                                    {
                                        string code = p[0];
                                        string name = p[1];
                                        decimal pr = decimal.Parse(p[2]);
                                        int q = int.Parse(p[3]);
                                        int spec = int.Parse(p[4]);

                                        Product prod = null;
                                        switch (type)
                                        {
                                            case "1":
                                                prod = new Beverage { Name = name, Price = pr, Volume = spec };
                                                break;
                                            case "2":
                                                prod = new Snack { Name = name, Price = pr, Weight = spec };
                                                break;
                                            case "3":
                                            default:
                                                prod = new Bread { Name = name, Price = pr, ExpirationDays = spec };
                                                break;
                                        }

                                        vendingMachine.AddProduct(code, prod, q);
                                        Console.WriteLine("Nowy produkt dodany.");
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Błąd danych. Upewnij się, że cena i liczby są poprawne.");
                                    }
                                }
                                break;
                            case "3":
                                inAdminMenu = false;
                                break;
                        }
                    }
                    vendingMachine.DisplayProducts();
                    continue;
                }

                // ZWYKŁY WYBÓR PRODUKTU
                vendingMachine.SelectProduct(input);
            }
        }
    }
}