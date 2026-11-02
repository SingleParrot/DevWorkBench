using System;
using System.IO;

namespace DevWorkBench;
// Singleton pattern for logging to a file
public sealed class FileLogger // File can't be inherited, Singleton pattern
{
    private string _filePath = string.Empty;
    private readonly object _lock = new();

    private FileLogger() { }
    // Property to access the singleton instance of FileLogger
    public static FileLogger Instance { get; } = new FileLogger(); // Only the code inside the class can create an instance of FileLogger, and it is created only once and shared across the application.

    public void CreateLogFile()
    {
        lock (_lock)
        {
            string dir = Path.Combine(AppContext.BaseDirectory, "logs");
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string fileName = $"log_{timestamp}.log";

            Directory.CreateDirectory(dir);   // ← Ordner sicherstellen
            _filePath = Path.Join(dir, fileName);
            File.Create(_filePath).Dispose(); // ← Datei tatsächlich anlegen
        }
    }

    public void AddLog(string logEntry, string type)
    {
        lock (_lock)
        {
            string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{type}] {logEntry}{Environment.NewLine}";

            if (!string.IsNullOrEmpty(_filePath))
            {
                File.AppendAllText(_filePath, entry); // ← synchron, lock bleibt bis nach dem Write
            }
        }
    }
}   