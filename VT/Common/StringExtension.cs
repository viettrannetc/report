﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace VT.Common
{
	public static class StringExtension
	{
		public static DateTime ToDate(this string dateString, string dateFormat = "dd-MM-yyyy-HH-mm-ss")
		{
			try
			{
				//var dateOfFile = new DateTime();

				//if (DateTime.TryParse(dateString, CultureInfo.InvariantCulture, DateTimeStyles. dateFormat, out dateOfFile))
				//    //var dateOfFile = DateTime.ParseExact(dateString, dateFormat, CultureInfo.InvariantCulture);
				//    return dateOfFile;
				//else
				//    return DateTime.ParseExact(dateString, "dd/MMM/yy HH:mm tt", CultureInfo.InvariantCulture);

				if (dateString.Length == "dd-MM-yyyy-HH-mm-ss".Length)
					dateFormat = "dd-MM-yyyy-HH-mm-ss";
				//if (dateString.Length == "dd/MMM/yy HH:mm".Length)
				//	dateFormat = "dd/MMM/yy HH:mm tt";
				//if (dateString.Length == "dd/MMM/yy H:mm".Length)
				//	dateFormat = "dd/MMM/yy H:mm tt";
				if (dateString.Length == "dd/MMM/yy hh:mm tt".Length)
					dateFormat = "dd/MMM/yy hh:mm tt";
				if (dateString.Length == "dd/MMM/yy h:mm tt".Length)
					dateFormat = "dd/MMM/yy h:mm tt";

				return DateTime.ParseExact(dateString, dateFormat, CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				return DateTime.Parse(dateString);
			}
		}

		public static DateTime GetDateByFileName(this string fileName)
		{
			var dateOfFileString = fileName.Split("Report-AdStream-")[1].Split(".csv")[0];
			var dateOfFile = dateOfFileString.ToDate();
			return dateOfFile;
		}

		public static List<string> ReadFilesFromFolder(this string folder, DateTime? from, DateTime? to)
		{
			var result = new List<string>();
			foreach (var fileName in Directory.GetFiles(folder, "Report-AdStream-*"))
			{
				var dateOfFile = fileName.GetDateByFileName();
				if (from.HasValue && from <= dateOfFile && to.HasValue && dateOfFile <= to)
				{
					result.Add(fileName);
				}
			}

			return result;
		}
	}
}
