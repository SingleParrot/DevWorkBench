namespace DevWorkBench;

public static class ConsoleLogger
{
    // 1. Info-Log (Cyan für gute Lesbarkeit)
    public static void Info(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [INFO] {message}");
        Console.ResetColor(); // Wichtig: Farbe zurücksetzen, sonst bleibt alles farbig!
    }

    // 2. Warning-Log (Gelb für Warnungen)
    public static void Warning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [WARNING] {message}");
        Console.ResetColor();
    }

    // 3. Error-Log (Rot für Fehlermeldungen - ohne Exception)
    public static void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [ERROR] {message}");
        Console.ResetColor();
    }

    // 4. Error-Log mit echter Exception (Überladene Methode / Overloading)
    public static void Error(string message, Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] [ERROR] {message}");
        Console.WriteLine($"Details: {ex.Message}");
        Console.ResetColor();
    }
}