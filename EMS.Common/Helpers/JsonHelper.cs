using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
namespace EMS.Common;

public class JsonHelper
{
    public void Save<T>(string filePath, T content)
    {
        string serializedContent = JsonSerializer.Serialize(content, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, serializedContent);
    }

    public T Deserialize<T>(string jsonData)
    {
        return JsonSerializer.Deserialize<T>(jsonData);
    }
}