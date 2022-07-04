﻿using Common.API;
using Common.Domain.Base;
using Common.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Seed.Api.Controllers
{
    [Authorize]
    [Route("api/document/upload")]
    public class UplaodController : Controller
    {

        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;
        private readonly string _uploadRoot;
        private readonly ConfigSettingsBase _configSettingsBase;
        private readonly IStorage _storage;

        public UplaodController(ILoggerFactory logger, IWebHostEnvironment env, IOptions<ConfigSettingsBase> configSettingsBase, IStorage storage)
        {
            this._logger = logger.CreateLogger<UplaodController>();
            this._env = env;
            this._uploadRoot = "upload";
            this._configSettingsBase = configSettingsBase.Value;
            this._storage = storage;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ICollection<IFormFile> files, string folder, bool rename = true)
        {
            var result = new HttpResult<List<string>>(this._logger);
            try
            {
                if (this._configSettingsBase.EnabledStorage)
                    return await this.StorageSystemUpload(files, folder, rename, result);

                return await this.FileSystemUpload(files, folder, rename, result);
            }
            catch (Exception ex)
            {
                var responseEx = result.ReturnCustomException(ex, " Seed - Upload");
				return responseEx;

            }
        }

        [HttpDelete("{folder}/{fileName}")]
        public async Task<IActionResult> Delete(string folder, string fileName)
        {
            var result = new HttpResult<List<string>>(this._logger);
            try
            {
                if (this._configSettingsBase.EnabledStorage)
                    return StorageSystemDelete(folder, fileName, result);

                return await FileSystemDelete(folder, fileName, result);

            }
            catch (Exception ex)
            {
                var responseEx =  result.ReturnCustomException(ex, "Seed - upload");
				return responseEx;
            }
        }

        private async Task<IActionResult> FileSystemDelete(string folder, string fileName, HttpResult<List<string>> result)
        {
            var uploads = Path.Combine(this._env.ContentRootPath, this._uploadRoot, folder);
            await Task.Run(() =>
            {
                new FileInfo(Path.Combine(uploads, fileName)).Delete();
            });
            return result.ReturnCustomResponse();
        }

        private IActionResult StorageSystemDelete(string folder, string fileName, HttpResult<List<string>> result)
        {
            this._storage.Delete(folder, fileName);
            return result.ReturnCustomResponse();
        }

        private async Task<IActionResult> StorageSystemUpload(ICollection<IFormFile> files, string folder, bool rename, HttpResult<List<string>> result)
        {
            var fileSuccess = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        var fileName = file.FileName;
                        if (rename)
                            fileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(file.FileName));

                        file.CopyTo(ms);
                        await this._storage.Upload(ms.ToArray(), folder, fileName);
                        fileSuccess.Add(fileName);
                    }
                }
            }

            return result.ReturnCustomResponse(fileSuccess);
        }

        private async Task<IActionResult> FileSystemUpload(ICollection<IFormFile> files, string folder, bool rename, HttpResult<List<string>> result)
        {
            var uploads = Path.Combine(this._env.ContentRootPath, this._uploadRoot, folder);
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var fileSuccess = new List<string>();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    var fileName = file.FileName;
                    if (rename)
                        fileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), Path.GetExtension(file.FileName));

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    fileSuccess.Add(fileName);
                }
            }

            return result.ReturnCustomResponse(fileSuccess);
        }

    }
}
