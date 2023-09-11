namespace BlogTemplate.Presentation.Utills
{
    public class ImageUtility
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageUtility(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file)
        {
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "thumbnails");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(folderPath, uniqueFileName);
            using (FileStream fileStream = System.IO.File.Create(filePath))
            {
                file.CopyTo(fileStream);
            }
            return uniqueFileName;
        }

        public void Remove(string? thumbnailUrl)
        {
            if (string.IsNullOrWhiteSpace(thumbnailUrl))
                return;
            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "thumbnails");
            var filePath = Path.Combine(folderPath, thumbnailUrl);
            System.IO.File.Delete(filePath);
        }
    }
}
