namespace PRN222_Beverage_Website_Project.Services
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile, string folderName);
        void DeleteImage(string imagePath);
    }
}
