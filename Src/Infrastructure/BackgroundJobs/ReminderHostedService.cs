using Application.Common.Interfaces;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Infrastructure.BackgroundJobs
{
    public class ReminderHostedService : JobActivator
    {
        private readonly ILogger<ReminderHostedService> _logger;
        private readonly IApplicationDbContext _applicationDbContext;

        public ReminderHostedService(ILogger<ReminderHostedService> logger, IApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public void Execute()
        {
            DateTime from = DateTime.Now.AddMinutes(30);
            DateTime to = from.AddMinutes(1);
            var entities = _applicationDbContext.TodoItems
                .Where(p => p.TimeTodo.Value > from && p.TimeTodo.Value < to)
                .AsNoTracking().ToList();

            foreach (var item in entities)
                _logger.LogInformation($"------------ Send Message For Reminder Todo with id \"{item.Id}\" in \"{item.TimeTodo.Value}\"");
        }
    }
}
