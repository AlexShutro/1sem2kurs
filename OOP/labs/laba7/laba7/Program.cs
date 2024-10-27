using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public interface IGenericCollection<T>
{
    void Add(T item);
    bool Remove(T item);
    void Display();
    void SaveToFile(string filePath); 
    void LoadFromFile(string filePath);
}
public class GenericCollection<T> : IGenericCollection<T>
{
    private List<T> items;

    public GenericCollection()
    {
        items = new List<T>();
    }

    public void Add(T item)
    {
        items.Add(item);
    }

    public bool Remove(T item)
    {
        return items.Remove(item);
    }

    public void Display()
    {
        Console.WriteLine("Содержимое коллекции:");
        foreach (T item in items)
        {
            Console.WriteLine(item);
        }
    }

    public void SaveToFile(string filePath)
    {
        // Выберите формат сохранения:
        // Текстовый файл
        SaveToTextFile(filePath);

        // ИЛИ

        // XML файл
        // SaveToXmlFile(filePath);

        // ИЛИ

        // JSON файл
        // SaveToJsonFile(filePath);
    }

    public void LoadFromFile(string filePath)
    {
        // Выберите формат чтения:
        // Текстовый файл
        LoadFromTextFile(filePath);

        // ИЛИ

        // XML файл
        // LoadFromXmlFile(filePath);

        // ИЛИ

        // JSON файл
        // LoadFromJsonFile(filePath);
    }

    // Методы соответствующие текстовому формату
    private void SaveToTextFile(string filePath)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (T item in items)
                {
                    writer.WriteLine(item);
                }
            }
            Console.WriteLine("Коллекция успешно сохранена в текстовый файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка при сохранении коллекции в текстовый файл: " + ex.Message);
        }
    }

    private void LoadFromTextFile(string filePath)
    {
        try
        {
            items.Clear();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    items.Add((T)Convert.ChangeType(line, typeof(T)));
                }
            }
            Console.WriteLine("Коллекция успешно сохранена в текстовый файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка при сохранении коллекции в текстовый файл: " + ex.Message);
        }
    }

    // Дополнительные методы сохранения и чтения для XML и JSON форматов

    private void SaveToXmlFile(string filePath)
    {
        // Дополнительная логика для сохранения в XML файл
    }

    private void LoadFromXmlFile(string filePath)
    {
        // Дополнительная логика для загрузки из XML файла
    }

    private void SaveToJsonFile(string filePath)
    {
        try
        {
            string json = JsonSerializer.Serialize(items);
            File.WriteAllText(filePath, json);
            Console.WriteLine("Collection saved to JSON file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while saving collection to JSON file: " + ex.Message);
        }
    }

    private void LoadFromJsonFile(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            items = JsonSerializer.Deserialize<List<T>>(json);
            Console.WriteLine("Collection loaded from JSON file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while loading collection from JSON file: " + ex.Message);
        }
    }
}

public class Program
{
    public static void Main()
    {
        IGenericCollection<string> collection = new GenericCollection<string>();

        collection.Add("Item 1");
        collection.Add("Item 2");
        collection.Add("Item 3");

        collection.Display();

        collection.Remove("Item 2");

        collection.Display();

        // Сохранение в файл и загрузка из файла
        string filePath = "collection.txt";
        collection.SaveToFile(filePath);
        collection.LoadFromFile(filePath);
    }
}