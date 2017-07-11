using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace SampleLog
{
    public class Logger
    {
        public delegate void LogInformationCallback(string message, StatusImageIndex Status, string CompanyCode);
        public static LogInformationCallback MYCALLBACK = null;

        /// <summary>
        /// Local copy of the Logger instance
        /// </summary>
        private static NLog.Logger logger;

        public enum Level
        {
            /// <summary>
            /// Information Log
            /// </summary>
            Info,

            /// <summary>
            /// Warning Log
            /// </summary>
            Warning,

            /// <summary>
            /// Error Log
            /// </summary>
            Error
        }

        public enum Source
        {
            Settings,
            BatchInvoice
        }

        /// <summary>
        /// Checks if the Logger have been initialized and if not it will create new Event logger.
        /// </summary>
        /// <param name="selectedLogger">The Logger name to be selected - Loggers can be found in the NLog.config file</param>
        private static void Initialize(string selectedLogger)
        {
            ////if (!System.Diagnostics.EventLog.SourceExists("WMS Exporter"))
            ////{
            ////    System.Diagnostics.EventLog.CreateEventSource("WMS Exporter", "WMS Integration");
            ////}

            if (logger == null)
            {
                logger = NLog.LogManager.GetLogger(selectedLogger);
            }
            else if (logger.Name != selectedLogger)
            {
                logger = NLog.LogManager.GetLogger(selectedLogger);
            }

        }

        public enum StatusImageIndex
        {
            ERROR = 0,
            INFO = 1,
            WARNING = 2
        }

        public static void SetLogPath(string path)
        {

            NLog.Config.LoggingConfiguration config = NLog.LogManager.Configuration;

            //Change the NLog filename at runtime
            var target = (FileTarget)NLog.LogManager.Configuration.FindTargetByName("PDFFile");
            if (target != null)
                target.FileName = path;

            NLog.LogManager.Configuration = config;
        }

        public static string GetLogFilename()
        {
            string path = string.Empty;

            NLog.Config.LoggingConfiguration config = NLog.LogManager.Configuration;

            //Change the NLog filename at runtime
            var target = (FileTarget)NLog.LogManager.Configuration.FindTargetByName("PDFFile");
            if (target != null)
                path = System.IO.Path.GetFileName(target.FileName.ToString());

            return path;
        }

        /// <summary>
        /// Process / Error Logging specific to the Importer
        /// </summary>
        /// <param name="level">Severity Level - Info/Warning/Error</param>
        /// <param name="companyCode">Company Code</param>
        /// <param name="message">Message Content</param>
        /// <param name="exception">Exception - optional parameter</param>
        public static void LogMessage(Logger.Level level, string companyCode, string message, Exception exception = null)
        {
            StringBuilder log = new StringBuilder();

            log.Append(level.ToString().PadRight(8, ' '));
            log.Append(" | " + companyCode.PadRight(6, ' '));
            log.Append(" | " + message);

            switch (level)
            {
                case Logger.Level.Info:
                    Logger.AddInfo(log.ToString());
                    MakeCallBack(message, StatusImageIndex.INFO, companyCode);
                    break;
                case Logger.Level.Warning:
                    Logger.AddWarning(log.ToString());
                    MakeCallBack(message, StatusImageIndex.WARNING, companyCode);
                    break;
                case Logger.Level.Error:
                    Logger.AddError(log.ToString(), exception);
                    MakeCallBack(message, StatusImageIndex.ERROR, companyCode);
                    break;
            }
        }

        /// <summary>
        /// Process / Error Logging specific to the Importer
        /// </summary>
        /// <param name="level">Severity Level - Info/Warning/Error</param>
        /// <param name="companyCode">Company Code</param>
        /// <param name="message">Message Content</param>
        /// <param name="exception">Exception - optional parameter</param>
        public static void LogtoPDFLog(Logger.Level level, string companyCode, string message, Exception exception = null)
        {
            StringBuilder log = new StringBuilder();

            log.Append(level.ToString().PadRight(8, ' '));
            log.Append(" | " + companyCode.PadRight(6, ' '));
            log.Append(" | " + message);

            switch (level)
            {
                case Logger.Level.Info:
                    Logger.AddPDFInfo(log.ToString());
                    MakeCallBack(message, StatusImageIndex.INFO, companyCode);
                    break;
                case Logger.Level.Warning:
                    Logger.AddPDFWarning(log.ToString());
                    MakeCallBack(message, StatusImageIndex.WARNING, companyCode);
                    break;
                case Logger.Level.Error:
                    Logger.AddPDFError(log.ToString(), exception);
                    MakeCallBack(message, StatusImageIndex.ERROR, companyCode);
                    break;
            }
        }


        public static void MakeCallBack(string message, StatusImageIndex Status, string companyCode)
        {
            try
            {
                if (MYCALLBACK != null)
                    MYCALLBACK(message, Status, companyCode);
            }
            catch (Exception)
            {
                //do nothing
            }

        }

        /// <summary>
        /// Log an Error - Critical Errors
        /// </summary>
        /// <param name="message">Content to be logged</param>
        /// <param name="exception">Exception parameter - Optional parameter</param>
        public static void AddError(string message, Exception exception = null)
        {
            //initialize(getSource(source) + Level.Error.ToString());
            Initialize("AplicationLogger");
            if (exception != null)
            {
                message += ", " + exception.ToString();
            }

            Log(NLog.LogLevel.Error, message);
        }

        /// <summary>
        /// Log an Information - Process Logging
        /// </summary>
        /// <param name="message">Content to be logged</param>
        public static void AddInfo(string message)
        {
            //initialize(getSource(source) + Level.Info.ToString());
            Initialize("AplicationLogger");
            Log(NLog.LogLevel.Info, message);
        }

        /// <summary>
        /// Log a Warning - Non critical occurrences that did not caused by unexpected exceptions
        /// </summary>
        /// <param name="message">Content to be logged</param>
        public static void AddWarning(string message)
        {
            //initialize(getSource(source) + Level.Warning.ToString());
            Initialize("AplicationLogger");
            Log(NLog.LogLevel.Warn, message);
        }




        /// <summary>
        /// Log an Error - Critical Errors
        /// </summary>
        /// <param name="message">Content to be logged</param>
        /// <param name="exception">Exception parameter - Optional parameter</param>
        public static void AddPDFError(string message, Exception exception = null)
        {
            //initialize(getSource(source) + Level.Error.ToString());
            Initialize("PDFLogger");
            if (exception != null)
            {
                message += ", " + exception.ToString();
            }

            Log(NLog.LogLevel.Error, message);
        }

        /// <summary>
        /// Log an Information - Process Logging
        /// </summary>
        /// <param name="message">Content to be logged</param>
        public static void AddPDFInfo(string message)
        {
            //initialize(getSource(source) + Level.Info.ToString());
            Initialize("PDFLogger");
            Log(NLog.LogLevel.Info, message);
        }

        /// <summary>
        /// Log a Warning - Non critical occurrences that did not caused by unexpected exceptions
        /// </summary>
        /// <param name="message">Content to be logged</param>
        public static void AddPDFWarning(string message)
        {
            //initialize(getSource(source) + Level.Warning.ToString());
            Initialize("PDFLogger");
            Log(NLog.LogLevel.Warn, message);
        }





        private static void Log(NLog.LogLevel level, string message)
        {
            logger.Log(level, message);
        }

        /// <summary>
        /// Get the first part of the Logger from the source
        /// </summary>
        /// <param name="source">Application that requires to log the message</param>
        /// <returns>String First part of the logger name.</returns>
        private static string GetSource(Source source)
        {
            switch (source)
            {
                case Source.BatchInvoice: return "BatchInvoice_";
                case Source.Settings: return "Settings_";
                default: return source.ToString();
                    // "UNKNOWNSOURCE_"
            }
        }

    }
}
