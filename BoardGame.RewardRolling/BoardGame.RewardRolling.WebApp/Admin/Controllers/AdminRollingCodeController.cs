using BoardGame.RewardRolling.WebApp.Admin.Models.RollingCode;
using Hinox.Mvc.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.RewardRolling.WebApp.Admin.Controllers
{
    public class AdminRollingCodeController : BaseRestController
    {
        private readonly Logger logger = LogManager.GetLogger("UploadFile");

        [HttpPost]
        public async Task<UploadCodeResultModel> UploadFile([FromForm] IFormFile file, [FromForm] string type)
        {
            try
            {
                logger.Info(string.Format("fileName:{0}, contentType:{1}, name: {2}, length: {3}", file.FileName, file.ContentType, file.Name, file.Length));
            }
            catch (Exception e)
            {
                logger.Error(e);
            }

            var fileDirectory = "wwwroot/iofiles/upload/{0:yyyy}/{0:MM}/{0:dd}/{0:HH}/{0:mm}";
            var directory = string.Format(fileDirectory, DateTime.Now);

            var originalPath = string.Format("{0}/{1}", directory, file.FileName);

            string extension = Path.GetExtension(originalPath);

            var newFileName = file.FileName + "-" + DateTime.Now.Ticks.ToString();

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            var fullPath = string.Format("{0}/{1}{2}", directory, newFileName, extension);

            using (FileStream bits = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(bits);
            }


            var workbook = new XSSFWorkbook(fullPath);
            var sheet = workbook.GetSheet("");


            return new UploadCodeResultModel();
        }
    }
}
