using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Zadanie_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Stwotrzenie stoper do mierzenia czasu działania aplikacji
            Stopwatch stoper = new Stopwatch();
            stoper.Start();
            //Tablica stringów do zapisania pliku tekstowego
            string[] wszystkieLinie = null;

            //Try - catch zapisujący plik tekstowy do tablicy
            try
            {
              wszystkieLinie = File.ReadAllLines("D:\\Pobrane D\\logi.txt");
            }
            catch (Exception FileNotFoundException)
            {
                Console.WriteLine("Błąd: Nie znaleziono pliku 'logi.txt'. Upewnij się, że jest w odpowiednim folderze.");
            }

            //Zmienne do zliczania błedów
            int info = 0;
            int warning = 0;
            int error = 0;
            int debug = 0;

            //Lista stringów do zapisywaia errorów
            List<string> errors = new List<string>();

            //Pętla zliczająca wystąpienia
            foreach (string linia in wszystkieLinie)
            {
                if (linia.Contains("INFO"))
                {
                    info++;
                }
                if (linia.Contains("WARNING"))
                {
                    warning++;
                }
                if (linia.Contains("ERROR"))
                {
                    error++;
                    errors.Add(linia);
                }
                if (linia.Contains("DEBUG"))
                {
                    debug++;
                }
            }

            //Wypisywanie wystąpień
            Console.WriteLine("INFO: " + info);
            Console.WriteLine("WARNING: " + warning);
            Console.WriteLine("ERROR: " + error);
            Console.WriteLine("DEBUG: " + debug);

            //Zapiasywanie eroorów do pliku tekstowego
            File.WriteAllLines("error.txt", errors);

            //Zmienna która będzie zapisana do pliku tekstowego
            string tekstRaportu = "Podsumowanie logów:\n" +
                      "INFO: " + info + "\n" +
                      "WARNING: " + warning + "\n" +
                      "ERROR: " + error + "\n" +
                      "DEBUG: " + debug;
            //Zapisywanie zminnej do pliku tekstowgo
            File.WriteAllText("raport.txt", tekstRaportu);
            stoper.Stop();

            //Wyświetlanie czasu programu
            Console.WriteLine($"Dokładny czas: {stoper.Elapsed.TotalSeconds} sekund");
        }
    }
}
