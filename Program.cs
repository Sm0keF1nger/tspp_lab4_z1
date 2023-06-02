using System;
using System.Collections.Generic;
using System.Linq;

namespace WeatherDatabase
{
    public class Program
    {
        public class Weather
        {
            public int Day { get; set; }
            public int Month { get; set; }
            public double Temperature { get; set; }

            public Weather(int day, int month, double temperature)
            {
                Day = day;
                Month = month;
                Temperature = temperature;
            }

        }
        public static void Main(string[] args)
        {
            List<Weather> weatherData = new List<Weather>();

            while (true)
            {
                Console.WriteLine("Введіть дату ДД:ММ:");
                string input1 = Console.ReadLine();
                string[] parts = input1.Split(':');

                if (parts.Length != 2 || !int.TryParse(parts[0], out int day) || !int.TryParse(parts[1], out int month))
                {
                    Console.WriteLine("Некоректно введені дані");
                    continue;
                }

                if (day < 1 || day > 31 || month < 1 || month > 12)
                {
                    Console.WriteLine("Некоректна дата");
                    continue;
                }

                Console.WriteLine("Введіть температуру:");
                string input2 = Console.ReadLine();

                if (!double.TryParse(input2, out double temperature))
                {
                    Console.WriteLine("Некоректно введені дані");
                    continue;
                }

                Weather weather = new Weather(day, month, temperature);
                weatherData.Add(weather);
                Console.WriteLine("Дані успішно додані до бази даних.");

                Console.WriteLine("Бажаєте ввести ще один день? (+ / -)");
                string input3 = Console.ReadLine();

                if (input3.ToLower() == "-")
                {
                    break;
                }
            }

            Console.WriteLine("Середня температура за кожний місяць:");

            var avgTemperaturesByMonth = weatherData
                .GroupBy(w => w.Month)
                .Select(g => new { Month = g.Key, AvgTemperature = g.Average(w => w.Temperature) });

            foreach (var avgTempByMonth in avgTemperaturesByMonth)
            {
                Console.WriteLine($"Місяць: {avgTempByMonth.Month}, Середня температура: {avgTempByMonth.AvgTemperature}");
            }

            var monthWithHighestAvgTemperature = avgTemperaturesByMonth
                .OrderByDescending(g => g.AvgTemperature)
                .First();

            Console.WriteLine($"Місяць з найвищою середньою температурою: {monthWithHighestAvgTemperature.Month}");
        }
    }
}
