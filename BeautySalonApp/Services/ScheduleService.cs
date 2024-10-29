using BeautySalonApp.Data;
using BeautySalonApp.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

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
                .ToList();

            return schedules.Any() ? schedules : null;
        }

        public void AddSchedule(Schedule schedule)
        {
            _localContext.Schedules.Add(schedule);
            _localContext.SaveChanges();
        }

        public void UpdateSchedule(Schedule schedule)
        {
            var existingSchedule = _localContext.Schedules.Find(schedule.Id);
            if (existingSchedule != null)
            {
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
