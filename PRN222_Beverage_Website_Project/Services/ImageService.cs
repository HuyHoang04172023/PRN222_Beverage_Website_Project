namespace PRN222_Beverage_Website_Project.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;
        private readonly string[] _allowedExtensions = { ".png", ".webp", ".jpg" }; // Các định dạng cho phép

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveImageAsync(IFormFile imageFile, string folderName)
        {
            // Kiểm tra xem có file hay không
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("No image file provided.");
            }

            // Kiểm tra định dạng file
            string fileExtension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Only .png and .webp files are allowed.");
            }

            // Đường dẫn thư mục lưu ảnh
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "images", folderName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Tạo tên file duy nhất
            string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Lưu file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            // Trả về đường dẫn tương đối
            return $"/images/{folderName}/{uniqueFileName}";
        }

        public void DeleteImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
            {
                return;
            }

            string fullPath = Path.Combine(_environment.WebRootPath, imagePath.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
