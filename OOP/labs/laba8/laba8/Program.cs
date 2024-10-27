using System;

public class User
{
    public string Name { get; set; }
    public string Software { get; set; }
    public int Version { get; set; }

    public event EventHandler Upgrade;
    public event EventHandler Work;

    public User(string name, string software)
    {
        Name = name;
        Software = software;
        Version = 1;
    }

    public virtual void OnUpgrade()
    {
        Version++;
        Upgrade?.Invoke(this, EventArgs.Empty);
    }

    public virtual void OnWork()
    {
        Work?.Invoke(this, EventArgs.Empty);
    }
}
public class Program
{
    public static void Main()
    {
        // Создаем объекты Пользователь
        User user1 = new User("User 1", "Software A");
        User user2 = new User("User 2", "Software B");
        User user3 = new User("User 3", "Software C");

        // Подписываем объекты на события
        user1.Upgrade += User_Upgrade;
        user1.Work += User_Work;

        user2.Upgrade += User_Upgrade;
        user2.Work += User_Work;

        user3.Upgrade += User_Upgrade;
        user3.Work += User_Work;

        // Имитация наступления событий
        user1.OnUpgrade(); // Первый пользователь обновляет версию
        user2.OnWork(); // Второй пользователь работает
        user3.OnUpgrade();
        user3.OnWork();

        // Проверка результатов изменения объектов
        Console.WriteLine("Результаты изменения объектов:");
        Console.WriteLine("{0} - Версия ПО: {1}", user1.Name, user1.Version);
        Console.WriteLine("{0} - Версия ПО: {1}", user2.Name, user2.Version);
        Console.WriteLine("{0} - Версия ПО: {1}", user3.Name, user3.Version);
    }

    private static void User_Upgrade(object sender, EventArgs e)
    {
        User user = (User)sender;
        Console.WriteLine("{0} обновил версию программного обеспечения {1} до версии {2}", user.Name, user.Software, user.Version);
    }

    private static void User_Work(object sender, EventArgs e)
    {
        User user = (User)sender;
        Console.WriteLine("{0} работает с программным обеспечением {1}", user.Name, user.Software);
    }
}