using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace API.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);//IForm is a file sent via http req
        Task<DeletionResult> DeletePhotoAsync(string photoPublicId);
    }
}