using System;
using System.Collections.Generic;

public class Logger
{
    private static Logger _instance;
    private List<string> _logs;

    private Logger()
    {
        _logs = new List<string>();
    }

    public static Logger GetInstance()
    {
        if (_instance == null)
        {
            _instance = new Logger();
        }
        return _instance;
    }

    public void Log(string message)
    {
        _logs.Add(message);
        Console.WriteLine($"[Rejestracja]: {message}");
    }

    public void PrintLogs()
    {
        foreach (string log in _logs)
        {
            Console.WriteLine(log);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Logger logger1 = Logger.GetInstance();
        logger1.Log("Uruchomienie systemu.");
        logger1.Log("Ladowanie modulu A.");

        Logger logger2 = Logger.GetInstance();
        logger2.Log("Wystapil blad w module A.");

        Console.WriteLine("\n--- Dziennik wszystkich komunikatow ---");
        
        Logger logger3 = Logger.GetInstance();
        logger3.PrintLogs();

        if (logger1 == logger2 && logger2 == logger3)
        {
            Console.WriteLine("\nTest: Wszystkie zmienne wskazuja na te sama instancje.");
        }
    }
}
