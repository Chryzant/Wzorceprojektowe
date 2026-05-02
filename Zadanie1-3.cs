using System;

public class Pizza
{
    public string Crust { get; set; }
    public string Meat { get; set; }
    public string Cheese { get; set; }
    public string Vegetables { get; set; }
    public string Spices { get; set; }

    public void ShowPizza()
    {
        Console.WriteLine("Gotowa Pizza:");
        Console.WriteLine($"- Ciasto: {Crust}");
        Console.WriteLine($"- Wędliny: {Meat}");
        Console.WriteLine($"- Ser: {Cheese}");
        Console.WriteLine($"- Warzywa: {Vegetables}");
        Console.WriteLine($"- Przyprawy: {Spices}\n");
    }
}

public interface IPizzaBuilder
{
    void AddCrust();
    void AddMeat();
    void AddCheese();
    void AddVegetables();
    void AddSpices();
    Pizza GetPizza();
}

public class PeperoniPizzaBuilder : IPizzaBuilder
{
    private Pizza _pizza = new Pizza();

    public void AddCrust()
    {
        _pizza.Crust = "Cienkie wloskie";
    }

    public void AddMeat()
    {
        _pizza.Meat = "Podwojne pepperoni";
    }

    public void AddCheese()
    {
        _pizza.Cheese = "Mozzarella";
    }

    public void AddVegetables()
    {
        _pizza.Vegetables = "Papryczki jalapeño";
    }

    public void AddSpices()
    {
        _pizza.Spices = "Oregano";
    }

    public Pizza GetPizza()
    {
        return _pizza;
    }
}

public class PizzaDirector
{
    private IPizzaBuilder _builder;

    public PizzaDirector(IPizzaBuilder builder)
    {
        _builder = builder;
    }

    public void MakePizza()
    {
        _builder.AddCrust();
        _builder.AddCheese();
        _builder.AddMeat();
        _builder.AddVegetables();
        _builder.AddSpices();
    }
}

class Program
{
    static void Main(string[] args)
    {
        IPizzaBuilder builder = new PeperoniPizzaBuilder();
        PizzaDirector director = new PizzaDirector(builder);

        director.MakePizza();

        Pizza myPizza = builder.GetPizza();
        myPizza.ShowPizza();
    }
}
