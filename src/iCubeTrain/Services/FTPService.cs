using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using iCubeTrain.Services.Interface;

namespace iCubeTrain.Services
{
    public class FTPService : IFTPService
    {
        public async Task<string> ListDirectoryAsync(string ftpServerUrl, string path, string? ftpUsername = null, string? ftpPassword = null)
        {
            var request = (FtpWebRequest)WebRequest.Create($"{ftpServerUrl}/{path}");
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            // if credentials are null return Not logged in
            if (!string.IsNullOrEmpty(ftpUsername) && !string.IsNullOrEmpty(ftpPassword))
            {
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            }else{
                return "Not logged in";
            }

            using var response = (FtpWebResponse)await request.GetResponseAsync();
            using var responseStream = response.GetResponseStream();
            using var reader = new StreamReader(responseStream);
            
            return await reader.ReadToEndAsync();
        }

        public async Task UploadFileAsync(string ftpServerUrl, string path, Stream fileStream, string? ftpUsername = null, string? ftpPassword = null)
        {
            var request = (FtpWebRequest)WebRequest.Create($"{ftpServerUrl}/{path}");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            if (!string.IsNullOrEmpty(ftpUsername) && !string.IsNullOrEmpty(ftpPassword))
            {
                request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            }

            using var requestStream = await request.GetRequestStreamAsync();
            await fileStream.CopyToAsync(requestStream);
            requestStream.Close();

            using var response = (FtpWebResponse)await request.GetResponseAsync();
        }

        public async Task<bool> TestConnectionAsync(string ftpServerUrl, string? ftpUsername = null, string? ftpPassword = null)
        {
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(ftpServerUrl);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                if (!string.IsNullOrEmpty(ftpUsername) && !string.IsNullOrEmpty(ftpPassword))
                {
                    request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                }

                using var response = (FtpWebResponse)await request.GetResponseAsync();
                return response.StatusCode == FtpStatusCode.OpeningData || response.StatusCode == FtpStatusCode.DataAlreadyOpen;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}