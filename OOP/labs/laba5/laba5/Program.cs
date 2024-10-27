using System;

public partial class Program
{
    public static void Main()
    {
        Using.Controller controller = new Using.Controller();
        controller.AddFurniture("Стол", 1000, "Испания");
        controller.AddFurniture("Стул", 2000, "Испания");

        controller.DisplayFurnitureDescriptions();
        controller.DisplayFurnitureCountries();
        controller.DisplayTotalFurnitureCount();
    }
}

public partial class Using
{
    public abstract class Товар
    {
        public string Название { get; set; }
        public decimal Цена { get; set; }

        public virtual void ВывестиОписание()
        {
            Console.WriteLine($"Товар: {Название}, Цена: {Цена}");
        }

        public abstract void ПоказатьСтрануПроизводителя();
    }

    public interface IМебель
    {
        Товар[] Товары { get; set; }

        void ПоказатьКоличествоТоваров();
    }

    public partial class Мебель : Товар, IМебель
    {
        public Товар[] Товары { get; set; }

        public override void ВывестиОписание()
        {
            Console.WriteLine($"Мебель: {Название}, Цена: {Цена}");
        }

        public override void ПоказатьСтрануПроизводителя()
        {
            Console.WriteLine("Страна производителя: Испания");
        }

        public void ПоказатьКоличествоТоваров()
        {
            Console.WriteLine($"Количество товаров: {Товары.Length}");
        }
    }

    public partial class Шкаф : Мебель
    {
        public Товар[] Товары { get; set; }

        public override void ВывестиОписание()
        {
            Console.WriteLine($"Шкаф: {Название}, Цена: {Цена}");
        }

        public override void ПоказатьСтрануПроизводителя()
        {
            Console.WriteLine("Страна производителя: Франция");
        }

        public void ПоказатьКоличествоТоваров()
        {
            Console.WriteLine($"Количество товаров: {Товары.Length}");
        }
    }

    public class Controller
    {
        private Мебель[] container;

        public Controller()
        {
            container = new Мебель[0];
        }

        public void AddFurniture(string name, decimal price, string country)
        {
            Мебель furniture = new Мебель();
            furniture.Название = name;
            furniture.Цена = price;

            Array.Resize(ref container, container.Length + 1);
            container[container.Length - 1] = furniture;
        }

        public void DisplayFurnitureDescriptions()
        {
            foreach (Мебель furniture in container)
            {
                furniture.ВывестиОписание();
            }
        }

        public void DisplayFurnitureCountries()
        {
            foreach (Мебель furniture in container)
            {
                furniture.ПоказатьСтрануПроизводителя();
            }
        }

        public void DisplayTotalFurnitureCount()
        {
            int total = 0;
            foreach (Мебель furniture in container)
            {
                if (furniture is IМебель)
                {
                    total += ((IМебель)furniture).Товары.Length;
                }
            }

            Console.WriteLine($"Total furniture count: {total}");
        }
    }
}