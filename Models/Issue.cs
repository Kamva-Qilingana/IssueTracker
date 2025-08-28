using System;
using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Models
{
    public enum IssueStatus { Open, InProgress, Resolved, Closed }
    public enum IssuePriority { Low, Medium, High, Critical }

    public class Issue
    {
        [Key]
        public int IssueId { get; set; }

       

        [StringLength(4000)]
        public string? Description { get; set; }

        [Display(Name = "Status")]
        public IssueStatus Status { get; set; } /*= IssueStatus.Open;*/

        [Display(Name = "Priority")]
        public IssuePriority Priority { get; set; } /*= IssuePriority.Medium;*/

        [Display(Name = "Created"), DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Updated"), DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }

        // 🔗 Foreign key to IssueType
        [Display(Name = "Issue Type")]
        public int IssueTypeId { get; set; }

        public IssueType? IssueType { get; set; }   // Navigation property
    }
}
