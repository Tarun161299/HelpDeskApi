//-----------------------------------------------------------------------
// <copyright file="DashboardCount.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------
namespace HelpDeskAPI.Data.Abstractions.Models;
public class DashboardCount
{
    public int? Total { get; set; }
    public int? Completed { get; set; }  
    public int? Return { get; set; }
    public int? Accepted { get; set; }
    public int? Closed { get; set; }
    public int? Submitted { get; set; }

    public int? Reject { get; set; }

    public int? Pending { get; set; }

    public int? Discard { get; set; }

    public int? TotalTicket { get; set; }
    public int? Assigned { get; set; }
    public int? UnAssigned { get; set; }

    public int? Progress { get; set; }
    public int? TaskClosed { get; set; }
    public int? NeedClarification { get; set; }
    public int? AlreadyCompleted { get; set; }
    public int? TkReturn { get; set; }

    public int? NeedHelp { get; set; }

    public List<DashboardCount> StatusDetail { get; set; }
}