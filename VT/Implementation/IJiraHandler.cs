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
    public class JiraHandler : IJira
    {
        public void ExportReport(List<StatusReportFormatter> records, string outputPath)
        {
            //TODO: need to change
            //using (var writer = new StreamWriter($"{outputPath}\\calculator.csv"))
            //{
            //	using (var csv = new CsvWriter(writer))
            //	{
            //		csv.WriteRecords(records);
            //	}
            //}
        }

        public List<StatusReportFormatter> MakeReportData(List<List<JiraReportFormatter>> JiraDataByDates)
        {
            var result = new List<StatusReportFormatter>();
            foreach (var dataInDate in JiraDataByDates)
            {
                StatusReportFormatter statusReport = new StatusReportFormatter
                {
                    Date = dataInDate.First().DataInDate,
                    Doing = dataInDate.DoingTickets(),
                    Closed = dataInDate.ClosedTickets(dataInDate.DataFromYesterday(JiraDataByDates)),
                    Created = dataInDate.CreatedTickets(),
                    Total = dataInDate.TotalTickets(),
                    Done = dataInDate.DoneTickets()
                };

                result.Add(statusReport);
            }

            return result;
        }

        public List<JiraReportFormatter> BuildJiraTableData(string filePath)
        {
            var result = new List<JiraReportFormatter>();
            //TODO: need to change
            //using (var reader = new StreamReader(filePath))
            //{
            //	DateTime dateData = filePath.GetDateByFileName();
            //	using (var csv = new CsvReader(reader))
            //	{
            //		result = csv.GetRecords<JiraReportFormatter>().ToList();
            //		Parallel.ForEach(result, r => r.DataInDate = dateData);
            //	}
            //}
            return result;
        }

        public List<ReportRawDataModel> BuildReportTableData(string filePath)
        {
            var result = new List<ReportRawDataModel>();
            
            using (var reader = new StreamReader(filePath))
            {
                DateTime dateData = filePath.GetDateByFileName();
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    result = csv.GetRecords<ReportRawDataModel>().ToList();
                    //Parallel.ForEach(result, r => r.DataInDate = dateData);
                }
            }
            return result;
        }

        public List<ReportRawDataModel> CombineDataWareHouse()
        {
            var files = Constants.JiraDataFolder.ReadFilesFromFolder(new DateTime(2020, 11, 01), new DateTime(2020, 11, 30));
            var result = new List<ReportRawDataModel>();

            foreach (var filePath in files)
            {
                result.AddRange(BuildReportTableData(filePath));
            }

            return result;
        }

        public void Read(string filePath, string[] specificSheetName)
        {
            throw new NotImplementedException();
        }

        public List<JiraReportFormatter> Download(string url)
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
    }
}
