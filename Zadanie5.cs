using System;

public interface IOldLogger
{
    void LogMessage(string message);
}

public class NewThirdPartyLogger
{
    public void LogEvent(string eventDetails)
    {
        Console.WriteLine($"[ThirdPartyLogger] Rejestracja zdarzenia: {eventDetails}");
    }
}

public class LoggerAdapter : IOldLogger
{
    private readonly NewThirdPartyLogger _newLogger;

    public LoggerAdapter(NewThirdPartyLogger newLogger)
    {
        _newLogger = newLogger;
    }

    public void LogMessage(string message)
    {
        _newLogger.LogEvent(message);
    }
}

public class AppService
{
    private readonly IOldLogger _logger;

    public AppService(IOldLogger logger)
    {
        _logger = logger;
    }

    public void ProcessData()
    {
        Console.WriteLine("Przetwarzanie danych w systemie...");
        _logger.LogMessage("Zakonczono przetwarzanie danych.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        NewThirdPartyLogger thirdPartyLogger = new NewThirdPartyLogger();
        
        IOldLogger loggerAdapter = new LoggerAdapter(thirdPartyLogger);

        AppService service = new AppService(loggerAdapter);
        
        service.ProcessData();
    }
}
