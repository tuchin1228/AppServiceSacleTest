using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ScaleTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakeLoadController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(int seconds, int percentage)
        {
            FakeWorkload(seconds, percentage);

            return Ok($"Fake load of {percentage}% for {seconds} seconds");
        }

        // Method to simulate cpu load for a period of time        
        private void FakeWorkload(int seconds, int percentageCpu)
        {
            percentageCpu = Math.Max(100, percentageCpu);
            var end = DateTime.Now.AddSeconds(seconds);
            var percentage = percentageCpu;
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (DateTime.Now < end)
            {
                // Make the loop go on for "percentage" milliseconds then sleep the
                // remaining percentage milliseconds. So 40% utilization means work 40ms and sleep 60ms
                if (watch.ElapsedMilliseconds > percentage)
                {
                    System.Threading.Thread.Sleep(100 - percentage);
                    watch.Reset();
                    watch.Start();
                }
            }
        }
    }
}
