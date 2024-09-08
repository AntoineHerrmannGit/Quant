using Models.Enums.Logger.Interface;
using Models.Logger;

namespace Tools.Logger;

public class Logger : ILogger
{
    private string _file;
    private string _directory;
    private LoggerLevel _level;

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
