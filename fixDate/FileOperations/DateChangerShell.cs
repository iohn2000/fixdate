using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fixDate.FileOperations
{
    /// <summary>
    /// use operating system shell commands to change file dates
    /// seems necessary when onedrive and cryptomator are used together
    /// </summary>
    public class DateChangerShell : IModifiedDateChanger
    {
        public string GetFileNameWithoutExtension(string fname)
        {
            throw new NotImplementedException();
        }

        public DateTime GetModifiedDate(string fname)
        {
            throw new NotImplementedException();
        }

        public bool SetModifiedDate(string fname, DateTime fdate)
        {
            throw new NotImplementedException();
        }
    }
}
