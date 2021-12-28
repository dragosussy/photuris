using NCrontab;
using photuris_backend.DbContext;

namespace photuris_backend.Utilities
{
    public class ExpiredSessionsCleanerJob : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        private readonly CrontabSchedule _schedule;
        private DateTime _nextRun;
        private string Schedule => "*/15 * * * * *"; //Runs every 10 seconds

        public ExpiredSessionsCleanerJob(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                if (DateTime.Now > _nextRun)
                {
                    await Process();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(2000, stoppingToken);
            } while (!stoppingToken.IsCancellationRequested);
            
        }

        private async Task Process()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repository = scope.ServiceProvider.GetService<Repository>();
                if (repository == null) throw new Exception("cleaning sessions exception: null DbContext");

                var expiredSessions = repository.Sessions.Where(s => s.ExpirationDate < DateTime.Now);
                foreach (var expiredSession in expiredSessions)
                    repository.Sessions.Remove(expiredSession);
                await repository.SaveChangesAsync();
            }
        }
    }
}
