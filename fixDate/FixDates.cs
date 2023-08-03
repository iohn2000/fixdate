using fixDate.interfaces;

namespace fixDate
{
    public class FixDates : IFixDates
    {
        private const string ConfigPath = @".\";
        private readonly IFileNameReader _fileNameReader;
        private readonly IConfigurationReader _cfgReader;
        private readonly IDateMatch _dateMatcher;
        private readonly IDateParsing _dateParser;

        public FixDates(IFileNameReader fileNameReader, IConfigurationReader cfgReader, IDateMatch dateMatcher,
            IDateParsing dateParser)
        {
            _fileNameReader = fileNameReader;
            _cfgReader = cfgReader;
            _dateMatcher = dateMatcher;
            _dateParser = dateParser;
        }


        public List<TheReportLine> LosGehts(bool forceUpdate, string relativeStartPath = @".\")
        {
            // for parallel each - this has to be thread safe, ConcurrentBag ...
            List<TheReportLine> theReport = new List<TheReportLine>();

            SortedList<int, string> parseFormats = _cfgReader.GetParsingFormats(ConfigPath);
            SortedList<int, string> dateFormats = _cfgReader.GetMatchingPatterns(ConfigPath);

            var listOfFiles = _fileNameReader.GetAllFileNames(relativeStartPath).ToList();
          
            foreach (string theFile in listOfFiles)
            {
                CheckFileDateAndFix(forceUpdate, theFile, dateFormats, parseFormats, theReport);
            }

            return theReport;
        }

        private void CheckFileDateAndFix(
            bool forceUpdate, string f, SortedList<int,string> dateFormats, SortedList<int, string> parseFormats, List<TheReportLine> theReport)
        {
            bool success = false;
            string n = Path.GetFileNameWithoutExtension(f);
            TheMatchResult theMatch = _dateMatcher.FindDateFormatMatch(n, dateFormats);
            if (theMatch.Success)
            {
                var parseWentOk = _dateParser.TryParseDateExact(theMatch, parseFormats.Values.ToList(), out var fdate);
                if (parseWentOk)
                {
                    success = true;
                    FileInfo fi = new FileInfo(f);
                    if (fi.LastWriteTime != fdate || forceUpdate)
                    {
                        fi.LastWriteTime = fdate;
                        fi.Refresh();
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
}