using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BoardGame.RewardRolling.WebApp.Services.Interfaces
{
    public interface IUploadService
    {
        Task<string> StoreUploadedFile(IFormFile file);
    }
}
