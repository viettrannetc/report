using VT.Model;
using System.Collections.Generic;
using System;

namespace VT.Interface
{
	public interface IDataCollector
	{
		List<ReportRawDataModel> BuildDrawData(string folderPath, DateTime from, DateTime to);
	}
}
