using System;

// Пользовательское исключение для недопустимых данных во время инициализации объекта
public class InvalidDataException : Exception
{
    public InvalidDataException(string message) : base(message)
    {
    }
}

public class Person
{
    private string name;
    private int age;

    public Person(string name, int age)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new InvalidDataException("Invalid name");
        }

        if (age <= 0)
        {
            throw new InvalidDataException("Invalid age");
        }

        this.name = name;
        this.age = age;
    }
}

public class Program
{
    public static void Main()
    {
        try
        {
            // Исключение 1: Недопустимые данные во время инициализации объекта
            Person person1 = new Person("", 25);
        }
        catch (InvalidDataException ex)
        {
            Console.WriteLine("Exception 1: " + ex.Message);
        }
        
        int number1 = 10;
        int number2 = 0;
        try
        {
            Console.WriteLine(number1/number2);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine("Exception 2: " + ex.Message);
        }

        try
        {
            // Исключение 3: Индекс выходит за пределы диапазона
            int[] numbers = { 1, 2, 3 };
            int value = numbers[3];
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine("Exception 3: " + ex.Message);
        }

        try
        {
            // Исключение 4: Нулевая ссылка
            string name = null;
            int length = name.Length;
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine("Exception 4: " + ex.Message);
        }

        try
        {
            // Исключение 5: Файл не найден
            string filePath = "nonexistent.txt";
            string fileContent = System.IO.File.ReadAllText(filePath);
        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.WriteLine("Exception 5: " + ex.Message);
        }
        
    }
}


