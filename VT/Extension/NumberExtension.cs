using VT.Common;
using VT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VT.Extension
{
    public static class NumberExtension
    {
		public static string Take2LastDigits(this int number)
		{
			return number.ToString().Substring(2, 2);
		}

		public static decimal ConvertToSP(this int originalEstimate)
		{
			return originalEstimate / 3600 / 8;
		}
	}
}
