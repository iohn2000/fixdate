using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using fixDate.interfaces;

namespace fixDate;

public class ConfigurationReaderHardCoded : IConfigurationReader
{
    public SortedList<int, string> GetDateTimeParsingFormats()
    {
        return new SortedList<int, string>
        {
            {10, "yyyy-MM-dd HH.mm.ss"},
            {20, "yyyy-M-d H.m.s"},
            {30, "yyyy-MM-dd HH.mm"},
            {40, "yyyy-M-d H.m"},
            {50, "yyyy-M-d"},
            {60, "yyyy-MM-dd"}
        };
    }

    public SortedList<int, string> GetDateTimeMatchingPatterns()
    {
        return new SortedList<int, string>
        {
            {1, @"\d{4}-\d{1,2}-\d{1,2} \d{1,2}[_\.]\d{1,2}[_\.]\d{1,2}"},
            {2, @"\d{4}-\d{1,2}-\d{1,2} \d{1,2}[_\.]\d{1,2}"},
            {3, @"\d{4}-\d{1,2}-\d{1,2}"}
        };
    }

    public List<string> GetExcludedFoldersPatterns()
    {
        return new List<string>
        {
            "ScannedDocsTheYears",
            "pdf-tool"
        };
    }
}