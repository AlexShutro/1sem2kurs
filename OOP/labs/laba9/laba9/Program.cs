using System;
using System.Collections;
using System.Collections.Generic;

public class GeometricFigure
{
    public string Type { get; set; }

    public GeometricFigure(string type)
    {
        Type = type;
    }
}

public class GeometricFigureCollection : IEnumerable<GeometricFigure>
{
    private Stack<GeometricFigure> figures;

    public GeometricFigureCollection()
    {
        figures = new Stack<GeometricFigure>();
    }

    public void AddFigure(GeometricFigure figure)
    {
        figures.Push(figure);
    }

    public bool RemoveFigure(GeometricFigure figure)
    {
        if (figures.Contains(figure))
        {
            Stack<GeometricFigure> tempStack = new Stack<GeometricFigure>();
            while (figures.Peek() != figure)
            {
                tempStack.Push(figures.Pop());
            }
            figures.Pop();
            while (tempStack.Count > 0)
            {
                figures.Push(tempStack.Pop());
            }
            return true;
        }
        return false;
    }

    public void DisplayFigures()
    {
        Console.WriteLine("Гееометрические фигуры:");
        foreach (GeometricFigure figure in figures)
        {
            Console.WriteLine(figure.Type);
        }
    }

    public IEnumerator<GeometricFigure> GetEnumerator()
    {
        return figures.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return figures.GetEnumerator();
    }
}

public class Program
{
    public static void Main()
    {
        GeometricFigureCollection collection = new GeometricFigureCollection();

        GeometricFigure figure1 = new GeometricFigure("Круг");
        GeometricFigure figure2 = new GeometricFigure("Площадь");
        GeometricFigure figure3 = new GeometricFigure("Треугольник");

        collection.AddFigure(figure1);
        collection.AddFigure(figure2);
        collection.AddFigure(figure3);

        collection.DisplayFigures();

        collection.RemoveFigure(figure2);

        collection.DisplayFigures();

        // Использование интерфейса IEnumerator
        Console.WriteLine("Использование IEnumerator:");
        IEnumerator<GeometricFigure> enumerator = collection.GetEnumerator();
        while (enumerator.MoveNext())
        {
            Console.WriteLine(enumerator.Current.Type);
        }
    }
}