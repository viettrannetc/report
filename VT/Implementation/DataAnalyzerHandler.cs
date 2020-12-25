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
using VT.Models.Monthly.KPIs;

namespace VT.Implementation
{
    /// <summary>
    /// https://joshclose.github.io/CsvHelper/getting-started
    /// </summary>
    public class DataAnalyzerHandler : IDataAnalyzer
    {
        public List<TicketCleanDataModel> CollectTicketData(List<ReportRawDataModel> resource)
        {
            var sortedData = resource
                .GroupBy(r => r.IssueKey)
                .ToDictionary(r => r.Key, r => r.ToList());

            var result = new List<TicketCleanDataModel>();
            foreach (var item in sortedData)
            {
                result.Add(new TicketCleanDataModel(item.Key, item.Value));
            }

            return result;
        }

        public KPI002ViewModel GenerateKPI002(KPIRequestModel request)
        {
            var result = new KPI002ViewModel(request);
            return result;
        }

        public KPI003ViewModel GenerateKPI003(KPIRequestModel request)
        {
            var result = new KPI003ViewModel(request);
            return result;
        }

        public KPI004ViewModel GenerateKPI004(KPIRequestModel request)
        {
            var result = new KPI004ViewModel(request);
            return result;
        }
    }
}
