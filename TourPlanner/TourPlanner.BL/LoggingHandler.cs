using System.Runtime.CompilerServices;

namespace TourPlanner.BL
{
    public class LoggingHandler
    {
        public static log4net.ILog GetLogger([CallerFilePath] string filename = "")
        {
            return log4net.LogManager.GetLogger(filename);
        }
    }
}