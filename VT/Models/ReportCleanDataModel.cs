﻿using System;
using System.Collections.Generic;
using System.Linq;
using VT.Common;
using VT.Common.Enum;
using VT.Extension;

namespace VT.Model
{
    public class ReportDataResponseModel
    {
        public ReportDataResponseModel()
        {
            TicketsData = new List<TicketCleanDataModel>();
        }

        public List<TicketCleanDataModel> TicketsData { get; set; }
    }

    public class TicketCleanDataModel
    {
        private List<ReportRawDataModel> _ticketHistory = new List<ReportRawDataModel>();
        public string TicketNumber;
        public decimal FTE;

        /// <summary>
        /// Created when?
        /// <para>Resolved when?                      </para>
        /// <para>ticket original point?              </para>
        /// <para>has the ticket passed testing?      </para>
        /// <para>who did involve in development?     </para>
        /// <para>is ticket get returned at review?   </para>
        /// <para>who did involve in review?          </para>
        /// <para>is ticket get returned at QA?       </para>
        /// <para>who did involve in testing?         </para>
        /// <para>Is ticket defect?                   </para>
        /// <para>Is ticket CR?                       </para>
        /// <para>Did create by auto team?            </para>
        /// <para>Time to transition between stages   </para>
        /// <para>Ticket on time?                     </para>
        /// </summary>
        public TicketCleanDataModel(string ticketNumber, List<ReportRawDataModel> history)
        {
            TicketNumber = ticketNumber;
            _ticketHistory = history.OrderByDescending(h => h.PulledDataAt).ToList();
        }

        //Created when?
        public DateTime Created
        {
            get
            {
                return _ticketHistory.First().Created.ToDate();
            }
        }

        //Resolved when?
        public DateTime? Resolved
        {
            get
            {
                if (string.IsNullOrEmpty(_ticketHistory.First().Resolution))
                {
                    return null;
                }

                return _ticketHistory.Last(th => !string.IsNullOrEmpty(th.Resolution)).PulledDataAt;
            }
        }

        //ticket original point?
        public decimal StoryPoint
        {
            get
            {
                return _ticketHistory.First().OriginalEstimate / 3600 / 8;
            }
        }

        //ticket original point?
        public List<Tuple<DateTime, decimal>> StoryPointHistory
        {
            get
            {
                var result = new List<Tuple<DateTime, decimal>>();
                foreach (var item in _ticketHistory)
                {
                    result.Add(new Tuple<DateTime, decimal>(item.PulledDataAt, item.OriginalEstimate / 3600 / 8));
                }

                return result;
            }
        }

        //has the ticket passed testing?
        public bool PassedTesting
        {
            get
            {
                return _ticketHistory.First().EnumStatus.IsPassedTesting();
            }
        }

        public List<Tuple<DateTime, bool>> PassedTestingHistory
        {
            get
            {
                var result = new List<Tuple<DateTime, bool>>();
                foreach (var item in _ticketHistory)
                {
                    result.Add(new Tuple<DateTime, bool>(item.PulledDataAt, item.EnumStatus.IsPassedTesting()));
                }

                return result;
            }
        }


        #region In analysis?
        private List<ReportRawDataModel> InAnalysis
        {
            get
            {
                var analysisPhase = new List<ReportRawDataModel>();

                foreach (var currentIndexItem in _ticketHistory.OrderBy(th => th.PulledDataAt))
                {
                    if (currentIndexItem.EnumStatus.IsInAnalysis())
                    {
                        analysisPhase.Add(currentIndexItem);
                    }

                    if (currentIndexItem.EnumStatus.IsInDevelopment() && analysisPhase.Any() && analysisPhase.Last().EnumStatus.IsInAnalysis())
                    {
                        analysisPhase.Add(currentIndexItem);
                    }
                }

                return analysisPhase.OrderBy(th => th.PulledDataAt).ToList();
            }
        }

        public decimal InAnalysisTime
        {
            get
            {
                return InAnalysis.TotalTime();
            }
        }

        public List<Tuple<string, decimal>> InAnalysisAssignees
        {
            get
            {
                return InAnalysis.AssigneesHistory();
            }
        }
        #endregion

        #region In development? 
        public List<ReportRawDataModel> AssigneesInDevelopment
        {
            get
            {
                var analysisPhase = new List<ReportRawDataModel>();

                foreach (var currentIndexItem in _ticketHistory.OrderBy(th => th.PulledDataAt))
                {
                    if (currentIndexItem.EnumStatus.IsInDevelopment())
                    {
                        analysisPhase.Add(currentIndexItem);
                    }

                    if (currentIndexItem.EnumStatus.IsInReview() && analysisPhase.Any() && analysisPhase.Last().EnumStatus.IsInDevelopment())
                    {
                        analysisPhase.Add(currentIndexItem);
                    }

                    if (currentIndexItem.EnumStatus.IsInReadyForDev() && analysisPhase.Any() && analysisPhase.Last().EnumStatus.IsInDevelopment())
                    {
                        analysisPhase.Add(currentIndexItem);
                    }
                }

                return analysisPhase.OrderBy(th => th.PulledDataAt).ToList();
            }
        }

        public decimal InDevelopmentTime
        {
            get
            {
                return AssigneesInDevelopment.TotalTime();
            }
        }

        public List<Tuple<string, decimal>> InDevelopmentAssignees
        {
            get
            {
                return AssigneesInDevelopment.AssigneesHistory();
            }
        }
        #endregion

