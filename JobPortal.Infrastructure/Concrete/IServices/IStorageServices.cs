using JobPortal.Infrastructure.Global;
using Microsoft.AspNetCore.Http;

namespace JobPortal.Infrastructure.Concrete.IServices
{
    public interface IStorageServices
    {
        Task<APIResponse> DeleteFile(string path);
        Task<ServiceResponse<string>> UploadFile(string path, IFormFile file);
        Task<ServiceResponse<string>> UploadFile(string path, string file);
    }
}