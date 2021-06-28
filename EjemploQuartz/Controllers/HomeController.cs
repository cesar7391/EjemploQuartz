using EjemploQuartz.Jobs;
using EjemploQuartz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploQuartz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScheduler scheduler;
        private readonly IConfiguration configuration;

        public HomeController(IScheduler scheduler, IConfiguration configuration)
        {
            this.scheduler = scheduler;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> StartBDJob()
        {
            IJobDetail jobBD = JobBuilder.Create<InsertData>()
                .WithIdentity("JobBD", "QuartzEjemplo")
                .StoreDurably()
                .Build();

            await scheduler.AddJob(jobBD, true);

            ITrigger triggerBD = TriggerBuilder.Create()
                .ForJob(jobBD)
                .StartNow()
                .WithDailyTimeIntervalSchedule(x => x
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(8, 0))
                    .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(13, 0))
                    .OnDaysOfTheWeek(DayOfWeek.Monday, DayOfWeek.Saturday)
                    .WithIntervalInSeconds(5))
                //.WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(5)).RepeatForever())
                .Build();

            await scheduler.ScheduleJob(triggerBD);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> StartSimpleJob()
        {
            //InsertData pruebaIn = new InsertData(configuration);
            //pruebaIn.InsertarDatos("DesdeVS", "DescripcionVS", 1500.56);    

            IJobDetail job = JobBuilder.Create<SimpleJob>()
                //.UsingJobData("username","cesar73")
                .WithIdentity("Simple_Job", "Ejemplo_Quartz")
                .StoreDurably()
                .Build();

            //USAR UN MODELO
            job.JobDataMap.Put("Producto", new ProductoModel("Producto Desde Quartz", "Descripción desde Quartz", 1500));
            //job.JobDataMap.Put("user", new UserModel("cesar73","123456789"));

            //Guardar el Job
            await scheduler.AddJob(job, true);

            ITrigger trigger = TriggerBuilder.Create()
                .ForJob(job)
                //.UsingJobData("triggerparam", "Simple trigger 1 Parameter")
                //.WithIdentity("testtrigger", "quartzexamples")
                .StartNow()
                //=== POR INTERVALO ===
                .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(5)).RepeatForever())
                //=== PARA ESPECIFICAR ENTRE QUE TIEMPO DEL DIA SE USARÁ ===
                /*
                .WithDailyTimeIntervalSchedule(x => x
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(13, 0))
                    .EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(14, 0))
                    .OnDaysOfTheWeek(DayOfWeek.Friday, DayOfWeek.Saturday)
                    .WithIntervalInSeconds(5))
                */
                //=== PARA USAR INTERVALO DE CALENDARIO ===
                /*
                .WithCalendarIntervalSchedule(x=>x.WithIntervalInDays(1)
                    .PreserveHourOfDayAcrossDaylightSavings(true)
                    .SkipDayIfHourDoesNotExist(true))
                */
                .Build();            
                
            await scheduler.ScheduleJob(trigger);

            //=== PARA USAR OTRO TRIGGER ===
            /*
            ITrigger trigger2 = TriggerBuilder.Create()
                .ForJob(job)
                .UsingJobData("triggerparam", "Simple trigger 2 Parameter")
                .WithIdentity("testtrigger2", "quartzexamples")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(1).WithRepeatCount(5))
                .Build();
            await scheduler.ScheduleJob(trigger2);
            */

            //await scheduler.ScheduleJob(job, trigger);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
