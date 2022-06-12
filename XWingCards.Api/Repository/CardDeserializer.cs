using System.Text.Json;

namespace XWingCards.Api.Repositories;
public static class CardDeserializer {
    
    public static List<T> DeserializeList<T>(string json)
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
    public static T? Deserialize<T>(string json)
    {
        var jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true, // JSON is case-insensitive
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // JSON is camelCase
            WriteIndented = true, // JSON is indented
        };
        json = json.Replace("non-limited", "nonLimited")
            .ReplaceJankyCharacters()
            .Normalize();
        if (string.IsNullOrWhiteSpace(json))
            return default;
        return JsonSerializer.Deserialize<T>(json, jsonSerializerOptions)!;
    }

    private static string ReplaceJankyCharacters(this string json)
    {
        if (json.IndexOf('\u2013') > -1) json = json.Replace('\u2013', '-');
        if (json.IndexOf('\u2014') > -1) json = json.Replace('\u2014', '-');
        if (json.IndexOf('\u2015') > -1) json = json.Replace('\u2015', '-');
        if (json.IndexOf('\u2017') > -1) json = json.Replace('\u2017', '_');
        if (json.IndexOf('\u2018') > -1) json = json.Replace('\u2018', '\'');
        if (json.IndexOf('\u2019') > -1) json = json.Replace('\u2019', '\'');
        if (json.IndexOf('\u201a') > -1) json = json.Replace('\u201a', ',');
        if (json.IndexOf('\u201b') > -1) json = json.Replace('\u201b', '\'');
        if (json.IndexOf('\u201c') > -1) json = json.Replace("\u201c", "\\\"");
        if (json.IndexOf('\u201d') > -1) json = json.Replace("\u201d", "\\\"");
        if (json.IndexOf('\u201e') > -1) json = json.Replace("\u201e", "\\\"");
        if (json.IndexOf('\u2026') > -1) json = json.Replace("\u2026", "...");
        if (json.IndexOf('\u2032') > -1) json = json.Replace('\u2032', '\'');
        if (json.IndexOf('\u2033') > -1) json = json.Replace("\u2033", "\\\""); 
        return json;
    }
}