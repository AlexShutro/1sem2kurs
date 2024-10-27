using System;

Password password1 = new Password("password123");
Password password2 = new Password("ArsenalAubaLawa1804");

var password = password1;
Console.WriteLine(password.IsLengthValid()); // Правда
Console.WriteLine(password2.IsLengthValid()); // Ложь

Console.WriteLine(password.GetMiddleChar()); // 'w'

Console.WriteLine(password > password2); // Правда
Console.WriteLine(password != password2); // Правда

Console.WriteLine(password); // "password123"

password++; // Сброс пароля к значению по умолчанию
Console.WriteLine(password); // "пароль"

if (password) // Проверка, надежен ли пароль
{
    Console.WriteLine("Password is strong");
}
else
{
    Console.WriteLine("Password is weak");
}

public class Password
{
    private string value;
    private const string defaultPassword = "Password";

    public Password(string value)
    {
        this.value = value;
    }

    public static Password operator -(Password password, char item)
    {
        if (password.value.Length > 0)
        {
            password.value = password.value.Substring(0, password.value.Length - 1) + item;
        }
        return password;
    }

    public static bool operator >(Password password1, Password password2)
    {
        return password1.value.Length > password2.value.Length;
    }

    public static bool operator <(Password password1, Password password2)
    {
        throw new NotImplementedException();
    }

    public static bool operator !=(Password password1, Password password2)
    {
        return password1.value != password2.value;
    }

    public static bool operator ==(Password password1, Password password2)
    {
        return !(password1 != password2);
    }

    public static Password operator ++(Password password)
    {
        password.value = defaultPassword;
        return password;
    }

    public static bool operator true(Password password)
    {
        return password.IsStrong();
    }

    public static bool operator false(Password password)
    {
        return !password.IsStrong();
    }

    public char GetMiddleChar()
    {
        int middleIndex = value.Length / 2;
        return value[middleIndex];
    }

    public bool IsLengthValid()
    {
        return value.Length >= 6 && value.Length <= 12;
    }

    private bool IsStrong()
    {
        // Проверьте, надежен ли пароль (например, содержит заглавные и строчные буквы, цифры и символы).
        // Эта реализация является всего лишь примером и должна быть заменена на более надежную
        return value.Length >= 8 && value != value.ToLower() && value != value.ToUpper() && value.Contains("123");
    }
}