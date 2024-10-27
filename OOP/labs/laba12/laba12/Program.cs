using System;

public class Program
{
    public static void Main(string[] args)
    {
        string logFilePath = "D:\\1 сем\\ооп\\лабы\\log.txt";

        XXXLog logger = new XXXLog(logFilePath);

        // Пример записи действия в лог файл
        logger.LogAction("File Created", "file.txt", "D:\\1 сем\\ооп\\лабы\\log.txt");

        // Пример чтения содержимого лог файла
        logger.ReadLogFile();

        // Пример поиска текста в лог файле
        string searchText = "File Created";
        logger.SearchLog(searchText);
    }
}

public class XXXLog
{
    private readonly string logFilePath;

    public XXXLog(string logFilePath)
    {
        this.logFilePath = logFilePath;
    }

    public void LogAction(string action, string fileName, string filePath)
    {
        string logEntry = $"{DateTime.Now} - Action: {action}, File Name: {fileName}, File Path: {filePath}";

        using (StreamWriter writer = File.AppendText(logFilePath))
        {
            writer.WriteLine(logEntry);
        }
    }

    public void ReadLogFile()
    {
        if (File.Exists(logFilePath))
        {
            string logContents = File.ReadAllText(logFilePath);
            Console.WriteLine(logContents);
        }
        else
        {
            Console.WriteLine("Log file does not exist.");
        }
    }

    public void SearchLog(string searchText)
    {
        if (File.Exists(logFilePath))
        {
            string logContents = File.ReadAllText(logFilePath);

            if (logContents.Contains(searchText))
            {
                Console.WriteLine("Search results:");
                Console.WriteLine(logContents);
            }
            else
            {
                Console.WriteLine("Search text not found.");
            }
        }
        else
        {
            Console.WriteLine("Log file does not exist.");
        }
    }
}

public class XXXDiskInfo
{
    public void GetFreeSpace(string driveName)
    {
        DriveInfo drive = new DriveInfo(driveName);

        if (drive.IsReady)
        {
            long freeSpace = drive.TotalFreeSpace;
            Console.WriteLine($"Свободное пространство на диске {driveName}: {freeSpace / (1024 * 1024 * 1024)} D");
        }
        else
        {
            Console.WriteLine($"Диск {driveName} не готов к использованию.");
        }
    }

    public void GetFileSystem(string driveName)
    {
        DriveInfo drive = new DriveInfo(driveName);

        if (drive.IsReady)
        {
            string fileSystem = drive.DriveFormat;
            Console.WriteLine($"Файловая система диска {driveName}: {fileSystem}");
        }
        else
        {
            Console.WriteLine($"Диск {driveName} не готов к использованию.");
        }
    }

    public void GetDriveInfo()
    {
        DriveInfo[] drives = DriveInfo.GetDrives();

        foreach (DriveInfo drive in drives)
        {
            Console.WriteLine($"Диск: {drive.Name}");
            Console.WriteLine($"Объем: {drive.TotalSize / (1024 * 1024 * 1024)} D");
            Console.WriteLine($"Доступное пространство: {drive.AvailableFreeSpace / (1024 * 1024 * 1024)} D");
            Console.WriteLine($"Метка тома: {drive.VolumeLabel}");
            Console.WriteLine();
        }
    }
}

