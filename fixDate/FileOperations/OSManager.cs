using System.Runtime.InteropServices;

namespace fixDate.FileOperations
{
    public static class OSManager
    {
        public static OSPlatform GetOperationSystemPlatform()
        {

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OSPlatform.Windows;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OSPlatform.Linux;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OSPlatform.OSX;
            }
            else
            {
                return OSPlatform.FreeBSD;
            }
        }
    }
}
