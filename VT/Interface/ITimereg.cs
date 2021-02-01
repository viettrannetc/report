using VT.Models.Timereg;

namespace VT.Interface
{
	public interface ITimereg
	{
		TimeregExcelModel Read(string filePath);		
	}
}
