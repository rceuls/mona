using Microsoft.EntityFrameworkCore;
using Mona.Data.Model;

namespace Mona.Data
{
    public class ReportDataContext : DbContext
    {
        public ReportDataContext(DbContextOptions config): base(config)
        {
        }
        public DbSet<Report> Reports { get; set; }
        public DbSet<ReportLine> ReportLines{ get; set; }
    }
}
