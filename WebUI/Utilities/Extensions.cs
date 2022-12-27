using System.Runtime;
using static NuGet.Packaging.PackagingConstants;

namespace WebUI.Utilities
{
    public static class Extensions
    {
        public static bool CheckFileSize(this IFormFile file, int maxSize)
        {
            return file.Length / 1024 < maxSize;
        }
        public static bool CheckFileFormat(this IFormFile file, string fileFormat)
        {
            return file.ContentType.Contains(fileFormat);
        }

        public static async Task<string> CopyFileAsync(this IFormFile file, string wwwroot, params string[] folders)
        {
            try
            {
                string path = Helper.Combine(wwwroot, folders);
                string fileName = Guid.NewGuid().ToString() + file.FileName;
                string filePath = Path.Combine(path, fileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                   await file.CopyToAsync(fileStream);
                    return filePath;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
