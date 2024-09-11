using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            return Path.GetFileNameWithoutExtension(fname);
        }

        public DateTime GetModifiedDate(string fname)
        {
            FileInfo fi = new FileInfo(fname);
            return fi.LastWriteTime;
        }

        public bool SetModifiedDate(string fname, DateTime fdate)
        {
            // Define the shell command you want to execute
            string command = $"powershell -command \"(Get-Item '{fname}').LastWriteTime=('{fdate.ToString("dd/MM/yyyy 12:00")}')\""; 

            // Create a new process
            Process process = new Process();

            // Configure the process start info
            process.StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe", // Use "bash" for Linux/MacOS
                Arguments = $"/C {command}", // Use "-c" for bash on Linux/MacOS
                RedirectStandardOutput = true, // Redirect the standard output
                RedirectStandardError = true,  // Redirect the standard error
                UseShellExecute = false,       // Required for redirection
                CreateNoWindow = true          // Do not create a window
            };

            // Start the process
            process.Start();

            // Read the standard output
            var output = process.StandardOutput.ReadToEnd().Split(Environment.NewLine);

            // Read the standard error
            string error = process.StandardError.ReadToEnd();

            // Wait for the process to exit
            process.WaitForExit();

            // Print the error (if any)
            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine("Error:");
                Console.WriteLine(error);
            }

            return string.IsNullOrEmpty(error);
        }
    }
}
