using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Common;

public interface ILogger
{
    void LogSuccess(string message);
    void LogError(string message);
    void LogInfo(string message);
}
