using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rekkuzan.Utilities
{
    /// <summary>
    /// Utility to use all your coroutine on the same gameObject.
    /// <remarks>
    /// This is an experimental untested feature that requires functional tests.
    /// Use with caution.
    /// </remarks>
    /// </summary>
    public class CoroutineRunner : SingletonMonobehaviour<CoroutineRunner>
    {
        private readonly Dictionary<Coroutine, Coroutine> _activeCoroutines = new();
    
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    
        public static Coroutine Start(IEnumerator routine)
        {
            if (Instance == null)
            {
                Debug.LogError("CoroutineRunner is not initialized!");
                return null;
            }
    
            Coroutine coroutine = Instance.StartCoroutine(routine);
            Coroutine internalCoroutine = Instance.StartCoroutine(Instance.InternalCoroutine(coroutine));
            Instance._activeCoroutines.Add(internalCoroutine, coroutine);
            return internalCoroutine;
        }

        private IEnumerator InternalCoroutine(Coroutine coroutine)
        {
            yield return coroutine;
            KeyValuePair<Coroutine, Coroutine> activeCoroutine = _activeCoroutines.FirstOrDefault(pair => pair.Value == coroutine);
            
            // The launched coroutine is done, remove it from the list
            _activeCoroutines.Remove(activeCoroutine.Key);
        }
    
        public static void Stop(Coroutine coroutine)
        {
            if (Instance == null)
            {
                Debug.LogError("CoroutineRunner is not initialized!");
                return;
            }

            if(coroutine == null || !Instance._activeCoroutines.TryGetValue(coroutine, out Coroutine userCoroutine))
            {
                if(coroutine != null)
                {
                    Instance.StopCoroutine(coroutine);
                }
                
                return;
            }

            if(userCoroutine != null)
            {
                Instance.StopCoroutine(userCoroutine);
            }

            Instance.StopCoroutine(coroutine);
            Instance._activeCoroutines.Remove(coroutine);
        }
    
        public static void StopAll()
        {
            if (Instance == null)
            {
                Debug.LogError("CoroutineRunner is not initialized!");
                return;
            }
    
            foreach (KeyValuePair<Coroutine, Coroutine> pair in Instance._activeCoroutines)
            {
                Stop(pair.Key);
            }
            
            Instance._activeCoroutines.Clear();
        }
    }

}
