using System;

namespace RpgSimulator
{
    // 1. Abstrakcyjna klasa bazowa
    public abstract class Character
    {
        protected static readonly Random rnd = new Random();

        public string Name { get; set; }

        public int MaxHealthPoints { get; set; }
        public int AttackPower { get; set; }

        // Właściwość z publicznym pobieraniem (get) i PRYWATNYM zapisem (set)
        // Dzięki temu 'BattleArena' może wyświetlić HP, ale nie może go zmienić.
        public int HealthPoints { get; private set; }

        protected Character(string name, int maxHealth, int attack)
        {
            Name = name;
            MaxHealthPoints = maxHealth;
            AttackPower = attack;
            // Ustawiamy HP tylko raz, przy tworzeniu postaci
            HealthPoints = maxHealth;
        }

        // JEDYNA dopuszczalna metoda zmiany HP zgodnie z wymaganiami
        public void TakeDamage(int damage)
        {
            HealthPoints -= damage;
            if (HealthPoints < 0) HealthPoints = 0;

            // Opcjonalnie: logowanie w konsoli dla testu
            // Console.WriteLine($"{Name} otrzymuje {damage} obrażeń. Pozostało: {HealthPoints} HP.");
        }

        public bool IsAlive() => HealthPoints > 0;

        public abstract void Attack(Character target);
    }
    // 2. Klasa Bohatera
    public class Hero : Character
    {
        public Hero(string name) : base(name, 100, 20) { }

        public override void Attack(Character target)
        {
            int damage = AttackPower;

            if (rnd.Next(1, 6) == 1) // 20% szansy na krytyka
            {
                damage *= 2;
                Console.WriteLine($"[KRYTYK!] {Name} uderza z pełną mocą!");
            }

            Console.WriteLine($"{Name} atakuje i zadaje {damage} obrażeń postaci {target.Name}!");
            target.TakeDamage(damage);
        }
    }

    // 3. Baza dla potworów
    public abstract class Monster : Character
    {
        protected Monster(string name, int hp, int attack) : base(name, hp, attack) { }
    }

    // 4. Konkretne potwory
    public class Goblin : Monster
    {
        public Goblin() : base("Goblin Złodziej", 50, 8) { }

        public override void Attack(Character target)
        {
            Console.WriteLine($"{Name} atakuje szybko i zadaje {AttackPower} obrażeń!");
            target.TakeDamage(AttackPower);

            if (new Random().Next(1, 3) == 1) // 50% szansy na drugi cios
            {
                Console.WriteLine($"[COMBO!] {Name} poprawia szybkim ciosem za {AttackPower / 2} obrażeń!");
                target.TakeDamage(AttackPower / 2);
            }
        }
    }

    public class Orc : Monster
    {
        public Orc() : base("Wściekły Ork", 120, 25) { }

        public override void Attack(Character target)
        {
            if (new Random().Next(1, 5) == 1) // 25% szansy na pudło
            {
                Console.WriteLine($"[PUDŁO!] {Name} zamachnął się potężnie, ale nie trafił!");
            }
            else
            {
                Console.WriteLine($"{Name} miażdży ciosem i zadaje {AttackPower} obrażeń!");
                target.TakeDamage(AttackPower);
            }
        }
    }

    // 5. Arena Walki
    public class BattleArena
    {
        private Character Player;
        private Character Opponent;

        public BattleArena(Character player, Character opponent)
        {
            Player = player;
            Opponent = opponent;
        }

        public void StartBattle()
        {
            Console.WriteLine("\n=== ROZPOCZYNA SIĘ WALKA! ===");
            Console.WriteLine($"{Player.Name} ({Player.HealthPoints} HP) VS {Opponent.Name} ({Opponent.HealthPoints} HP)\n");
            Console.WriteLine("Naciśnij dowolny klawisz, aby rozpocząć pierwszą turę...");
            Console.ReadKey(true); // true sprawia, że naciśnięty klawisz nie wyświetla się w konsoli

            int turn = 1;

            while (Player.IsAlive() && Opponent.IsAlive())
            {
                Console.Clear(); // Opcjonalnie: czyści ekran, by każda tura była osobno
                Console.WriteLine($"--- TURA {turn} ---");
                Console.WriteLine($"{Player.Name}: {Player.HealthPoints} HP | {Opponent.Name}: {Opponent.HealthPoints} HP\n");

                // Atak Gracza
                Player.Attack(Opponent);

                if (!Opponent.IsAlive())
                {
                    Console.WriteLine($"\n{Opponent.Name} pada na ziemię!");
                    break;
                }

                Console.WriteLine("\n--- Ruch przeciwnika ---");
                // Atak Przeciwnika
                Opponent.Attack(Player);

                if (!Player.IsAlive())
                {
                    Console.WriteLine($"\n{Player.Name} traci przytomność!");
                }

                turn++;

                // KLUCZOWY MOMENT: Czekanie na gracza
                Console.WriteLine("\nNaciśnij dowolny klawisz, aby przejść dalej...");
                Console.ReadKey(true);
            }

            // Podsumowanie bitwy
            Console.WriteLine("\n=== KONIEC WALKI ===");
            if (Player.IsAlive())
                Console.WriteLine($"Zwycięża {Player.Name}! Gratulacje!");
            else
                Console.WriteLine($"Niestety, {Player.Name} poległ w walce... Wygrywa {Opponent.Name}.");
        }
    }

    // 6. Główna część programu (Main)
    class Program
    {
        private static readonly Random _gameRnd = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine("=== WITAJ W RPG BATTLE SIMULATOR ===");
            Console.Write("Podaj imię swojego bohatera: ");
            string name = Console.ReadLine();

            // Tworzenie bohatera
            Hero player = new Hero(string.IsNullOrWhiteSpace(name) ? "Arthur" : name);

            // Losowanie potwora
            Monster opponent;
            if (_gameRnd.Next(1, 3) == 1)
                opponent = new Goblin();
            else
                opponent = new Orc();

            Console.WriteLine($"Twoim przeciwnikiem jest {opponent.Name}!");

            // Rozpoczęcie walki
            BattleArena arena = new BattleArena(player, opponent);
            arena.StartBattle();

            Console.WriteLine("\nNaciśnij dowolny klawisz, aby zakończyć...");
            Console.ReadKey();
        }
    }
}