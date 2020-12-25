using VT.Common;
using VT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VT.Common.Enum;

namespace VT.Extension
{
    public static class StringExtension
    {
        public static bool IsPassedTesting(this JiraStatus currentStatus)
        {
            return (int)currentStatus >= (int)JiraStatus.PassedTesting;
        }

        public static bool IsInDevelopment(this JiraStatus currentStatus)
        {
            return (int)currentStatus == (int)JiraStatus.PassedTesting;
        }

        public static bool IsInAnalysis(this JiraStatus currentStatus)
        {
            return (int)currentStatus == (int)JiraStatus.InAnalysis;
        }

        public static bool IsInReview(this JiraStatus currentStatus)
        {
            return (int)currentStatus == (int)JiraStatus.InReview;
        }

        public static bool IsInReadyForDev(this JiraStatus currentStatus)
        {
            return (int)currentStatus == (int)JiraStatus.ReadyForDev;
        }

        public static bool IsInReadyForTesting(this JiraStatus currentStatus)
        {
            return (int)currentStatus == (int)JiraStatus.ReadyForTesting;
        }

        public static bool IsInTesting(this JiraStatus currentStatus)
        {
            return (int)currentStatus == (int)JiraStatus.InTesting;
        }

        public static JiraStatus ParseToJiraEnumStatus(this string currentStringJiraStatus)
        {
            switch (currentStringJiraStatus)
            {
                case "Open":
                    return JiraStatus.Open;
                case "To Do":
                    return JiraStatus.Todo;
                case "In Analysis":
                    return JiraStatus.InAnalysis;
                case "Ready for Dev":
                    return JiraStatus.ReadyForDev;
                case "In Development":
                    return JiraStatus.InDevelopment;
                case "In Review":
                    return JiraStatus.InReview;
                case "Ready for Testing":
                    return JiraStatus.ReadyForTesting;
                case "In Testing":
                    return JiraStatus.InTesting;
                case "Passed Testing":
                    return JiraStatus.PassedTesting;
                case "Done":
                    return JiraStatus.Done;
                case "On Preprod":
                    return JiraStatus.OnPreprod;
                case "On live":
                    return JiraStatus.OnLive;
                case "Closed":
                    return JiraStatus.Closed;
            }
            return JiraStatus.Unknown;
        }
    }
}
