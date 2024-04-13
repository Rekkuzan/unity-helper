using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rekkuzan.Utilities
{
    /// <summary>
    /// Utility to retrieve variable environment.
    /// <remarks>
    /// This is an experimental untested feature that requires functional tests.
    /// Use with caution.
    /// </remarks>
    /// </summary>
    public class EnvironmentVariableHandler : Singleton<EnvironmentVariableHandler>
    {
        private readonly Dictionary<string, string> _variableLookup = new Dictionary<string, string>();

        public static string GetEnvironmentVariable(string variableName, EnvironmentVariableTarget target = EnvironmentVariableTarget.User)
        {
            string value = Instance._variableLookup.GetValueOrDefault(variableName, null);

            if(value != null)
            {
                return value;
            }

            try
            {
                value = Environment.GetEnvironmentVariable(variableName, target);
                Instance._variableLookup.Add(variableName, value);
            }
            catch (Exception ex)
            {
                Debug.LogError("Error getting environment variable: " + ex.Message);
            }
            
            return value;
        }
    }
}
