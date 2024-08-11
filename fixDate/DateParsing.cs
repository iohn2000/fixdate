using System.Globalization;
using fixDate.interfaces;

namespace fixDate;

public class DateParsing : IDateParsing
{
    /// <summary>
    /// a wrapper around datetime.tryparseexcat
    /// </summary>
    /// <param name="theMatchValue"></param>
    /// <param name="parseFormats"></param>
    /// <param name="fdate"></param>
    /// <returns></returns>
    public bool TryParseDateExact(TheMatchResult theMatchValue, List<string> parseFormats, out DateTime fdate)
    {
        return DateTime.TryParseExact(theMatchValue.TheValue, 
            parseFormats.ToArray(), 
            new CultureInfo("de-DE"), 
            DateTimeStyles.None, 
            out fdate);
    }
}