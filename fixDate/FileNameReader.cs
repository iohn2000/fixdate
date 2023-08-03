using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fixDate.interfaces;

namespace fixDate
{
    public class FileNameReader : IFileNameReader
    {
        public string[] GetAllFileNames(string basePath)
        {
            EnumerationOptions o = new EnumerationOptions { MatchCasing = MatchCasing.CaseInsensitive, RecurseSubdirectories = true };
            return Directory.GetFiles(Path.Combine(basePath, ""), "*.*", o);
        }
    }
}
