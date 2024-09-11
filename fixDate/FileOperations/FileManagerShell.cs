﻿using System.Diagnostics;

namespace fixDate.FileOperations;

/// <summary>
/// use operating system shell commands to change file dates
/// seems necessary when onedrive and cryptomator are used together
/// </summary>
public class FileManagerShell : IFileManager
{
    public string GetFileNameWithoutExtension(string fileName)
    {
        return Path.GetFileNameWithoutExtension(fileName);
    }

    public DateTime GetModifiedDate(string fileName)
    {
        FileInfo fi = new FileInfo(fileName);
        return fi.LastWriteTime;
    }

    public bool SetModifiedDate(string fileName, DateTime fileDate)
    {
        // Define the shell command you want to execute
        string command = $"powershell -command \"(Get-Item '{fileName}').LastWriteTime=('{fileDate.ToString("yyyy-MM-dd 12:00")}')\""; 

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

    public List<string> GetFileNames(string basePath)
    {
        // Define the shell command you want to execute
        string command = $"dir {basePath} /s /b"; // Replace with your actual command

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

        return output.Where(w => !string.IsNullOrWhiteSpace(w)).ToList();
    }
}
