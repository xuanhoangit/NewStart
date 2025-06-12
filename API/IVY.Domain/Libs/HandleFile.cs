using Microsoft.AspNetCore.Http;

namespace IVY.Domain.Libs;
public static class HandleFile
{   
    public static string GetUploadFilePath(string localStorage){
        return Path.Combine(Directory.GetCurrentDirectory(),$"wwwroot/{localStorage}");
    }
    public static async Task<bool> Upload(IFormFile file,string filePath){
        try
        {
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return true;
        }
        catch (System.Exception)
        {
            
            return false;
        }
    }
}