namespace DreamGarden.Shared
{
    public interface IFileService
    {
        // Define methods for saving and deleting files
    
        void DeleteFile(string fileName);
        Task<string> SaveFile(IFormFile file, string [] allowedExtensions);
    }
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public async Task<string> SaveFile(IFormFile file, string[] allowedExtensions)
        {
            var wwwPath=_environment.WebRootPath;
            var path=Path.Combine(wwwPath, "images"); //images to be uploaded will be stored in wwwroot/images
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path); //create the directory if it does not exist    
            }

            var extension = Path.GetExtension(file.FileName);
            if(!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("Invalid file type. Allowed types are: " + string.Join(", ", allowedExtensions));
            }

            string fileName = $"{Guid.NewGuid()}{extension}"; //generate a unique file name
            string fileNameWithPath = Path.Combine(path, fileName);
            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await file.CopyToAsync(stream); //copy the file to the server
            return fileName;
        }

        public void DeleteFile(string fileName)
        {
            var wwwPath = _environment.WebRootPath;
            var fileNameWithPath = Path.Combine(wwwPath, "images\\", fileName); //path to the file to be deleted
            if (!File.Exists(fileNameWithPath))
            {
                throw new FileNotFoundException(fileName); //file does not exist exception
            }

            File.Delete(fileNameWithPath); //delete the file if it exists
        }

        
    }
}
