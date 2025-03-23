using System.Diagnostics;
using UnityEngine;

namespace Rekkuzan.Utilities
{
    public static class ConsoleLogger
    {
        [Conditional("UNITY_LOG_ENABLE_DEBUG")]
        public static void Debug(string message)
        {
#if UNITY_LOG_ENABLE_DEBUG
            UnityEngine.Debug.Log(message);
#endif
        }

        [Conditional("UNITY_LOG_ENABLE")]
        public static void Warning(string message)
        {
#if UNITY_LOG_ENABLE
            UnityEngine.Debug.LogWarning(message);
#endif
        }

        [Conditional("UNITY_LOG_ENABLE")]
        public static void Error(string message)
        {
#if UNITY_LOG_ENABLE
            UnityEngine.Debug.LogError(message);
#endif
        }
    }
}