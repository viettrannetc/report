using CsvHelper;
using VT.Common;
using VT.Extension;
using VT.Interface;
using VT.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace VT.Implementation
{
    /// <summary>
    /// https://joshclose.github.io/CsvHelper/getting-started
    /// </summary>
    public class FolderCollectorHandler : IDataCollector
    {
        private List<JiraReportFormatter> Download(string url)
        {
            try
            {
                string response;
                var request = (HttpWebRequest)WebRequest.Create(url);
                var encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(Configuration.Username + ":" + Configuration.Password));
                request.Headers.Add("Authorization", "Basic " + encoded);
                HttpWebResponse httpWebResponse = (HttpWebResponse)request.GetResponse();
                using (var sr = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    response = sr.ReadToEnd();
                }

                Console.WriteLine(response);
                //TODO: store to database 
            }
            catch (Exception ex)
            {

                throw;
            }

            return null;
        }

        public List<ReportRawDataModel> BuildDrawData(string folderPath, DateTime from, DateTime to)
        {
            var filePaths = folderPath.ReadFilesFromFolder(from, to);

            var result = new List<ReportRawDataModel>();

            foreach (var filePath in filePaths)
            {
                result.AddRange(ParseFromFileToDrawTableData(filePath));
            }

            return result;
        }

        private List<ReportRawDataModel> ParseFromFileToDrawTableData(string filePath)
        {
            var result = new List<ReportRawDataModel>();

            using (var reader = new StreamReader(filePath))
            {
                DateTime dateData = filePath.GetDateByFileName();
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    result = csv.GetRecords<ReportRawDataModel>().ToList();
                    result.ForEach(r =>
                    {
                        r.EnumStatus = r.Status.ParseToJiraEnumStatus();
                        r.PulledDataAt = dateData;
                    });
                }
            }
            return result;
        }
    }
}
