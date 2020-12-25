using System.Collections.Generic;

namespace VT.Models.Monthly.KPIs
{
	public class KPI005ViewModel
	{
		public KPI005ViewModel()
		{
			Data = new List<KPI005Model>();
		}
		public string Title { get { return "KPI005 - User Story Points per developer"; } }
		public List<KPI005Model> Data { get; set; }
	}

	public class KPI005Model
	{
		public string Month { get; set; }
		public string FTEStoryPointsCompletedPerMonth { get; set; }
		public string AllocatedStoryPoints { get; set; }
		public string FTEBaseline { get; set; }
		public string FTEMinimunBaseline { get; set; }
	}
}
