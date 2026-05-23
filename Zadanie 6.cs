using System;

namespace DekoratorKawa
{
    // === 1. COMPONENT ===
    public abstract class Coffee
    {
        public abstract string GetDescription();
        public abstract double GetCost();
    }

    // === 2. CONCRETE COMPONENT ===
    public class Espresso : Coffee
    {
        public override string GetDescription()
        {
            return "Espresso";
        }

        public override double GetCost()
        {
            return 8.00; // cena bazowa w zł
        }
    }

    public class RoastCoffee : Coffee
    {
        public override string GetDescription()
        {
            return "Kawa Rozpuszczalna";
        }

        public override double GetCost()
        {
            return 6.00;
        }
    }

    // === 3. DECORATOR (Abstrakcyjny) ===
    public abstract class CoffeeDecorator : Coffee
    {
        protected Coffee _coffee;

        protected CoffeeDecorator(Coffee coffee)
        {
            _coffee = coffee;
        }

        public override string GetDescription()
        {
            return _coffee.GetDescription();
        }

        public override double GetCost()
        {
            return _coffee.GetCost();
        }
    }

    // === 4. CONCRETE DECORATORS ===
    public class MilkDecorator : CoffeeDecorator
    {
        public MilkDecorator(Coffee coffee) : base(coffee) { }

        public override string GetDescription()
        {
            return _coffee.GetDescription() + ", ze spienionym mlekiem";
        }

        public override double GetCost()
        {
            return _coffee.GetCost() + 2.50; // dopłata za mleko
        }
    }

    public class SugarDecorator : CoffeeDecorator
    {
        public SugarDecorator(Coffee coffee) : base(coffee) { }

        public override string GetDescription()
        {
            return _coffee.GetDescription() + ", z cukrem";
        }

        public override double GetCost()
        {
            return _coffee.GetCost() + 1.00; // dopłata za cukier
        }
    }

    public class VanillaSyrupDecorator : CoffeeDecorator
    {
        public VanillaSyrupDecorator(Coffee coffee) : base(coffee) { }

        public override string GetDescription()
        {
            return _coffee.GetDescription() + ", z syropem waniliowym";
        }

        public override double GetCost()
        {
            return _coffee.GetCost() + 3.50; // dopłata za syrop
        }
    }

    // === 5. CLIENT (Program testowy) ===
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== SYSTEM ZAMAWIANIA KAWY ===");

            // 1. Zamówienie czystego espresso
            Coffee kawa1 = new Espresso();
            Console.WriteLine($"Zamówienie 1: {kawa1.GetDescription()} | Koszt: {kawa1.GetCost():F2} zł");

            // 2. Zamówienie Espresso z mlekiem i cukrem
            Coffee kawa2 = new Espresso();
            kawa2 = new MilkDecorator(kawa2);
            kawa2 = new SugarDecorator(kawa2);
            Console.WriteLine($"Zamówienie 2: {kawa2.GetDescription()} | Koszt: {kawa2.GetCost():F2} zł");

            // 3. Zamówienie kawy rozpuszczalnej ze wszystkim (podwójny cukier)
            Coffee certyfikowanyKlasyk = new RoastCoffee();
            certyfikowanyKlasyk = new MilkDecorator(certyfikowanyKlasyk);
            certyfikowanyKlasyk = new SugarDecorator(certyfikowanyKlasyk);
            certyfikowanyKlasyk = new SugarDecorator(certyfikowanyKlasyk); // podwójny cukier
            certyfikowanyKlasyk = new VanillaSyrupDecorator(certyfikowanyKlasyk);
            
            Console.WriteLine($"Zamówienie 3: {certyfikowanyKlasyk.GetDescription()} | Koszt: {certyfikowanyKlasyk.GetCost():F2} zł");
            
            Console.ReadKey();
        }
    }
}
