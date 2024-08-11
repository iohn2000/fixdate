
namespace fixDate.interfaces;

public interface IConfigurationReader
{
    SortedList<int,string> GetMatchingPatterns(string basePath = @".\");
    SortedList<int,string> GetParsingFormats(string basePath = @"\,");
}