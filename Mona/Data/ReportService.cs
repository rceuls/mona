using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Mona.Data.Model;

namespace Mona.Data
{
    public class ReportService
    {
        private readonly AzureBlobUploader _uploader;
        private readonly ReportDataContext _reportDataContext;

        public ReportService(AzureBlobUploader uploader, ReportDataContext reportDataContext)
        {
            _uploader = uploader;
            _reportDataContext = reportDataContext;
        }

        public Task<List<Report>> GetAllReportsAsync()
        {
            return _reportDataContext.Reports.ToListAsync();
        }

        public Task<List<ReportLine>> GetReportLinesByReportIdAsync(long reportId)
        {
            return _reportDataContext.ReportLines.Where(x => x.ReportId == reportId).ToListAsync();
        }

        public async Task Persist(ReportLine toPersist, IBrowserFile fileData)
        {
            var resizedFile = await fileData.RequestImageFileAsync("png", 800, 600);
            var fileName = $"{toPersist.ReportId.ToString().PadLeft(3, '0')}/{Guid.NewGuid()}.png";
            toPersist.ImageUrl = await _uploader.UploadFileAsync(resizedFile.OpenReadStream(2_000_000), fileName);
            await _reportDataContext.ReportLines.AddAsync(toPersist);
            await _reportDataContext.SaveChangesAsync();
        }

        [Authorize]
        public async Task<long> CreateNewReport(string reportedBy)
        {
            var report = new Report
            {
                CreatedOn = DateTimeOffset.UtcNow,
                ReportedBy = reportedBy
            };
            await _reportDataContext.AddAsync(report);
            await _reportDataContext.SaveChangesAsync();
            return report.Id;
        }
    }
}
