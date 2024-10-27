using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication
{
    class TaskManager
    {
        Dictionary<string, Action<string[]>> tasks = new Dictionary<string, Action<string[]>>()
        {
            { "1",
                (string[] args) =>
                {
                    //Упковка
                    Object[] typesExemple = {
                        new Boolean(),
                        new Byte(),
                        new SByte(),
                        new Char(),
                        new Decimal(),
                        new Double(),
                        new Single(),
                        new Int32(),
                        new UInt32(),
                        new IntPtr(),
                        new UIntPtr(),
                        new Int64(),
                        new UInt64(),
                        new Int16(),
                        new UInt16()
                    };

                    int maxLenght = 0;

                    foreach (Object element in typesExemple)
                    {
                        int lenght = element.GetType().Name.Length;
                        if (maxLenght < lenght)
                        {
                            maxLenght = lenght;
                        }
                    }

                    foreach (var value in typesExemple)
                    {
                        Console.Write("type: " + value.GetType());
                        for (int i = 0; i < maxLenght - value.GetType().Name.Length; i++)
                            Console.Write(" ");
                        Console.WriteLine("   value: " + value);
                    }

                    Console.WriteLine("\nЯвное приведение");
                    Int32 bufferInt32 = (Int32)typesExemple[7];
                    Int64 bufferInt64 = (Int64)bufferInt32;
                    Console.WriteLine(bufferInt64);
                    Console.WriteLine("\nНеявное приведение");
                    bufferInt64 = bufferInt32;
                    Console.WriteLine(bufferInt64);
                    Console.WriteLine("Convert демонстрация ToInt64");
                    Console.WriteLine(Convert.ToInt64(bufferInt32));

                    Console.WriteLine("\nРаспоковка " + typesExemple[0].GetType().Name);
                    Console.WriteLine((Boolean)typesExemple[0]);

                    Console.WriteLine("\nNullable<Boolean> демонстрация");
                    Boolean? boolNull = new Nullable<Boolean>();
                    Console.WriteLine("Значение: " + boolNull);
                    boolNull = true;
                    Console.WriteLine("Значение: " + boolNull);

                    try
                    {
                        var bVar = true;
#if FOR_ERROR
                        bVar = 10;
#endif
                        Console.WriteLine(bVar);
                    }
                    catch
                    {
                        Console.WriteLine("Var автоматически определяет тип, при присвоении ему значения другого типа возникает неопределённость");
                    }
                }
            },
            { "2",
                (string[] args) =>
                {
                    Console.WriteLine("Сравнение строковых литералов 123 == 1234 => " + ("123" == "1234"));

                    String str1 = "Задание ";
                    String str2 = "№2";
                    String str3 = "";

                    Console.WriteLine("Сцепление и копирование");
                    str3 = str1 + str2;
                    char[] buffer = str3.ToArray();
                    str2.CopyTo(0, buffer, 0, Math.Min(str2.Length, buffer.Length));
                    Console.WriteLine(buffer);
                    Console.WriteLine("Выделение подстроки");
                    Console.WriteLine(str3.Substring(3));
                    Console.WriteLine("Разделение строки на слова");
                    foreach (String word in str3.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        Console.Write(word + " | ");
                    }
                    Console.Write('\n');
                    Console.WriteLine("Вставки подстроки в заданную позицию");
                    Console.WriteLine(str3.Insert(2, str1));
                    Console.WriteLine("Удаление заданной подстроки");
                    Console.WriteLine(str3.Remove(2, 3));

                    var date = new DateTime(2023, 9, 25);
                    Console.WriteLine($"На {date:dddd, MMMM dd, yyyy}");

                    //null строка и пустая строка были использованы в алгоритме вызова задания

                    var sb = new StringBuilder("Привет мир");
                    sb.Append("!");
                    sb.Insert(7, "компьютерный ");
                    Console.WriteLine(sb);  // Привет компьютерный мир!
                     
                    sb.Replace("мир", "world");
                    Console.WriteLine(sb);  // Привет компьютерный world!
                     
                    sb.Remove(7, 13);
                    Console.WriteLine(sb);  // Привет world!
                     
                    string text = sb.ToString();
                    Console.WriteLine(text);    // Привет world!
                }
            },
            { "3",
                (string[] args) =>
                {
                    bool[][] matrix = new bool[5][];
                    for (int i = 0; i < matrix.Length; i++)
                    {
                        matrix[i] = new bool[5];
                        for (int j = 0;  j < matrix[i].Length; j++)
                        {
                            matrix[i][j] = Random.Shared.Next(2) > 0;
                        }
                    }

                    Console.WriteLine("+-+-+-+-+-+");
                    for (int y = 0; y < matrix.Length; y++)
                    {
                        Console.Write("|");
                        for (int x = 0; x < matrix[y].Length; x++)
                        {
                            Console.Write(matrix[y][x] ? '1' : '0');
                            Console.Write("|");
                        }
                        Console.WriteLine("\n+-+-+-+-+-+");
                    }

                    string[] words =
                    {
                        "Тут",
                        "не",
                        "на",
                        "что",
                        "смотреть"
                    };

                    foreach (string word in words)
                    {
                        Console.Write(word + " ");
                    }
                    Console.WriteLine("\nКоличество слов " + words.Length);
                    Console.WriteLine("Выберите индекс слова которое необходимо поменять");
                    int indexWord = 0;
                    do
                    {
                        indexWord = Convert.ToInt32(Console.ReadLine());
                        if (indexWord >= 0 && indexWord < words.Length)
                            break;
                        Console.WriteLine("Неверный индекс");
                    } while (true);

                    Console.WriteLine("Слово для замены");
                    string swepWord = "";
                    do
                    {
                        swepWord = Console.ReadLine();
                        if (swepWord != null)
                            break;
                        Console.WriteLine("Невернон слово");
                    } while (true);

                    words[indexWord] = swepWord;
                    Console.WriteLine("Слово заменено");
                    foreach (string word in words)
                    {
                        Console.Write(word + " ");
                    }

                    int[][] array = new int[3][];
                    array[0] = new int[2];
                    array[1] = new int[3];
                    array[2] = new int[4];

                    Console.WriteLine("\n+-+-+-+");
                    for (int y = 0; y < 4; y++)
                    {
                        Console.Write("|");
                        for (int x = 0;  x < 3; x++)
                        {
                            if (y < array[x].Length)
                            {
                                array[x][y] = Random.Shared.Next(2);
                                Console.Write(array[x][y] + "|");
                            } else
                            {
                                Console.Write(" |");
                            }
                        }
                        Console.WriteLine("\n+-+-+-+");
                    }

                    var arr = new object[0];
                    var str = "";
                }
            },
            { "4",
                (string[] args) =>
                {
                    (int, string, char, string, ulong) person = (0, "qwerty", 'w', "uiop", 12);
                    Console.WriteLine(person);
                    Console.WriteLine(person.Item2);

                    var (index, name, latter, title, lng) = person;
                    var copyPerson = person;
                    Console.WriteLine("Результат сравнения двух кортежей => " + (copyPerson == person));
                }
            },
            { "6",
                (string[] args) =>
                {
                    int Multiply(int a, int b) => a * b;

                    int factor = 2;

                    try
                    {
                        checked
                        {
                            Console.WriteLine(Multiply(factor, int.MaxValue));  // output: -2
                        }
                    }
                    catch (OverflowException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    try
                    {
                        checked
                        {
                            Console.WriteLine(Multiply(factor, factor * int.MaxValue));
                        }
                    }
                    catch (OverflowException e)
                    {
                        Console.WriteLine(e.Message);  // output: Arithmetic operation resulted in an overflow.
                    }
                }
            }
        };

        public void Start(string[] args)
        {
            string nameTask = "";

            do
            {
                Console.WriteLine("Введите номер задания из списка");
                foreach (var arg in tasks)
                {
                    Console.WriteLine(arg.Key);
                }

                Console.Write("Задание: ");
                nameTask = Console.ReadLine();
                Console.WriteLine("\n========================================\n");

                if (String.IsNullOrEmpty(nameTask) == false && tasks.ContainsKey(nameTask))
                {
                    tasks[nameTask](args);
                }
                else
                {
                    Console.Write("Не");
                }
                Console.WriteLine("\n========================================\n");
            } while (nameTask != "exit");
        }
    }

    class MClass
    {
        static void Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();
            taskManager.Start(args);
        }
    }
}
public class Base { }