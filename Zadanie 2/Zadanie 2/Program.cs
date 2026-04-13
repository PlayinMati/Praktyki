using System;

namespace RpgSimulator
{
    // 1. Abstrakcyjna klasa bazowa
    public abstract class Character
    {
        public string Name { get; set; }
        public int HealthPoints { get; set; }
        public int MaxHealthPoints { get; set; }
        public int AttackPower { get; set; }

        protected Character(string name, int maxHealth, int attack)
        {
            Name = name;
            MaxHealthPoints = maxHealth;
            HealthPoints = maxHealth;
            AttackPower = attack;
        }

        public void TakeDamage(int damage)
        {
            HealthPoints -= damage;
            if (HealthPoints < 0) HealthPoints = 0;
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
            Random rnd = new Random();
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

            int turn = 1;

            // Pętla walki
            while (Player.IsAlive() && Opponent.IsAlive())
            {
                Console.WriteLine($"--- TURA {turn} ---");

                // Atak Gracza
                Player.Attack(Opponent);
                Console.WriteLine($"{Opponent.Name} ma teraz {Opponent.HealthPoints}/{Opponent.MaxHealthPoints} HP.\n");

                // Sprawdzamy czy potwór zginął po naszym ataku
                if (!Opponent.IsAlive()) break;

                // Atak Przeciwnika
                Opponent.Attack(Player);
                Console.WriteLine($"{Player.Name} ma teraz {Player.HealthPoints}/{Player.MaxHealthPoints} HP.\n");

                turn++;
            }

            // Podsumowanie bitwy
            Console.WriteLine("=== KONIEC WALKI ===");
            if (Player.IsAlive())
                Console.WriteLine($"Zwycięża {Player.Name}! Gratulacje!");
            else
                Console.WriteLine($"Niestety, {Player.Name} poległ w walce... Wygrywa {Opponent.Name}.");
        }
    }

    // 6. Główna część programu (Main)
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== WITAJ W RPG BATTLE SIMULATOR ===");
            Console.Write("Podaj imię swojego bohatera: ");
            string name = Console.ReadLine();

            // Tworzenie bohatera
            Hero player = new Hero(string.IsNullOrWhiteSpace(name) ? "Arthur" : name);

            // Losowanie potwora
            Monster opponent;
            if (new Random().Next(1, 3) == 1)
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