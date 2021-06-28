using EjemploQuartz.Models;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploQuartz.Jobs
{
    public class ScheduledJobs
    {
        public static void Start()
        {
            Console.WriteLine("\n========================================\n======== SCHEDULED PRINCIPAL ===========\n========================================");
            System.Diagnostics.Debug.WriteLine("\n========================================\n======== SCHEDULED PRINCIPAL ===========\n========================================");

            //IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<SimpleJob>().Build();
            job.JobDataMap.Put("Producto", new ProductoModel("Producto Desde Quartz", "Descripción desde Quartz", 1500.99));
            //job.JobDataMap.Put("Producto", new ProductoModel("Producto Desde Quartz", "Descripción desde Quartz", 999999999999999));

            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("Simple Job", "tarea 1")
            .WithDailyTimeIntervalSchedule(x => x
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(8, 0))
                    .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(20, 0))
                    .OnDaysOfTheWeek(DayOfWeek.Monday, DayOfWeek.Saturday)
                    .WithIntervalInSeconds(5))
            .Build();

            scheduler.ScheduleJob(job, trigger);           

        }
    }
}
