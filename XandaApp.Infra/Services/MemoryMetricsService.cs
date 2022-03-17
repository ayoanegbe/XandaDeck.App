using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XandaApp.Infra.Services
{
    public class MemoryMetricsService
    {
        public class MemoryMetrics
        {
            public double Total;
            public double Used;
            public double Free;
        }

        public class MemoryMetricsClient
        {
            public MemoryMetrics GetMetrics()
            {
                MemoryMetrics metrics;
               
                metrics = GetWindowsMetrics();
                
                return metrics;
            }

            private MemoryMetrics GetWindowsMetrics()
            {
                var output = string.Empty;
                var info = new ProcessStartInfo
                {
                    FileName = "wmic",
                    Arguments = "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value",
                    RedirectStandardOutput = true
                };

                using (var process = Process.Start(info))
                {
                    output = process.StandardOutput.ReadToEnd();
                }

                var lines = output.Trim().Split('\n');
                var freeMemoryParts = lines[0].Split('=', (char)StringSplitOptions.RemoveEmptyEntries);
                var totalMemoryParts = lines[1].Split('=', (char)StringSplitOptions.RemoveEmptyEntries);
                
                var metrics = new MemoryMetrics
                {
                    Total = Math.Round(double.Parse(totalMemoryParts[1]) / 1024, 0),
                    Free = Math.Round(double.Parse(freeMemoryParts[1]) / 1024, 0)
                };
                metrics.Used = metrics.Total - metrics.Free;

                return metrics;
            }
        }
    }
}
