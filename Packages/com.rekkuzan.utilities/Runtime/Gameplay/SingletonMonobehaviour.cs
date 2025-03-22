using UnityEngine;

namespace Rekkuzan.Utilities
{
    public class SingletonMonobehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Singleton Pattern

        protected static T _instance = null;

        public static T Instance
        {
            get
            {
                _instance ??= FindFirstObjectByType<T>();

                return _instance;
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }

        #endregion

    }
}