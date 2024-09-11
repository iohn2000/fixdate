using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fixDate.FileOperations
{
    public interface IModifiedDateChanger
    {
        bool SetModifiedDate(string fname, DateTime fdate);
        DateTime GetModifiedDate(string fname);
        string GetFileNameWithoutExtension(string fname);
    }
}
