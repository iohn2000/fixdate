using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fixDate.FileOperations
{
    public class DateChangerDotNet : IModifiedDateChanger
    {
        public string GetFileNameWithoutExtension(string fname)
        {
            return Path.GetFileNameWithoutExtension(fname);
        }

        public DateTime GetModifiedDate(string fname)
        {
            FileInfo fi = new FileInfo(fname);
            return fi.LastWriteTime;
        }

        public bool SetModifiedDate(string fname, DateTime fdate)
        {
            FileInfo fi = new FileInfo(fname);
            fi.LastWriteTime = fdate;
            fi.Refresh();
            return true;
        }
    }
}
