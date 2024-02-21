using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("GraphicsLabor.Editor"), InternalsVisibleTo("GraphicsLabor.Tests")]
namespace GraphicsLabor.Scripts.Core.Utility
{
    internal enum LogLevel
    {
        Info,
        Warning,
        Error,
    }
    
    /// <summary>
    /// Internal Logger used by the package
    /// </summary>
    internal static class GLogger
    {
        private const string Prefix = "GraphicsLabor";
        private const string FormatString = "[{0} - {1}] ";

        /// <summary>
        /// Logs a message to the console using a formatted string. Defaults to Info log level
        /// </summary>
        /// <param name="message">Log Message</param>
        /// <param name="logLevel">Log Info Level</param>
        public static void Log(string message, LogLevel logLevel = LogLevel.Info)
        {
            Debug.Log(string.Format(FormatString, Prefix, logLevel.ToString()) + message);
        }
        
        /// <summary>
        /// LogWarning a message to the console using a formatted string. Defaults to Warning log level
        /// </summary>
        /// <param name="message">Log Message</param>
        /// <param name="logLevel">Log Info Level</param>
        public static void LogWarning(string message, LogLevel logLevel = LogLevel.Warning)
        {
            Debug.LogWarning(string.Format(FormatString, Prefix, logLevel.ToString()) + message);
        }
        
        /// <summary>
        /// LogError a message to the console using a formatted string. Defaults to Error log level
        /// </summary>
        /// <param name="message">Log Message</param>
        /// <param name="logLevel">Log Info Level</param>
        public static void LogError(string message, LogLevel logLevel = LogLevel.Error)
        {
            Debug.LogError(string.Format(FormatString, Prefix, logLevel.ToString()) + message);
        }

    }
}