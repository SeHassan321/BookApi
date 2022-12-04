namespace BooksApi.Helpers
{
    public class DocumentSitting
    {
        public static string addFile(IFormFile file, string folderName)
        {
            var pathFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);
            

            var fileName = $"{file.FileName}";
            var filePath = Path.Combine(pathFolder, fileName);

            using var fs = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fs);
            return $"{fileName}"; ;
        }

        public static void deleteFile(string folderName ,string fileName)
        {
            var pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName, fileName);
            if (File.Exists(pathFile))
            {
                File.Delete(pathFile);
            } 
        }
    }
}
