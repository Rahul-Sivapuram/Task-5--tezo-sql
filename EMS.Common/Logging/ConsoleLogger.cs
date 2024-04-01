using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Common;

public class ConsoleLogger : ILogger
{
    public void LogInfo(string message) => LogMessage(message);

    public void LogSuccess(string message) => LogMessage(message, ConsoleColor.Green);

    public void LogError(string message) => LogMessage(message, ConsoleColor.Red);

    private void LogMessage(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
