using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Mona.Data
{
    public class Report
    {
        public long Id { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string ReportedBy { get; set; }
        [Required]
        public DateTimeOffset CreatedOn { get; set; }
        public IEnumerable<ReportLine> ReportLines { get; set; }
    }

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


    }

    public class ReportService
    {
        public Task<IEnumerable<Report>> GetAllReportsAsync()
        {
            return Task.FromResult(new List<Report>
            {
                new Report() { Id = 1, CreatedOn = DateTimeOffset.UtcNow, ReportedBy = "rceuls@gmail.com"},
                new Report() { Id = 2, CreatedOn = DateTimeOffset.UtcNow.AddDays(-1), ReportedBy = "rceuls@gmail.com"},

            } as IEnumerable<Report>);
        }

        public Task<IEnumerable<ReportLine>> GetReportLinesByReportIdAsync(long reportId)
        {
            return Task.FromResult(new List<ReportLine>
            {
                new ReportLine()
                {
                    Description = "no", Location = "yes", Id = 1, ReportId = reportId, Responsible = "Jef",
                    TargetDate = DateTimeOffset.UtcNow.AddDays(1)
                },
                new ReportLine()
                {
                    Description = "yes", Location = "no", Id = 1, ReportId = reportId, Responsible = "Jos",
                    TargetDate = DateTimeOffset.UtcNow.AddDays(2)
                },
                new ReportLine()
                {
                    Description = "could be", Location = "maybe", Id = 1, ReportId = reportId, Responsible = "Jan",
                    TargetDate = DateTimeOffset.UtcNow.AddDays(3)
                },
                new ReportLine()
                {
                    Description = "dfjd ", Location = "could be", Id = 1, ReportId = reportId, Responsible = "Joris",
                    TargetDate = DateTimeOffset.UtcNow.AddDays(4)
                },

            } as IEnumerable<ReportLine>);
        }
    }
}
