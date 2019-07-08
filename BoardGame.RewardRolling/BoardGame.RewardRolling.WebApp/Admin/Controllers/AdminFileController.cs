using BoardGame.RewardRolling.Core.Statics;
using BoardGame.RewardRolling.WebApp.Admin.Models.File;
using Hinox.Mvc.Controllers;
using Hinox.Static.Enumerate;
using Hinox.Static.Extensions;
using Hinox.Static.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    [Route("admin/api/files")]
    public class AdminFileController : BaseRestController
    {
        private readonly Logger logger = LogManager.GetLogger("UploadFile");

        [HttpPost]
        public async Task<UploadFileResultModel> UploadFile([FromForm] IFormFile file, [FromForm] string group)
        {
            try
            {
                logger.Info(string.Format("fileName:{0}, contentType:{1}, name: {2}, length: {3}", file.FileName, file.ContentType, file.Name, file.Length));
            }
            catch (Exception e)
            {
                logger.Error(e);
            }

            string fileGroupName = GetFileGroupName(group);

            var time = DateTime.Now;
            var fileDirectory = "wwwroot/iofiles/upload/{0}/{1:yyyy}/{1:MM}/{1:dd}/{1:HH}/{1:mm}";
            var directory = string.Format(fileDirectory, fileGroupName, time);

            var originalPath = string.Format("{0}/{1}", directory, file.FileName);

            string extension = Path.GetExtension(originalPath);

            var newFileName =
                file.FileName.ToUnsignText().RemoveSpecialCharacters().Replace(" ", "-").ToLower()
                + "-"
                + DateTime.Now.Ticks.ToString();

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var fullPath = string.Format("{0}/{1}{2}", directory, newFileName, extension);

            using (FileStream bits = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(bits);
            }

            var result = new UploadFileResultModel()
            {
                CdnAddress = "",
                Path = string.Format("/iofiles/upload/{0}/{1:yyyy}/{1:MM}/{1:dd}/{1:HH}/{1:mm}/{2}{3}", fileGroupName, time, newFileName, extension)
            };
            return result;
        }

        private static string GetFileGroupName(string groupName)
        {
            string fileGroupName = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(groupName))
                {
                    var fileType = Enumeration.FromDisplayName<UploadedFileGroup>(groupName);
                    fileGroupName = fileType.Name;
                }
                else
                    fileGroupName = UploadedFileGroup.Unknown.Name;
            }
            catch (Exception e)
            {
                fileGroupName = UploadedFileGroup.Unknown.Name;
            }

            return fileGroupName;
        }
    }
}