        #region In Review? 
        public List<ReportRawDataModel> InReview
        {
            get
            {
                var analysisPhase = new List<ReportRawDataModel>();

                foreach (var currentIndexItem in _ticketHistory.OrderBy(th => th.PulledDataAt))
                {
                    if (currentIndexItem.EnumStatus.IsInReview())
                    {
                        analysisPhase.Add(currentIndexItem);
                    }

                    if (currentIndexItem.EnumStatus.IsInReadyForDev() && analysisPhase.Any() && analysisPhase.Last().EnumStatus.IsInReview())
                    {
                        analysisPhase.Add(currentIndexItem);
                    }

                    if (currentIndexItem.EnumStatus.IsInReadyForTesting() && analysisPhase.Any() && analysisPhase.Last().EnumStatus.IsInReview())
                    {
                        analysisPhase.Add(currentIndexItem);
                    }
                }

                return analysisPhase.OrderBy(th => th.PulledDataAt).ToList();
            }
        }

        public decimal InReviewTime
        {
            get
            {
                return InReview.TotalTime();
            }
        }

        public List<Tuple<string, decimal>> InReviewAssignees
        {
            get
            {
                return InReview.AssigneesHistory();
            }
        }
        #endregion

        #region In QA? 
        public List<ReportRawDataModel> InWaitingForQA
        {
            get
            {
                var analysisPhase = new List<ReportRawDataModel>();

                foreach (var currentIndexItem in _ticketHistory.OrderBy(th => th.PulledDataAt))
                {
                    if (currentIndexItem.EnumStatus.IsInReadyForTesting())
                    {
                        analysisPhase.Add(currentIndexItem);
                    }

                    if (currentIndexItem.EnumStatus.IsInTesting() && analysisPhase.Any() && analysisPhase.Last().EnumStatus.IsInReadyForTesting())
                    {
                        analysisPhase.Add(currentIndexItem);
                    }

                    if (currentIndexItem.EnumStatus.IsInReadyForDev() && analysisPhase.Any() && analysisPhase.Last().EnumStatus.IsInReadyForTesting())
                    {
                        analysisPhase.Add(currentIndexItem);
                    }
                }

                return analysisPhase.OrderBy(th => th.PulledDataAt).ToList();
            }
        }

        public decimal InQATime
        {
            get
            {
                return InWaitingForQA.TotalTime();
            }
        }

        public List<Tuple<string, decimal>> InQAAssignees
        {
            get
            {
                return InWaitingForQA.AssigneesHistory();
            }
        }
        #endregion

        #region In QA Testing? 
        public List<ReportRawDataModel> InQATesting
        {
            get
            {
                var analysisPhase = new List<ReportRawDataModel>();

                foreach (var currentIndexItem in _ticketHistory.OrderBy(th => th.PulledDataAt))
                {
                    if (currentIndexItem.EnumStatus.IsInTesting())
                    {
                        analysisPhase.Add(currentIndexItem);
                    }

                    if (currentIndexItem.EnumStatus.IsPassedTesting() && analysisPhase.Any() && analysisPhase.Last().EnumStatus.IsInTesting())
                    {
                        analysisPhase.Add(currentIndexItem);
                    }

                    if (currentIndexItem.EnumStatus.IsInReadyForDev() && analysisPhase.Any() && analysisPhase.Last().EnumStatus.IsInTesting())
                    {
                        analysisPhase.Add(currentIndexItem);
                    }
                }

                return analysisPhase.OrderBy(th => th.PulledDataAt).ToList();
            }
        }

        public decimal InQATestingTime
        {
            get
            {
                return InQATesting.TotalTime();
            }
        }

        public List<Tuple<string, decimal>> InQATestingAssignees
        {
            get
            {
                return InQATesting.AssigneesHistory();
            }
        }
        #endregion

        #region Returned at review? 
        public List<DateTime> ReturnedAtReviewStageHistory
        {
            get
            {
                var result = new List<DateTime>();

                for (int i = _ticketHistory.Count() - 1; i >= 0; i--)
                {
                    var currentStatus = _ticketHistory[i].EnumStatus;

                    var nextStatus = i - 1 > 0
                        ? _ticketHistory[i - 1].EnumStatus
                        : Common.Enum.JiraStatus.Unknown;

                    if (currentStatus ==  Common.Enum.JiraStatus.InReview && (nextStatus == JiraStatus.ReadyForDev || nextStatus == JiraStatus.InDevelopment))
                        result.Add(_ticketHistory[i - 1].PulledDataAt);
                }

                return result;
            }
        }
        #endregion

        #region Returned at QA? 
        public List<DateTime> ReturnedAtQAStageHistory
        {
            get
            {
                var result = new List<DateTime>();

                for (int i = _ticketHistory.Count() - 1; i >= 0; i--)
                {
                    var currentStatus = _ticketHistory[i].EnumStatus;

                    var nextStatus = i - 1 > 0
                        ? _ticketHistory[i - 1].EnumStatus
                        : JiraStatus.Unknown;

                    if (currentStatus == JiraStatus.InTesting && (nextStatus == JiraStatus.ReadyForDev || nextStatus == JiraStatus.InDevelopment))
                        result.Add(_ticketHistory[i - 1].PulledDataAt);
                }

                return result;
            }
        }
        #endregion

        /// <summary>
        /// Is ticket defect OR CR?
        /// </summary>
        public string TicketType
        {
            get
            {
                return _ticketHistory.First().IssueType;
            }
        }

        /// <summary>
        /// //Did create by auto team?
        /// </summary>
        public bool IsCreatedByAutoTeam
        {
            get
            {
                return _ticketHistory.First().Labels.Contains("qaleak");
            }
        }


        /// <summary>
        /// Ticket on time?
        /// </summary>
        public bool IsOnTime
        {
            get
            {

                return (_ticketHistory.First().OriginalEstimate / 3600) > (InDevelopmentTime + InReviewTime);
            }
        }

        //Optionallll
        //Time to transition between stages
    }
}
