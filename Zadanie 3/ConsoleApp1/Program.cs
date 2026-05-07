using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachineApp
{
    // --- MODELE DANYCH ---

    public abstract class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Beverage : Product { public int Volume { get; set; } }
    public class Snack : Product { public int Weight { get; set; } }
    public class Bread : Product { public int ExpirationDays { get; set; } }

    public class ProductSlot
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    // --- LOGIKA AUTOMATU ---

    public class VendingMachine
    {
        // PRYWATNE pola - enkapsulacja stanu automatu
        private readonly Dictionary<string, ProductSlot> _products = new Dictionary<string, ProductSlot>();

        // Publiczny getter, prywatny setter - nikt z zewnątrz nie zmieni kredytu ręcznie
        public decimal CurrentCredit { get; private set; }

        public void InsertCoin(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine(">>> Błąd: Kwota musi być większa niż zero.");
                return;
            }
            CurrentCredit += amount; // Poprawne dodawanie zamiast nadpisywania
        }

        public void AddOrUpdateProduct(string code, Product product, int quantity)
        {
            _products[code] = new ProductSlot { Product = product, Quantity = quantity };
        }

        public void Restock(string code, int quantity)
        {
            if (_products.ContainsKey(code))
            {
                _products[code].Quantity += quantity;
            }
        }

        public void DisplayProducts()
        {
            Console.WriteLine("\n======= AUTOMAT Z PRZEKĄSKAMI =======");
            if (!_products.Any())
            {
                Console.WriteLine("Maszyna jest obecnie pusta.");
            }

            foreach (var item in _products)
            {
                var slot = item.Value;
                string status = slot.Quantity > 0
                    ? $"{slot.Product.Price:F2} PLN ({slot.Quantity} szt.)"
                    : "WYPRZEDANY";

                Console.WriteLine($"[{item.Key}] {slot.Product.Name.PadRight(15)} | {status}");
            }
            Console.WriteLine("=====================================");
            Console.WriteLine($"Dostępne środki: {CurrentCredit:F2} PLN\n");
        }

        public void SelectProduct(string code)
        {
            if (!_products.TryGetValue(code, out ProductSlot slot))
            {
                Console.WriteLine(">>> Błąd: Niepoprawny kod produktu.");
                return;
            }

            if (slot.Quantity <= 0)
            {
                Console.WriteLine(">>> Błąd: Produkt wyprzedany.");
                return;
            }

            if (CurrentCredit < slot.Product.Price)
            {
                Console.WriteLine($">>> Błąd: Niewystarczające środki. Brakuje {(slot.Product.Price - CurrentCredit):F2} PLN");
                return;
            }

            // Realizacja zakupu
            slot.Quantity--;
            CurrentCredit -= slot.Product.Price;
            Console.WriteLine($"\n[WYDANO]: {slot.Product.Name}");
            Console.WriteLine($"Pozostały kredyt: {CurrentCredit:F2} PLN");
        }

        public void ReturnChange()
        {
            if (CurrentCredit <= 0)
            {
                Console.WriteLine("Brak reszty do wydania.");
                return;
            }

            Console.WriteLine($"\n--- WYDAWANIE RESZTY: {CurrentCredit:F2} PLN ---");

            // Algorytm zachłanny wydawania reszty
            decimal[] denominations = { 5.00m, 2.00m, 1.00m, 0.50m, 0.20m, 0.10m, 0.05m, 0.02m, 0.01m };
            decimal remaining = CurrentCredit;

            foreach (decimal coin in denominations)
            {
                int count = (int)(remaining / coin);
                if (count > 0)
                {
                    Console.WriteLine($"Wydano {count} x {coin:F2} PLN");
                    remaining %= coin;
                    // Zaokrąglenie błędu precyzji zmiennoprzecinkowej
                    remaining = Math.Round(remaining, 2);
                }
            }

            CurrentCredit = 0;
            Console.WriteLine("------------------------------------\n");
        }
    }

    // --- PROGRAM GŁÓWNY ---

    internal class Program
    {
        static void Main(string[] args)
        {
            VendingMachine machine = new VendingMachine();

            // Dane startowe
            machine.AddOrUpdateProduct("A1", new Beverage { Name = "Coca-Cola", Price = 5.50m, Volume = 500 }, 5);
            machine.AddOrUpdateProduct("A2", new Snack { Name = "Lay's", Price = 7.00m, Weight = 150 }, 3);
            machine.AddOrUpdateProduct("A3", new Bread { Name = "Rogalik", Price = 3.50m, ExpirationDays = 2 }, 4);

            while (true)
            {
                machine.DisplayProducts();
                Console.WriteLine("Opcje: [kwota] - wrzuć monetę | [kod] - zakup | 'exit' - wyjście i reszta | 'admin' - panel");
                Console.Write("Wybór: ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input)) continue;

                if (input.ToLower() == "exit")
                {
                    machine.ReturnChange();
                    break;
                }

                if (input.ToLower() == "admin")
                {
                    RunAdminMenu(machine);
                    continue;
                }

                // Próba parsowania kwoty (doładowanie)
                if (decimal.TryParse(input, out decimal coin))
                {
                    machine.InsertCoin(coin);
                }
                else // Jeśli to nie liczba, traktujemy jak kod produktu
                {
                    machine.SelectProduct(input.ToUpper());
                }
            }
        }

        static void RunAdminMenu(VendingMachine machine)
        {
            Console.WriteLine("\n--- PANEL ADMINISTRATORA (Kod: 0000) ---");
            Console.Write("Podaj kod dostępu: ");
            if (Console.ReadLine() != "0000") return;

            Console.WriteLine("1. Uzupełnij zapas");
            Console.WriteLine("2. Dodaj nowy produkt");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Podaj kod produktu i ilość (np. A1 10): ");
                string[] parts = Console.ReadLine()?.Split(' ');
                if (parts?.Length == 2 && int.TryParse(parts[1], out int qty))
                {
                    machine.Restock(parts[0].ToUpper(), qty);
                    Console.WriteLine("Uzupełniono.");
                }
            }
            else if (choice == "2")
            {
                // Tutaj uproszczona logika dodawania
                Console.WriteLine("Format: [kod] [nazwa] [cena] [ilość]");
                string[] p = Console.ReadLine()?.Split(' ');
                if (p?.Length == 4)
                {
                    machine.AddOrUpdateProduct(p[0].ToUpper(),
                        new Snack { Name = p[1], Price = decimal.Parse(p[2]) },
                        int.Parse(p[3]));
                }
            }
        }
    }
}