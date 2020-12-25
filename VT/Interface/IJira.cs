using VT.Model;
using System.Collections.Generic;

namespace VT.Interface
{
	public interface IJira
	{
		/// <summary>
		/// Download csv data file from Jira
		/// </summary>
		/// <param name="url">JIRA filter</param>
		/// <returns></returns>
		List<JiraReportFormatter> Download(string url);
		List<ReportRawDataModel> CombineDataWareHouse();
	}
}
