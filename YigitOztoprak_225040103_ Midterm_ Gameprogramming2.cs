using System;
using System.Threading.Tasks;

// Base class representing a medieval character
class MedievalCharacter
{
    public string Name { get; set; }
    public int Health { get; set; }

    // Constructor to initialize name and health
    public MedievalCharacter(string name, int health)
    {
        Name = name;
        Health = health;
    }

    // Method to display character information
    public void DisplayInfo()
    {
        Console.WriteLine("[MEDIEVAL CHARACTER] " + Name + ", Health: " + Health);
    }
}

// Subclass representing the knight character
class Knight : MedievalCharacter
{
    private const int MAX_HEALTH = 100;

    public Knight(string name, int health) : base(name, health)
    {
    }

    // Method to attack the dragon
    public async Task AttackDragon(Dragon dragon)
    {
        Console.WriteLine("[BATTLE] " + Name + " charges towards the mighty " + dragon.Name + "...");
        await Task.Delay(2000); // Simulate some delay for the battle
        Random rand = new Random();
        int damage = rand.Next(20, 30); // Random damage between 20 and 30
        dragon.Health -= damage;
        Console.WriteLine("[BATTLE] " + Name + " dealt " + damage + " damage to " + dragon.Name + "!");
    }

    // Method to heal the knight
    public void Heal()
    {
        int healAmount = 20; // Healing amount
        Health = Math.Min(Health + healAmount, MAX_HEALTH); // Ensure health does not exceed max health
        Console.WriteLine("[HEAL] " + Name + " healed for " + healAmount + " health points.");
    }
}

// Subclass representing a dragon enemy
class Dragon : MedievalCharacter
{
    public Dragon(string name, int health) : base(name, health)
    {
    }

    // Method for the dragon to attack the knight
    public async Task AttackKnight(Knight knight)
    {
        Console.WriteLine("[BATTLE] " + Name + " unleashes a fiery breath towards " + knight.Name + "...");
        await Task.Delay(2000); // Simulate some delay for the attack
        Random rand = new Random();
        int damage = rand.Next(15, 25); // Random damage between 15 and 25
        knight.Health -= damage;
        Console.WriteLine("[BATTLE] " + Name + " dealt " + damage + " damage to " + knight.Name + "!");
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        // Create knight and dragon objects
        Knight knight = new Knight("Sir Galahad", 100);
        Dragon dragon = new Dragon("Firebreath", 105);

        // Display initial information
        knight.DisplayInfo();
        dragon.DisplayInfo();

        // Battle loop
        while (knight.Health > 0 && dragon.Health > 0)
        {
            // Display battle options
            Console.WriteLine("1. Attack the dragon");
            Console.WriteLine("2. Defend against the dragon's attack");
            Console.WriteLine("3. Heal");

            // Get player's choice
            Console.Write("Choose your action: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // Knight attacks the dragon
                    await knight.AttackDragon(dragon);
                    break;
                case "2":
                    // Dragon attacks the knight
                    await dragon.AttackKnight(knight);
                    break;
                case "3":
                    // Knight heals
                    knight.Heal();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            // Check if the dragon is defeated after the knight's attack
            if (dragon.Health <= 0)
            {
                break;
            }

            // Dragon attacks the knight
            await dragon.AttackKnight(knight);

            // Check if the knight is defeated after the dragon's attack
            if (knight.Health <= 0)
            {
                break;
            }

            // Display updated information
            knight.DisplayInfo();
            dragon.DisplayInfo();
            Console.WriteLine();
        }

        // Display the outcome of the battle
        if (knight.Health <= 0)
        {
            Console.WriteLine("You have been defeated by the dragon!");
        }
        else
        {
            Console.WriteLine("You have slain the dragon!");
        }
    }
}
