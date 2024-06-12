using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCubeTrain.Services.Interface
{
    public interface IFTPService
    {
        Task<string> ListDirectoryAsync(string ftpServerUrl, string path, string ftpUsername = null, string ftpPassword = null);
        Task UploadFileAsync(string ftpServerUrl, string path, Stream fileStream, string ftpUsername = null, string ftpPassword = null);
        Task<bool> TestConnectionAsync(string ftpServerUrl, string ftpUsername = null, string ftpPassword = null);
    }
}