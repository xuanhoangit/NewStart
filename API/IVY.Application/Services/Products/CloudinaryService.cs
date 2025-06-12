using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService()
    {   
                // Thay bằng thông tin của bạn
        Account account = new Account("delq6xxku", "572575937129645", "lLMU8D9geiIX85fa5OOteXa3Io4");
        _cloudinary = new Cloudinary(account);
    }
    public async Task<bool> DeleteImageAsync(string publicId)
    {
        var deletionParams = new DeletionParams(publicId)
        {
            ResourceType = ResourceType.Image
        };

        var result = await _cloudinary.DestroyAsync(deletionParams);

        return result.Result == "ok";
    }
    public async Task<string> UploadImageAsync(IFormFile file,string storageFilePath,string filename=null)
    {
        await using var stream = file.OpenReadStream();

        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = storageFilePath, // tùy chọn, để sắp xếp
            PublicId = string.IsNullOrEmpty(filename)?Guid.NewGuid().ToString():filename, // tên tệp trên cloud
            Overwrite = true,
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return uploadResult.SecureUrl.ToString(); // Trả về URL ảnh
    }
}
