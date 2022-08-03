using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTrackingSystem.Models
{
    public class IssueUserViewModel
    {
        public int IssueId { get; set; }
        public string AssigneeId { get; set; }

        public string Statuss { get; set; }
        public Issue Issue { get; set; }

        public IEnumerable<ApplicationUser> User { get; set; }

        public string tamamlanmaOrani { get; set; }

    }

   

}