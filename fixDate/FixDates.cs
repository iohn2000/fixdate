using fixDate.FileOperations;
using fixDate.interfaces;
using fixDate.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace fixDate;

public class FixDates(IFileListFilter fileListFilter, IConfigurationReader cfgReader, IDateMatch dateMatcher,
    IDateParsing dateParser, IModifiedDateChanger dateChanger) : IFixDates
{
    public List<TheReportLine> LosGehts(bool forceUpdate, string relativeStartPath = @".\")
    {
        // for parallel each - this has to be thread safe, ConcurrentBag ...
        List<TheReportLine> theReport = new List<TheReportLine>();

        SortedList<int, string> parseFormats = cfgReader.GetDateTimeParsingFormats();
        SortedList<int, string> dateFormats = cfgReader.GetDateTimeMatchingPatterns();
        List<string> excludedFolders = cfgReader.GetExcludedFoldersPatterns();

        var listOfFiles = fileListFilter.GetAllFileNames(relativeStartPath).ToList();
          
        foreach (var theFileItem in listOfFiles)
        {
            if (theFileItem != null && theFileItem.IsIncluded) 
            {
                CheckFileDateAndFix(forceUpdate, theFileItem.FileName, dateFormats, parseFormats, theReport);
            }
            else
            {
                theReport.Add(new TheReportLine
                { 
                    FileStatus = FStatus.Excluded, LogLine = $"{FStatus.Excluded}: {theFileItem.FileName} is excluded."
                });
            }
            Console.Write("*");
        }

        return theReport;
    }

    private void CheckFileDateAndFix(
        bool forceUpdate, string f, SortedList<int,string> dateFormats, SortedList<int, string> parseFormats, List<TheReportLine> theReport)
    {
        bool success = false;
        string n = dateChanger.GetFileNameWithoutExtension(f);
        TheMatchResult theMatch = dateMatcher.FindDateFormatMatch(n, dateFormats);
        if (theMatch.Success)
        {
            var parseWentOk = dateParser.TryParseDateExact(theMatch, parseFormats.Values.ToList(), out var fdate);
            if (parseWentOk)
            {
                success = true;
                
                var lastWriteTime  = dateChanger.GetModifiedDate(f);
                if (lastWriteTime != fdate || forceUpdate)
                {
                    dateChanger.SetModifiedDate(f,fdate);
                    theReport.Add(new TheReportLine
                    {
                        FileStatus = FStatus.Changed,
                        LogLine = $"{FStatus.Changed}: {f} date changed to:{fdate.ToString("u")}."
                    });
                }
                else
                {
                    theReport.Add(new TheReportLine
                    {
                        FileStatus = FStatus.Unchanged,
                        LogLine = $"{FStatus.Unchanged}: file date already correct for {f}"
                    });
                }
            }
        }

        if (!success)
        {
            theReport.Add(new TheReportLine
                {FileStatus = FStatus.Problem, LogLine = $"{FStatus.Problem}: cannot change {f} "});
        }
    }
}