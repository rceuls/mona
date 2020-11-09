using System;
using System.ComponentModel.DataAnnotations;

namespace Mona.Data
{
    public class ReportLine
    {
        public long Id { get; set; }
        public long ReportId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(4096)]
        public string Description { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(1024)]
        public string Location { get; set; }
        
        [Required]
        [MinLength(2)]
        [MaxLength(1024)]public string Responsible { get; set; }

        [Required]
        public DateTimeOffset TargetDate { get; set; }

        [Required]
        public string ImageUrl { get; set; }


    }
}