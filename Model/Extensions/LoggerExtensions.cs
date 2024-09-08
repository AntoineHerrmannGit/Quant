using System;

namespace Models.Extensions;

public class LoggerExtensions
{
    #region  Streaming informations
    public void Debug(this Logger logger, string message)
    {
        if (_level >= LoggerLevel.Debug)
        {
            message = FormatMessage(message);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_directory, "logs")))
            {
                outputFile.WriteLine(message);
            }
        }
    }

    public void Info(this Logger logger, string message)
    {
        if (_level >= LoggerLevel.Information)
        {
            message = FormatMessage(message);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_directory, "logs")))
            {
                outputFile.WriteLine(message);
            }
        }
    }
    
    public void Warning(this Logger logger, string message)
    {
        if (_level >= LoggerLevel.Warning)
        {
            message = FormatMessage(message);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_directory, "logs")))
            {
                outputFile.WriteLine(message);
            }
        }
    }
    
    public void Error(this Logger logger, string message)
    {
        if (_level >= LoggerLevel.Error)
        {
            message = FormatMessage(message);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_directory, "logs")))
            {
                outputFile.WriteLine(message);
            }
        }
    }
    
    public void Fatal(this Logger logger, string message)
    {
        if (_level >= LoggerLevel.Fatal)
        {
            message = FormatMessage(message);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(_directory, "logs")))
            {
                outputFile.WriteLine(message);
            }
        }
    }

    #endregion Streaming informations

    #region Private
    private string FormatMessage(string message)
    {
        string timeStamp = DateTime.Now.ToString();
        return $"[{timeStamp}] [{_level}] : {message}";
    }
    #endregion Private
}
