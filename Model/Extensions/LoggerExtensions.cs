using Models.Models.Logger.Core;
using Models.Enums;

namespace Models.Extensions;

public static class LoggerExtensions
{
    #region  Streaming informations
    public static void Debug(this Logger logger, string message)
    {
        if (logger.Level >= LoggerLevel.Debug)
        {
            message = logger.FormatMessage(message);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(logger.Dir, "logs")))
            {
                outputFile.WriteLine(message);
            }
        }
    }

    public static void Info(this Logger logger, string message)
    {
        if (logger.Level >= LoggerLevel.Information)
        {
            message = logger.FormatMessage(message);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(logger.Dir, "logs")))
            {
                outputFile.WriteLine(message);
            }
        }
    }
    
    public static void Warning(this Logger logger, string message)
    {
        if (logger.Level >= LoggerLevel.Warning)
        {
            message = logger.FormatMessage(message);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(logger.Dir, "logs")))
            {
                outputFile.WriteLine(message);
            }
        }
    }
    
    public static void Error(this Logger logger, string message)
    {
        if (logger.Level >= LoggerLevel.Error)
        {
            message = logger.FormatMessage(message);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(logger.Dir, "logs")))
            {
                outputFile.WriteLine(message);
            }
        }
    }
    
    public static void Fatal(this Logger logger, string message)
    {
        if (logger.Level >= LoggerLevel.Fatal)
        {
            message = logger.FormatMessage(message);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(logger.Dir, "logs")))
            {
                outputFile.WriteLine(message);
            }
        }
    }

    #endregion Streaming informations

    #region Private
    private static string FormatMessage(this Logger logger, string message)
    {
        string timeStamp = DateTime.Now.ToString();
        return $"[{timeStamp}] [{logger.Level}] : {message}";
    }
    #endregion Private
}
