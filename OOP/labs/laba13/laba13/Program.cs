using System.Runtime.Serialization.Formatters.Binary;

public interface ISerializer
{
    void Serialize(object obj, string fileName);
    object Deserialize(string fileName);
}

public class BinarySerializer : ISerializer
{
    public void Serialize(object obj, string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fs = new FileStream(fileName, FileMode.Create))
        {
            formatter.Serialize(fs, obj);
        }
    }

    public object Deserialize(string fileName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream fs = new FileStream(fileName, FileMode.Open))
        {
            return formatter.Deserialize(fs);
        }
    }
}

public class SoapSerializer : ISerializer
{
    public void Serialize(object obj, string fileName)
    {
        SoapFormatter formatter = new SoapFormatter();
        using (FileStream fs = new FileStream(fileName, FileMode.Create))
        {
            formatter.Serialize(fs, obj);
        }
    }

    public object Deserialize(string fileName)
    {
        SoapFormatter formatter = new SoapFormatter();
        using (FileStream fs = new FileStream(fileName, FileMode.Open))
        {
            return formatter.Deserialize(fs);
        }
    }
}

public class SoapFormatter
{
    public void Serialize(FileStream fs, object o)
    {
        throw new NotImplementedException();
    }

    public object Deserialize(FileStream fs)
    {
        throw new NotImplementedException();
    }
}

public class JsonSerializer : ISerializer
{
    public void Serialize(object obj, string fileName)
    {
        string json = JsonConvert.SerializeObject(obj);
        File.WriteAllText(fileName, json);
    }

    public object Deserialize(string fileName)
    {
        string json = File.ReadAllText(fileName);
        return JsonConvert.DeserializeObject(json);
    }
}

public class JsonConvert
{
    public static string SerializeObject(object o)
    {
        throw new NotImplementedException();
    }

    public static object DeserializeObject(string json)
    {
        throw new NotImplementedException();
    }
}

public class XmlSerializer : ISerializer
{
    public XmlSerializer(Type getType)
    {
        throw new NotImplementedException();
    }

    public XmlSerializer()
    {
        throw new NotImplementedException();
    }

    public void Serialize(object obj, string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(obj.GetType());
        using (TextWriter tw = new StreamWriter(fileName))
        {
            serializer.Serialize(tw, obj);
        }
    }

    private void Serialize(TextWriter tw, object fileName)
    {
        throw new NotImplementedException();
    }

    public object Deserialize(string fileName)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(object));
        using (TextReader tr = new StreamReader(fileName))
        {
            return serializer.Deserialize(tr);
        }
    }

    private object Deserialize(TextReader fileName)
    {
        throw new NotImplementedException();
    }
}

public class MyClass
{
    public string Name { get; set; }
    [NonSerialized]
    public int Age;
}

public class CustomSerializer
{
    private Dictionary<string, ISerializer> serializers;

    public CustomSerializer()
    {
        serializers = new Dictionary<string, ISerializer>();
        serializers.Add("Binary", new BinarySerializer());
        serializers.Add("SOAP", new SoapSerializer());
        serializers.Add("JSON", new JsonSerializer());
        serializers.Add("XML", new XmlSerializer());
    }

    public void Serialize(object obj, string format, string fileName)
    {
        if (serializers.ContainsKey(format))
        {
            ISerializer serializer = serializers[format];
            serializer.Serialize(obj, fileName);
        }
        else
        {
            Console.WriteLine("Unsupported format!");
        }
    }

    public object Deserialize(string format, string fileName)
    {
        if (serializers.ContainsKey(format))
        {
            ISerializer serializer = serializers[format];
            return serializer.Deserialize(fileName);
        }
        else
        {
            Console.WriteLine("Unsupported format!");
            return null;
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        MyClass obj = new MyClass()
        {
            Name = "John",
            Age = 25
        };

        CustomSerializer serializer = new CustomSerializer();
        string fileName = "data";

        // Сериализация объекта с использованием различных форматов
        serializer.Serialize(obj, "Binary", fileName + ".bin");
        serializer.Serialize(obj, "SOAP", fileName + ".xml");
        serializer.Serialize(obj, "JSON", fileName + ".json");
        serializer.Serialize(obj, "XML", fileName + ".xml");

        // Десериализация объекта из различных форматов
        object deserializedObj = serializer.Deserialize("Binary", fileName + ".bin");
        deserializedObj = serializer.Deserialize("SOAP", fileName + ".xml");
        deserializedObj = serializer.Deserialize("JSON", fileName + ".json");
        deserializedObj = serializer.Deserialize("XML", fileName + ".xml");

        // Демонстрация отсутствия запрещенного элемента в результате работы сериализаторов
        MyClass deserializedMyClass = deserializedObj as MyClass;
        Console.WriteLine(deserializedMyClass.Age); // Output: 0 (по умолчанию для int)
    }
}