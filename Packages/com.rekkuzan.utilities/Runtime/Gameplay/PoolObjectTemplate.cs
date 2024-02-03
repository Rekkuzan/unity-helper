using System.Collections.Generic;
using UnityEngine;

namespace Rekkuzan.Utilities
{
    /// <summary>
    /// Template of pool object based on MonoBehaviour
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PoolObjectTemplate<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region Singleton
        public static PoolObjectTemplate<T> Instance { get; private set; }

        protected void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this);
        }

        #endregion

        [SerializeField] protected GameObject ObjectToPool;
        [SerializeField] protected int InitialPoolSize = 10;
        [SerializeField] protected Transform SuperParent = null;

        protected Stack<T> PoolOfObject = new Stack<T>();
        protected bool isDestroying = false;

        #region MonoBehaviour's Methods
        protected void Start()
        {
            for (int i = 0; i < InitialPoolSize; i++)
            {
                T instance = CreateObject<T>();
                if (instance == null)
                {
                    ConsoleLogger.Error("Can't find instance of " + nameof(T));
                    return;
                }

                PoolOfObject.Push(instance);
            }
        }

        protected void OnDestroy()
        {
            isDestroying = true;
        }
        #endregion

        #region Protected Methods

        /// <summary>
        /// Will instantiate the object to be pooled
        /// </summary>
        /// <typeparam name="U">Monobehaviour registered on the pool</typeparam>
        /// <returns></returns>
        protected T CreateObject<U>()
        {
            if (typeof(T) != typeof(U))
                throw new System.Exception("Type is invalid");

            GameObject instantiated = Instantiate(ObjectToPool, transform);

            instantiated.SetActive(false);

            T res = instantiated.GetComponent<T>();

            if (res == null)
            {
                Destroy(instantiated);
            }

            return res;
        }

        /// <summary>
        /// Will return an object from the pool. If the pool is empty, it will create one.
        /// </summary>
        /// <returns></returns>
        protected T GetFromPoolInternal()
        {
            if (isDestroying)
                return default;
            T res = PoolOfObject.Count > 0 ? PoolOfObject.Pop() : CreateObject<T>();
            res.gameObject.transform.SetParent(SuperParent);
            return res;
        }

        /// <summary>
        /// Will return the object to the pool
        /// </summary>
        /// <param name="Object"></param>
        protected void ReturnObjectToPoolInternal(T Object)
        {
            if (isDestroying)
                return;
            if (Object == null || Object.gameObject == null)
                return;
            Object.transform.SetParent(transform);
            Object.gameObject.SetActive(false);
            PoolOfObject.Push(Object);
        }
        #endregion

        public static T GetFromPool()
        {
            return Instance.GetFromPoolInternal();
        }

        public static void ReturnObjectToPool(T Object)
        {
            Instance.ReturnObjectToPoolInternal(Object);
        }
    }
}
