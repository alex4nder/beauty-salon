using BeautySalonApp.Data;
using BeautySalonApp.Exceptions;
using BeautySalonApp.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalonApp.Services
{
    public class ScheduleService
    {
        private readonly LocalDbContext _localContext;

        public ScheduleService()
        {
            var databaseService = Program.ServiceProvider.GetRequiredService<DatabaseService>();
            var currentBranchContext = Program.ServiceProvider.GetRequiredService<CurrentBranchContext>();
            _localContext = databaseService.GetLocalDbContext(currentBranchContext.BranchId);
        }

        public List<Schedule>? GetEmployeeSchedule(Guid employeeId)
        {
            var schedules = _localContext.Schedules
                .Where(s => s.EmployeeId == employeeId)
                .OrderByDescending(s => s.Date)
                .ToList();

            return schedules.Any() ? schedules : null;
        }

        public Schedule GetScheduleById(Guid scheduleId)
        {
            return _localContext.Schedules.Find(scheduleId);
        }

        public void AddSchedule(Schedule schedule)
        {
            var existingSchedule = _localContext.Schedules
                .Any(s => s.EmployeeId == schedule.EmployeeId && s.Date == schedule.Date);

            if (existingSchedule)
            {
                throw new ScheduleDateConflictException();

            }
            _localContext.Schedules.Add(schedule);
            _localContext.SaveChanges();
        }

        public void UpdateSchedule(Schedule schedule)
        {
            var existingSchedule = _localContext.Schedules.Find(schedule.Id);
            if (existingSchedule != null)
            {
                bool dateConflict = _localContext.Schedules
                    .Any(s => s.EmployeeId == existingSchedule.EmployeeId && s.Date == schedule.Date && s.Id != existingSchedule.Id);
                if (dateConflict)
                {
                    throw new ScheduleDateConflictException();
                }
                existingSchedule.Date = schedule.Date;
                existingSchedule.StartTime = schedule.StartTime;
                existingSchedule.EndTime = schedule.EndTime;
                _localContext.SaveChanges();
            }
        }

        public void RemoveSchedule(Guid scheduleId)
        {
            var schedule = _localContext.Schedules.Find(scheduleId);
            if (schedule != null)
            {
                _localContext.Schedules.Remove(schedule);
                _localContext.SaveChanges();
            }
        }
    }
}
