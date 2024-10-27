using System;
using System.Collections.Generic;

public class MyCollection<T> : List<T>
{
    public bool Add(T element)
    {
        try
        {
            this.Add(element);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при добавлении элемента: " + ex.Message);
            return false;
        }
    }
}

partial class Program
{
    static void Main(string[] args)
    {
        MyCollection<int> collection = new MyCollection<int>();

        collection.Add(1);
        collection.Add(2);
        collection.Add(3);
        collection.Add(4); // Невозможность преобразования

        Console.WriteLine("Элементы в коллекции:");

        foreach (int item in collection)
        {
            Console.WriteLine(item);
        }
    }
}

partial class Program
{
    static void Main(string[] args)
    {
        Card card = new Card("1234567890123456", 12, 2023, 1000);

        Console.WriteLine("Информация о карте:");
        Console.WriteLine("Номер карты: " + card.Number);
        Console.WriteLine("Срок действия: {0}/{1}", card.Month, card.Year);
        Console.WriteLine("Баланс: " + card.Balance);

        Console.WriteLine("--- Пополнение баланса ---");
        card += 500; // Использование оператора +
        Console.WriteLine("Баланс после пополнения: " + card.Balance);

        Console.WriteLine("--- Снятие средств ---");
        card -= 200; // Использование оператора -
        Console.WriteLine("Баланс после снятия: " + card.Balance);
    }
}