using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    public class Beverage : Product
    {
        public int Voulume;
    }
    public class Snack : Product
    {
        public int Weight;
    }
    public class ProductSlot
    {
        public Product Product { get; set; }
        public int Quantity {  get; set; } 
    }
    public class VendingMachine
    {
        Dictionary<string, ProductSlot> ProductList { get; set; } =  new Dictionary<string, ProductSlot>();
        public decimal CurrnetCredit { get; set; }
        public void InsertCoin(decimal amount)
        {
            CurrnetCredit = amount;
        }
        public void AddProduct(string code, Product product, int quantity)
        {
            ProductList[code] = new ProductSlot { Product = product, Quantity = quantity };
        }
        public void DisplayProducts()
        {
            Console.WriteLine("Dostępne produkty: ");
            foreach (var pair in ProductList) {
                var slot = pair.Value;
                Console.WriteLine($"[{pair.Key}] {slot.Product.Name} - Cena: {slot.Product.Price} PLN (Sztuk: {slot.Quantity})");
            }
        }
        public void SelectProduct(string productsCode)
        {
            if(ProductList.ContainsKey(productsCode))
            {
                ProductSlot slot = ProductList[productsCode];
                if(slot.Quantity > 0)
                {
                    if(CurrnetCredit > slot.Product.Price)
                    {
                        Console.WriteLine(slot.Product.Name + " został wydany");
                        slot.Quantity--;
                        CurrnetCredit -= slot.Product.Price;
                        Console.WriteLine("Pozostały kredyt: " + CurrnetCredit + " PLN");
                    }
                    else
                    {
                        Console.WriteLine("Brak środków na koncie.");
                    }
                }
                else
                {
                    Console.WriteLine("Produkt wyprzedany.");
                }

            }
            else
            {
                Console.WriteLine("Niepoprawny kod produktu.");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vendingMachine = new VendingMachine();
            Beverage cola = new Beverage
            {
                Name = "Coca-Cola",
                Price = 5.50m,
                Voulume = 500
            };

            Snack chips = new Snack
            {
                Name = "Lay's",
                Price = 7.00m,
                Weight = 150
            };
            vendingMachine.AddProduct("A1", cola, 10);
            vendingMachine.AddProduct("B2", chips, 5);
            vendingMachine.DisplayProducts();

            Console.WriteLine("Wprowadź kwotę doładowania (PLN): ");
            vendingMachine.InsertCoin(decimal.Parse(Console.ReadLine()));

            while (true)
            {
                Console.WriteLine("Wprowadź kod produktu (lub 'exit' aby zakończyć): ");
                string input = Console.ReadLine();
                if (input.ToLower() == "exit")
                    break;
                vendingMachine.SelectProduct(input);
            }

            Console.ReadKey();
        }
    }
}
