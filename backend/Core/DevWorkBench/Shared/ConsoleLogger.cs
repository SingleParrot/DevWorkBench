using System;

namespace DevWorkBench;

public static class ConsoleLogger
{
    private static readonly object _lock = new();

    public static void Info(string message)
    {
        lock (_lock)
        {
            Console.WriteLine($"\u001b[36m[{DateTime.Now:HH:mm:ss}] [INFO] {message}\u001b[0m");
            Console.Out.Flush();  // ← das ist der Fix
        }
    }

    public static void Warning(string message)
    {
        lock (_lock)
        {
            Console.WriteLine($"\u001b[33m[{DateTime.Now:HH:mm:ss}] [WARNING] {message}\u001b[0m");
            Console.Out.Flush();
        }
    }

    public static void Error(string message)
    {
        lock (_lock)
        {
            Console.WriteLine($"\u001b[31m[{DateTime.Now:HH:mm:ss}] [ERROR] {message}\u001b[0m");
            Console.Out.Flush();
        }
    }

    public static void Error(string message, Exception ex)
    {
        lock (_lock)
        {
            Console.WriteLine($"\u001b[31m[{DateTime.Now:HH:mm:ss}] [ERROR] {message}\u001b[0m");
            Console.WriteLine($"\u001b[31m  Details: {ex.Message}\u001b[0m");
            Console.Out.Flush();
        }
    }
}   