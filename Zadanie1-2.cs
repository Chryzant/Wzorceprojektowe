using System;
using System.Collections.Generic;

public class PrintSpooler
{
    private static PrintSpooler _instance;
    private Queue<string> _printJobs;

    private PrintSpooler()
    {
        _printJobs = new Queue<string>();
    }

    public static PrintSpooler GetInstance()
    {
        if (_instance == null)
        {
            _instance = new PrintSpooler();
        }
        return _instance;
    }

    public void AddJob(string job)
    {
        _printJobs.Enqueue(job);
        Console.WriteLine($"[Bufor]: Dodano zadanie '{job}' do kolejki.");
    }

    public void ProcessJobs()
    {
        Console.WriteLine("\n--- Rozpoczecie drukowania ---");
        while (_printJobs.Count > 0)
        {
            string currentJob = _printJobs.Dequeue();
            Console.WriteLine($"[Drukarka]: Drukowanie dokumentu -> {currentJob}");
        }
        Console.WriteLine("--- Zakonczono drukowanie ---\n");
    }
}

class Program
{
    static void Main(string[] args)
    {
        PrintSpooler spooler1 = PrintSpooler.GetInstance();
        spooler1.AddJob("Dokument_finansowy.pdf");
        spooler1.AddJob("Zdjecie_z_wakacji.jpg");

        PrintSpooler spooler2 = PrintSpooler.GetInstance();
        spooler2.AddJob("Raport_roczny.docx");

        PrintSpooler spooler3 = PrintSpooler.GetInstance();
        spooler3.ProcessJobs();

        spooler1.AddJob("Bilet_na_pociag.pdf");
        spooler2.ProcessJobs();

        if (spooler1 == spooler2 && spooler2 == spooler3)
        {
            Console.WriteLine("Test: Bufor wydruku dziala poprawnie - istnieje tylko jedna instancja.");
        }
    }
}
