
namespace fixDate.interfaces;

public interface IConfigurationReader
{
    SortedList<int,string> GetDateTimeMatchingPatterns();
    SortedList<int,string> GetDateTimeParsingFormats();
    List<string> GetExcludedFoldersPatterns();
}