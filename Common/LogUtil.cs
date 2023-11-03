namespace Common
{
    public class LogUtil
    {
        private static readonly string LOG_FILENAME_PATH_FORMAT = @".\ErrorReport\Log.txt";
        private string logFileName = string.Format(LOG_FILENAME_PATH_FORMAT, DateTime.Today.ToString("yyyyMMdd"));
        private string logFileDirectory = @".\ErrorReport";

        private static readonly string logFormat = @"{0}  :  '{1}'  at  '{2}' StackTrace:'{3}'";

        public void WriteLog(string logMessage)
        {
            if (!Directory.Exists(logFileDirectory))
            {
                Directory.CreateDirectory(logFileDirectory);
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(logFileName, true))
            {
                file.WriteLine(logMessage);
            }
        }

        public void WriteLog(Exception ex)
        {
            string logMessage = string.Format(logFormat, DateTime.Now.ToString("yyyyMMdd HH:mm:ss"), "UnauthorizedAccessException" + ex.Message, ex.Source, ex.StackTrace);
            WriteLog(logMessage);
        }
    }
}
