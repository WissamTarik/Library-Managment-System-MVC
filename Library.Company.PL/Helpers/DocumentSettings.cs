namespace Library.Company.PL.Helpers
{
    public  static class DocumentSettings
    {
        public  static string UploadFile(IFormFile formFile,string folderName)
        {
            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files/", folderName);
            var Extension=Path.GetExtension(formFile.FileName);

            var fileName = $"{Guid.NewGuid()}{Extension}";

            var FilePath=Path.Combine(FolderPath, fileName);

           using var Stream=new FileStream(FilePath, FileMode.Create);
            formFile.CopyTo(Stream);
            return fileName;
        }
        public static void Delete(string fileName,string folderName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files/", folderName, fileName);

            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
