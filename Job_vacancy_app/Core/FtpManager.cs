using Job_vacancy_app.Model.DataBase;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Job_vacancy_app.Core
{
    internal class FtpManager
    {
        public async Task<bool> UploadFile(string fileName, string filePath)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://91.224.136.222/" + fileName);

                request.Method = WebRequestMethods.Ftp.UploadFile;

                FileStream fs = new FileStream(filePath, FileMode.Open);
                byte[] fileContents = new byte[fs.Length];
                await fs.ReadAsync(fileContents, 0, fileContents.Length);
                fs.Close();
                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                await requestStream.WriteAsync(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                response.Close();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> DownloadFile(string fileName, string filePath)
        {

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://91.224.136.222/" + fileName);

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (FileStream fileStream = File.Create(Path.Combine(filePath, fileName)))
            {
                byte[] buffer = new byte[2048];
                int bytesRead;

                while ((bytesRead = await responseStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await fileStream.WriteAsync(buffer, 0, bytesRead);
                }
            }
            return true;
        }
    }
}

