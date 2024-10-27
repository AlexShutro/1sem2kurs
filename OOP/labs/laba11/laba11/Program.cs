using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


public static class Reflector
{
    public static void ExploreClass(string className)
    {
        Type type = Type.GetType(className);
        if (type == null)
        {
            Console.WriteLine("Класс с именем '{0}' не найден.", className);
            return;
        }

        // Определение имени сборки, в которой определен класс
        Assembly assembly = type.Assembly;
        Console.WriteLine("Имя сборки, в которой определен класс '{0}': {1}", className, assembly.FullName);

        // Проверка наличия публичных конструкторов
        ConstructorInfo[] constructors = type.GetConstructors();
        bool hasPublicConstructors = constructors.Any(ctor => ctor.IsPublic);
        Console.WriteLine("Есть ли публичные конструкторы у класса '{0}': {1}", className, hasPublicConstructors);

        // Извлечение всех общедоступных публичных методов
        Console.WriteLine("Общедоступные публичные методы класса '{0}':", className);
        MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
        foreach (MethodInfo method in methods)
        {
            Console.WriteLine(method.Name);
        }

        // Получение информации о полях и свойствах класса
        Console.WriteLine("Поля и свойства класса '{0}':", className);
        IEnumerable<string> fieldsAndProperties = GetFieldsAndProperties(type);
        foreach (string info in fieldsAndProperties)
        {
            Console.WriteLine(info);
        }

        // Получение все реализованных классом интерфейсов
        Console.WriteLine("Интерфейсы, реализованные классом '{0}':", className);
        IEnumerable<string> interfaces = GetImplementedInterfaces(type);
        foreach (string interfaceName in interfaces)
        {
            Console.WriteLine(interfaceName);
        }
    }

    private static IEnumerable<string> GetFieldsAndProperties(Type type)
    {
        var fieldsAndProperties = new List<string>();

        // Поля
        FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        foreach (var field in fields)
        {
            fieldsAndProperties.Add("Field: " + field.Name);
        }

        // Свойства
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        foreach (var property in properties)
        {
            fieldsAndProperties.Add("Property: " + property.Name);
        }

        return fieldsAndProperties;
    }

    private static IEnumerable<string> GetImplementedInterfaces(Type type)
    {
        return type.GetInterfaces().Select(interfaceType => interfaceType.FullName);
    }

    public static void FindMethodsByParameterType(string className, Type parameterType)
    {
        Type type = Type.GetType(className);
        if (type == null)
        {
            Console.WriteLine("Класс с именем '{0}' не найден.", className);
            return;
        }

        Console.WriteLine("Методы класса '{0}', которые содержат параметр типа '{1}':", className, parameterType.Name);
        MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
        foreach (MethodInfo method in methods)
        {
            if (method.GetParameters().Any(p => p.ParameterType == parameterType))
            {
                Console.WriteLine(method.Name);
            }
        }
    }

    public static void InvokeMethod(string className, string methodName, string filePath)
    {
        Type type = Type.GetType(className);
        if (type == null)
        {
            Console.WriteLine("Класс с именем '{0}' не найден.", className);
            return;
        }

        MethodInfo method = type.GetMethod(methodName);
        if (method == null)
        {
            Console.WriteLine("Метод с именем '{0}' не найден в классе '{1}'.", methodName, className);
            return;
        }

        string[] parameterValues = ReadParameterValuesFromFile(filePath);
        object[] parameters = GenerateParameters(method.GetParameters(), parameterValues);

        object instance = Activator.CreateInstance(type);

        try
        {
            method.Invoke(instance, parameters);
            Console.WriteLine("Метод '{0}' класса '{1}' вызван успешно.", methodName, className);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при вызове метода '{0}' класса '{1}': {2}", methodName, className, ex.Message);
        }
    }

    private static string[] ReadParameterValuesFromFile(string filePath)
    {
        try
        {
            return File.ReadAllLines(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при чтении файла '{0}': {1}", filePath, ex.Message);
            return new string[0];
        }
    }

    private static object[] GenerateParameters(ParameterInfo[] parameterInfos, string[] parameterValues)
    {
        if (parameterValues.Length != parameterInfos.Length)
        {
            Console.WriteLine("Количество значений в файле не соответствует количеству параметров метода.");
            return new object[0];
        }

        List<object> parameters = new List<object>();
        for (int i = 0; i < parameterValues.Length; i++)
        {
            ParameterInfo parameterInfo = parameterInfos[i];
            Type parameterType = parameterInfo.ParameterType;
            object value = GenerateValueForType(parameterType, parameterValues[i]);
            parameters.Add(value);
        }

        return parameters.ToArray();
    }

    private static object GenerateValueForType(Type type, string valueAsString)
    {
        if (type == typeof(int))
        {
            int.TryParse(valueAsString, out int intValue);
            return intValue;
        }
        else if (type == typeof(double))
        {
            double.TryParse(valueAsString, out double doubleValue);
            return doubleValue;
        }
        else if (type == typeof(bool))
        {
            bool.TryParse(valueAsString, out bool boolValue);
            return boolValue;
        }
        else if (type == typeof(string))
        {
            return valueAsString;
        }

        return null;
    }

    public static void WriteClassInfoToFile(string className, string filePath)
    {
        Type type = Type.GetType(className);
        if (type == null)
        {
            Console.WriteLine("Класс с именем '{0}' не найден.", className);
            return;
        }

        ClassInfo classInfo = new ClassInfo
        {
            AssemblyName = type.Assembly.FullName,
            HasPublicConstructors = type.GetConstructors().Any(ctor => ctor.IsPublic),
            PublicMethods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(method => method.Name),
            FieldsAndProperties = GetFieldsAndProperties(type),
            ImplementedInterfaces = GetImplementedInterfaces(type)
        };
    }

    private class ClassInfo
    {
        public string AssemblyName { get; set; }
        public bool HasPublicConstructors { get; set; }
        public IEnumerable<string> PublicMethods { get; set; }
        public IEnumerable<string> FieldsAndProperties { get; set; }
        public IEnumerable<string> ImplementedInterfaces { get; set; }
    }
}

class MyClass
{
    public int MyMethod(string name, int age)
    {
        Console.WriteLine("Hello, {0}! You are {1} years old.", name, age);
        return age + 10;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Исследование класса MyClass
        Reflector.ExploreClass("MyClass");

        Console.WriteLine();

        // Поиск методов, содержащих параметр типа string
        Reflector.FindMethodsByParameterType("MyClass", typeof(string));

        Console.WriteLine();

        // Вызов метода класса MyClass
        Reflector.InvokeMethod("MyClass", "MyMethod", "parameters.txt");

        Console.WriteLine();

        // Запись информации о классе MyClass в файл
        Reflector.WriteClassInfoToFile("MyClass", "class_info.json");

        Console.ReadLine();
    }
}