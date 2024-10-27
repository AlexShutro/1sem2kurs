using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        List<Train> trainList = GetTrainData();

        // Задача 1: Вывести список поездов, следующих до заданного пункта назначения
        string destination = "Москва";
        var trainsToDestination = trainList.Where(train => train.Destination == destination);
        Console.WriteLine($"Поезды, следующие до пункта назначения '{destination}':");
        foreach (var train in trainsToDestination)
        {
            Console.WriteLine(train.ToString());
        }

        Console.WriteLine();

        // Задача 2: Вывести список поездов, следующих до заданного пункта назначения и отправляющихся после заданного часа
        int departureHour = 10;
        var trainsToDestinationAfterHour = trainList.Where(train => train.Destination == destination && train.DepartureHour > departureHour);
        Console.WriteLine($"Поезды, следующие до пункта назначения '{destination}' и отправляющиеся после {departureHour} часов:");
        foreach (var train in trainsToDestinationAfterHour)
        {
            Console.WriteLine(train.ToString());
        }

        Console.WriteLine();

        // Задача 3: Найти поезд с максимальным количеством мест
        var trainWithMaxSeats = trainList.OrderByDescending(train => train.SeatsCapacity).FirstOrDefault();
        Console.WriteLine($"Поезд с максимальным количеством мест:\n{trainWithMaxSeats.ToString()}");

        Console.WriteLine();

        // Задача 4: Вывести последние пять поездов по времени отправления
        var lastFiveTrainsByDepartureTime = trainList.OrderByDescending(train => train.DepartureTime).Take(5);
        Console.WriteLine("Последние пять поездов по времени отправления:");
        foreach (var train in lastFiveTrainsByDepartureTime)
        {
            Console.WriteLine(train.ToString());
        }

        Console.WriteLine();

        // Задача 5: Упорядочить список поездов по пункту назначения в алфавитном порядке
        var trainsByDestinationAlphabetically = trainList.OrderBy(train => train.Destination);
        Console.WriteLine("Список поездов, упорядоченный по пункту назначения в алфавитном порядке:");
        foreach (var train in trainsByDestinationAlphabetically)
        {
            Console.WriteLine(train.ToString());
        }
    }

    // Метод для получения данных о поездах (замените или расширьте его собственными данными)
    static List<Train> GetTrainData()
    {
        return new List<Train>
        {
            new Train("Москва", 10, 8),
            new Train("Санкт-Петербург", 9, 6),
            new Train("Москва", 12, 10),
            new Train("Екатеринбург", 11, 7),
            new Train("Москва", 14, 9),
            new Train("Новосибирск", 13, 12),
            new Train("Москва", 15, 11),
            new Train("Казань", 17, 14),
            new Train("Москва", 16, 13),
            new Train("Ростов-на-Дону", 18, 15)
        };
    }

    // Класс, представляющий информацию о поезде
    class Train
    {
        public string Destination { get; }
        public int DepartureTime { get; }
        public int SeatsCapacity { get; }
        public int DepartureHour { get; set; }

        public Train(string destination, int departureTime, int seatsCapacity)
        {
            Destination = destination;
            DepartureTime = departureTime;
            SeatsCapacity = seatsCapacity;
        }

        public override string ToString()
        {
            return $"Пункт назначения: {Destination}, Время отправления: {DepartureTime} часов, Вместимость: {SeatsCapacity} мест";
        }
    }
}