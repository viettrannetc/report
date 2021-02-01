using VT.Common;
using VT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VT.Extension
{
    public static class DateExtension
	{
		public static bool InTheSameDay(this DateTime date1, DateTime date2)
		{
			return date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day;
		}
	}
}
