using VT.Model;
using System.Collections.Generic;
using System;
using VT.Models.Monthly.KPIs;

namespace VT.Interface
{
    public interface IDataAnalyzer
    {
        List<TicketCleanDataModel> CollectTicketData(List<ReportRawDataModel> resource);
        KPI002ViewModel GenerateKPI002(KPIRequestModel request);
        KPI003ViewModel GenerateKPI003(KPIRequestModel request);
        KPI004ViewModel GenerateKPI004(KPIRequestModel request);
    }
}
