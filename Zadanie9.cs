using System;
using System.Collections.Generic;

namespace ObserwatorStacjaPogodowa
{
    public interface IWeatherObserver
    {
        void Update(double temperature, double humidity);
    }

    public class WeatherStation
    {
        private readonly List<IWeatherObserver> _observers = new List<IWeatherObserver>();
        private double _temperature;
        private double _humidity;

        public void Attach(IWeatherObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IWeatherObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_temperature, _humidity);
            }
        }

        public void SetMeasurements(double temperature, double humidity)
        {
            _temperature = temperature;
            _humidity = humidity;
            NotifyObservers();
        }
    }

    public class CurrentConditionsDisplay : IWeatherObserver
    {
        public void Update(double temperature, double humidity)
        {
            Console.WriteLine($"[Wyświetlacz Bieżący] Temperatura: {temperature}°C | Wilgotność: {humidity}%");
        }
    }

    public class ForecastDisplay : IWeatherObserver
    {
        private double _lastTemperature = 20.0;

        public void Update(double temperature, double humidity)
        {
            Console.Write("[Wyświetlacz Prognozy] ");
            if (temperature > _lastTemperature)
            {
                Console.WriteLine("Prognoza: Ciśnienie i temperatura rosną - spodziewaj się poprawy pogody.");
            }
            else if (Math.Abs(temperature - _lastTemperature) < 0.01)
            {
                Console.WriteLine("Prognoza: Stabilne warunki atmosferyczne bez większych zmian.");
            }
            else
            {
                Console.WriteLine("Prognoza: Temperatura spada - możliwy deszcz lub zachmurzenie.");
            }
            _lastTemperature = temperature;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            WeatherStation station = new WeatherStation();

            CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay();
            ForecastDisplay forecastDisplay = new ForecastDisplay();

            station.Attach(currentDisplay);
            station.Attach(forecastDisplay);

            Console.WriteLine("=== POMIAR 1 ===");
            station.SetMeasurements(22.5, 65.0);

            Console.WriteLine("\n=== POMIAR 2 ===");
            station.SetMeasurements(25.4, 60.0);

            Console.WriteLine("\n=== ODŁĄCZENIE WYŚWIETLACZA BIEŻĄCEGO ===");
            station.Detach(currentDisplay);

            Console.WriteLine("\n=== POMIAR 3 ===");
            station.SetMeasurements(19.1, 80.0);

            Console.ReadKey();
        }
    }
}
