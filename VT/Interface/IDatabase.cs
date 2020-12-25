using VT.Model;
using System.Collections.Generic;

namespace VT.Interface
{
	public interface IDatabase
	{
		/// <summary>
		/// Download csv data file from Jira
		/// </summary>
		/// <param name="url">JIRA filter</param>
		/// <returns></returns>
		void Read();
		void Add();
		
	}
}
