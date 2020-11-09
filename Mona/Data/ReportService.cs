using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace Mona.Data
{
    public class ReportService
    {
        private readonly AwsS3Uploader _uploader;

        public ReportService(AwsS3Uploader uploader)
        {
            _uploader = uploader;
        }

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

        public async Task Persist(ReportLine toPersist, IBrowserFile fileData)
        {
            var resizedFile = await fileData.RequestImageFileAsync("png", 800, 600);
            var fileName = $"{toPersist.ReportId.ToString().PadLeft(3, '0')}/{Guid.NewGuid()}.png";
            toPersist.ImageUrl = await _uploader.UploadFileAsync(resizedFile.OpenReadStream(2_000_000), fileName);
        }
    }
}
