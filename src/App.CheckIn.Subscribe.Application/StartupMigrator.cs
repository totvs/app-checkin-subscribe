using System.Threading;
using System.Threading.Tasks;
using App.CheckIn.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace AppCheckInSubscribe.Application
{
    /// <summary>
    /// Background service responsible for executing database migrations at the startup of the application
    /// </summary>
    public class StartupMigrator : BackgroundService
    {
        private readonly AppCheckInDbContext _appCheckInDbContext;

        public StartupMigrator(AppCheckInDbContext appCheckInDbContext)
        {
            _appCheckInDbContext = appCheckInDbContext;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return _appCheckInDbContext.Database.MigrateAsync();
        }
    }
}
