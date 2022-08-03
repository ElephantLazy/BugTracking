using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace BugTrackingSystem.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public virtual IssueType IssueType{ get; set; }
        public virtual Priority Priority { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string  Reporter { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ResolveDate { get; set; }
        public string Asignee { get; set; }
        public string Status { get; set; }
        public string AsigneeDescription { get; set; }

    }
    public enum IssueType
    {
        Bug=1,
        Feature=2,
        Failure=3,
        Improvement=4

    }
    public enum Priority
    {
        Blocker = 1,
        Critical = 2,
        Major = 3,
        Minor=4,
        Trivial=5

    }
    
}