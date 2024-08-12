using fixDate.interfaces;

namespace fixDate
{
    public class FileNameProvider : IFileNameProvider
    {
        public List<string> GetFileNames(string basePath)
        {
            EnumerationOptions o = new EnumerationOptions { MatchCasing = MatchCasing.CaseInsensitive, RecurseSubdirectories = true };
            return Directory.GetFiles(Path.Combine(basePath, ""), "*.*", o).ToList();
        }
    }
}
