using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Models
{
    public class IssueType
    {
        [Key]
        public int IssueTypeId { get; set; }

        [Required(ErrorMessage = "Issue name is required")]
        [StringLength(100)]
        public string IssueName { get; set; }
    }
}

