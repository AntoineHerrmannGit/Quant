using System;

namespace Models.Enums.Logger.Interface;

public interface ILogger
{
    void Debug(string message);
    void Info(string message);
    void Warning(string message);
    void Error(string message);
    void Fatal(string message);
}
