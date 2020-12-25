using VT.Common;
using VT.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VT.Extension
{
    public static class ReportRawDataModelExtension
    {
        public static decimal TotalTime(this List<ReportRawDataModel> dataInSpecificStatus)
        {
            decimal result = 0;
            for (int i = 0; i < dataInSpecificStatus.Count() - 1; i++)
            {
                result += (decimal)((dataInSpecificStatus[i + 1].PulledDataAt - dataInSpecificStatus[i].PulledDataAt).TotalSeconds);
            }

            return result;
        }

        public static List<Tuple<string, decimal>> AssigneesHistory(this List<ReportRawDataModel> dataInSpecificStatus)
        {
            var result = new List<Tuple<string, decimal>>();

            var assignees = dataInSpecificStatus.Select(t => t.Assignee).Distinct().ToList();

            foreach (var assignee in assignees)
            {
                decimal totalTime = 0;
                for (int i = 0; i < dataInSpecificStatus.Count() - 1; i++)
                {
                    if (dataInSpecificStatus[i].Assignee == assignee
                        || (i >= 1 && dataInSpecificStatus[i - 1].Assignee == assignee && dataInSpecificStatus[i].Assignee != assignee))
                        totalTime += (decimal)((dataInSpecificStatus[i + 1].PulledDataAt - dataInSpecificStatus[i].PulledDataAt).TotalSeconds);
                }

                result.Add(new Tuple<string, decimal>(assignee, totalTime));
            }

            return result;
        }
    }
}
