using System.Collections.Generic;

namespace IssueTracker.Models
{
    public class IssueListViewModel
    {
        public IEnumerable<Issue> Issues { get; set; } = new List<Issue>();
        public Issue NewIssue { get; set; } = new Issue();
        public IEnumerable<IssueType> IssueTypes { get; set; } = new List<IssueType>();
    }
}
