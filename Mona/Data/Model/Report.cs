using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mona.Data.Model
{
    public class Report
    {
        [Key]
        public long Id { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string ReportedBy { get; set; }
        [Required]
        public DateTimeOffset CreatedOn { get; set; }
        public IEnumerable<ReportLine> ReportLines { get; set; }
    }
}