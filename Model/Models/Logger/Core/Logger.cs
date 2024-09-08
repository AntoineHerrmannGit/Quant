using Models.Models.Logger.Interface;
using Models.Enums;

namespace Models.Models.Logger.Core;

public class Logger : ILogger
{
    private string _file;
    private string _directory;
    private LoggerLevel _level;

    public string File { get { return _file; } }
    public string Dir { get { return _directory; } }
    public LoggerLevel Level { get { return _level; } }

    public Logger(string file, LoggerLevel level = LoggerLevel.Information)
    {
        _level = level;
        _file = file;

        var currentDirectory = Directory.GetCurrentDirectory();
        while (currentDirectory != null && !currentDirectory.EndsWith("Quant"))
        {
            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
        }
        
        if (currentDirectory == null)
            throw new ArgumentNullException("Invalid location : \"../Quant\" not found.");        

        if (!Directory.GetDirectories(currentDirectory).Contains("logs"))
            Directory.CreateDirectory(currentDirectory + "/logs");

        _directory = currentDirectory;
    }


    public void Debug(string message){}
    public void Info(string message){}
    public void Warning(string message){}
    public void Error(string message){}
    public void Fatal(string message){}
}
