namespace fixDate;

/*public class ConfigurationReader : IConfigurationReader
{
    public List<string> GetParsingFormats(string basePath)
    {
        string path = Path.Combine(basePath, "date-parse-formats.json");
        List<string>? result = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(path));
        if (result == null)
            throw new Exception($"error reading {path}");
        return result;
    }

    public List<string> GetMatchingPatterns(string basePath)
    {
        string path = Path.Combine(basePath, "date-matching-patterns.json");
        List<string>? result = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(path));
        if (result == null)
            throw new Exception($"error reading {path}");
        return result;
    }
}*/