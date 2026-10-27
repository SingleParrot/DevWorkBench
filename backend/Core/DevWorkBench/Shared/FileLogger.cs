using System; 
using System.IO;
namespace DevWorkBench;

public class FileLogger
{
    public string FilePath = String.Empty;
    public async Task CreateLogFile ()
    {
        string Timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        string FileName = "log_" + Timestamp + ".log";
        this.FilePath = Path.Join("logs",FileName);

        // Server started
    }

    public async Task AddLog (string logEntry,string type)
    {
        string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{type}] {logEntry}{Environment.NewLine}";
        if (File.Exists(FilePath))
        {
            await File.AppendAllTextAsync(this.FilePath,entry);
        }

    }
}