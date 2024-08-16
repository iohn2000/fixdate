using fixDate.Models;

namespace fixDate.interfaces;

public interface IDateParsing
{
    bool TryParseDateExact(TheMatchResult theMatchValue, List<string> parseFormats, out DateTime fdate);
}