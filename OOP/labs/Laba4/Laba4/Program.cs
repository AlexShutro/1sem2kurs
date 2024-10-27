using System;

Using.Мебель мебель = new Using.Мебель();
мебель.Название = "Стол";
мебель.Цена = 1000;
мебель.ВывестиОписание();
мебель.ПоказатьСтрануПроизводителя();
((Using.IМебель)мебель).ПоказатьКоличествоТоваров();



public class Using
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

    public class Мебель : Товар, IМебель
    {
        public Товар[] Товары { get; set; }

        public override void ВывестиОписание()
        {
            Console.WriteLine($"Мебель: {Название}, Цена: {Цена}");
        }

        public override void ПоказатьСтрануПроизводителя()
        {
            Console.WriteLine("Страна производителя: Испания ");
        }

        public void ПоказатьКоличествоТоваров()
        {
            Console.WriteLine($"Количество товаров: {Товары.Length}");
        }
    }
    public interface IДиван
    {
        Товар[] Товары { get; set; }

        void ПоказатьКоличествоТоваров();
    }

    public class Диван : Мебель, IДиван
    {
        public int Размер { get; set; }

        public override void ВывестиОписание()
        {
            Console.WriteLine($"Кровать: {Название}, Цена: {Цена}, Размер: {Размер}");
        }

        public override void ПоказатьСтрануПроизводителя()
        {
            Console.WriteLine("Страна производителя: Германия");
        }
    }

    public class Шкаф : Мебель
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

    public sealed class ШкафКупе : Шкаф
    {
        public Шкаф[] Шкафы { get; set; }

        public sealed override void ВывестиОписание()
        {
            Console.WriteLine($"Шкаф-купе: {Название}, Цена: {Цена}");
        }

        public override void ПоказатьСтрануПроизводителя()
        {
            Console.WriteLine("Страна производителя: Швеция");
        }
    }

    public class Вешалка : Мебель
    {
        public int КоличествоВешалок { get; set; }

        public override void ВывестиОписание()
        {
            Console.WriteLine($"Вешалка: {Название}, Цена: {Цена}, Количество вешалок: {КоличествоВешалок}");
        }

        public override void ПоказатьСтрануПроизводителя()
        {
            Console.WriteLine("Страна производителя: Китай");
        }
    }

    public class Тумба : Мебель
    {
        public Товар[] Товары { get; set; }

        public override void ВывестиОписание()
        {
            Console.WriteLine($"Тумба: {Название}, Цена: {Цена}");
        }

        public override void ПоказатьСтрануПроизводителя()
        {
            Console.WriteLine("Страна производителя: Россия");
        }

        public void ПоказатьКоличествоТоваров()
        {
            Console.WriteLine($"Количество товаров: {Товары.Length}");
        }
    }

    public class Стул : Мебель
    {
        public int КоличествоНожек { get; set; }

        public override void ВывестиОписание()
        {
            Console.WriteLine($"Стул: {Название}, Цена: {Цена}, Количество ножек: {КоличествоНожек}");
        }

        public override void ПоказатьСтрануПроизводителя()
        {
            Console.WriteLine("Страна производителя: Испания");
        }
    }
}