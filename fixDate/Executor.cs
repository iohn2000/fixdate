using haha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fixDate.interfaces;

namespace fixDate
{
    /// <summary>
    /// class a starter class
    /// </summary>
    public class Executor
    {
        private readonly IFixDates _fixDates;

        public Executor(IFixDates fixDates)
        {
            _fixDates = fixDates;
        }

        public void Execute(bool forceUpdate, string relStartPath=@".\")
        {
            Console.WriteLine("Changing Modified Date of Filename according to suffix.");
            List<TheReportLine> report = _fixDates.LosGehts(forceUpdate, relStartPath);

            foreach (FStatus status in Enum.GetValues(typeof(FStatus)))
            {
                if (status == FStatus.Unchanged)
                    continue;
                report
                    .Where(w => w.FileStatus == status)
                    .Select(w => w.LogLine)
                    .ToList()
                    .ForEach(l => Console.WriteLine(l));
                Console.WriteLine("");
            }
        }
    }
}
