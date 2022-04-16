using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Armoury_V2.Core
{
    public static class Log
    {
        public enum LogEvent
        {
            Info, Success, Warning, Error, Critical
        }
        //yyyy-MM-dd
        //private static readonly string LogSession = DateTime.Now.ToString("H:mm:ss");
        
        public static void Write(LogEvent level, string Message)
        {
            string Event = string.Empty;

            switch (level)
            {
                case LogEvent.Info:
                    Event = "INFO";
                    break;
                case LogEvent.Success:
                    Event = "SUCCESS";
                    break;
                case LogEvent.Warning:
                    Event = "WARNING";
                    break;
                case LogEvent.Error:
                    Event = "ERROR";
                    break;
                case LogEvent.Critical:
                    Event = "CRITICAL";
                    break;

            }

            Task.Run(async () =>
            {
                await FileHelper.WriteFile(FileHelper.GetLogFilePath(), $"[{DateTime.Now.ToString("H:mm:ss")}] {Event}: {Message}");
            });
        }
    }
}
