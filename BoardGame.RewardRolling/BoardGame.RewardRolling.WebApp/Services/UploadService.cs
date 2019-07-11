using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.RewardRolling.WebApp.Services.Interfaces;
using Hinox.Static.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.RewardRolling.WebApp.Services
{
    public class UploadService : IUploadService
    {
        public async Task<string> StoreUploadedFile(IFormFile file)
        {
            var fileDirectory = "wwwroot/iofiles/upload/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}";
            var directory = string.Format(fileDirectory, DateTime.Now);

            var originalPath = string.Format("{0}/{1}", directory, file.FileName);

            string extension = Path.GetExtension(originalPath);

            var newFileName =
                file.FileName
                    .ToUnsignText()
                    .RemoveSpecialCharacters()
                    .Replace(" ", "-")
                    .ToLower()
                + "-"
                + DateTime.Now.Ticks.ToString();

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var fullPath = string.Format(
                "{0}/{1}{2}", 
                directory, 
                newFileName, 
                extension
                );

            using (FileStream bits = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(bits);
            }

            return fullPath;
        }
    }
}
