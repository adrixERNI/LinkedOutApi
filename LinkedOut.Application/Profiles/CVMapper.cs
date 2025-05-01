using LinkedOutApi.DTOs.CV;
using LinkedOutApi.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace LinkedOutApi.Profiles
{
    public static class CVMapper 
    {
        public static GetCV ToGetCVDto(this CV cvModel)
        {
            return new GetCV
            {
                Id = cvModel.Id,
                Name = cvModel.Name,
                File = cvModel.File,
                UserId = cvModel.UserId
            };
        }

        public static async Task<CV> ToCVFromCreateDtoAsync(this CreateCV createCVDto)
        {
            var cv = new CV
            {
                Name = createCVDto.Name,
                File = createCVDto.File,
                UserId = createCVDto.UserId
            };

            return cv;
        }
    }
}