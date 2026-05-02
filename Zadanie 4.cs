using System;

public class Square
{
    public double Side { get; set; }

    public Square(double side)
    {
        Side = side;
    }
}

public interface IShape
{
    double GetArea();
}

public class SquareAdapter : IShape
{
    private readonly Square _square;

    public SquareAdapter(Square square)
    {
        _square = square;
    }

    public double GetArea()
    {
        return _square.Side * _square.Side;
    }
}

class Program
{
    static void CalculateAndPrintArea(IShape shape)
    {
        Console.WriteLine($"Obszar ksztaltu wynosi: {shape.GetArea()}");
    }

    static void Main(string[] args)
    {
        Square mySquare = new Square(5.0);
        
        IShape adaptedSquare = new SquareAdapter(mySquare);

        CalculateAndPrintArea(adaptedSquare);
    }
}
