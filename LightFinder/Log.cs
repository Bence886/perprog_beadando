using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightFinder
{
    enum LogType { Console, File }
    enum LogLevel { Trace, Debug, Message, Error, Warning }
    static class Log
    {
        public static LogLevel CurrentLogLevel = LogLevel.Error;

        private static StreamWriter log = new StreamWriter("Log.txt", false);

        public static bool AllConsole = false;

        public static bool FileLog { get; set; } = true;

        public static void WriteLog(string message, LogType type, LogLevel level)
        {
            if (level >= CurrentLogLevel)
            {
                if (type == LogType.Console || AllConsole)
                {
                    Console.WriteLine(level + " : " + message);
                }
                if (FileLog)
                {
                    log.WriteLine(DateTime.Now.TimeOfDay + " : " + level + " : " + message);
                }
            }
        }

        public static void CloseWriter()
        {
            log.Close();
        }
    }
}
