using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rekkuzan.Utilities
{
    public class Singleton<T>  where T : new()
    {
        private static readonly object _lockObject = new object();
        private static T _instance;

        public static T Instance
        {
            get
            {
                lock (_lockObject)
                {
                    if (EqualityComparer<T>.Default.Equals(_instance, default(T)) || _instance is null)
                    {
                        _instance = new T();
                    }
                }

                return _instance;
            }
        }

        public static void Drop()
        {
            lock (_lockObject)
            {
                _instance = default(T);
            }
        }
    }
}
