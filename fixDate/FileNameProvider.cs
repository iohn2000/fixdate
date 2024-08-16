using fixDate.interfaces;

namespace fixDate
{
    public class FileNameProvider : IFileNameProvider
    {
        public List<string> GetFileNames(string basePath)
        {
            // status : cryptomator mapping is working
            //          onedrive alone is working
            //          onedrive with crypomator is not working
            //
            /*try
            {*/
            EnumerationOptions enumOptions = new EnumerationOptions { MatchCasing = MatchCasing.CaseInsensitive, RecurseSubdirectories = true };
             
                var files = Directory.GetFiles(Path.Combine(basePath, ""), "*.*", enumOptions).ToList();
            /*
               // Variation of GetFiles, also not working
               DirectoryInfo dir = new DirectoryInfo(basePath);  
                var files = dir.GetFiles(
                Path.Combine(Path.Combine(basePath, ""), "*.*"),
                enumOptions).Select(s=>s.FullName).ToList();
            */
            return files;
            /*}
            
            catch (IOException ioEx)
            {
                Console.WriteLine("IO Exception: " + ioEx.Message);
            }
            catch (UnauthorizedAccessException unAuthEx)
            {
                Console.WriteLine("Unauthorized Access: " + unAuthEx.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Exception: " + ex.Message);
            }*/
        }
    }
}
