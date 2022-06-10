using System.Text.Json;

namespace XWingCards.Api.Repositories;
public static class CardDeserializer {
    
    public static List<T> DeserializeCards<T>(string json)
    {
        var jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true, // JSON is case-insensitive
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // JSON is camelCase
            WriteIndented = true, // JSON is indented
        };
        json = json.Replace("non-limited", "nonLimited");
        if (string.IsNullOrWhiteSpace(json))
            return new List<T>();
        return JsonSerializer.Deserialize<List<T>>(json, jsonSerializerOptions)!;
    }
}