using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS;

public class ConsoleWriter : IWriter
{
    public void ShowInfo(string message) => ShowMessage(message);

    public void ShowSuccess(string message) => ShowMessage(message, ConsoleColor.Green);

    public void ShowError(string message) => ShowMessage(message, ConsoleColor.Red);

    private void ShowMessage(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
