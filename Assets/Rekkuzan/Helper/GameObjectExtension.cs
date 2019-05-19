using UnityEngine;

namespace Rekkuzan.Helper
{
    public static class GameObjectExtension
    {
        /// <summary>
        /// Prevent setting active a gameobject if it doesn't need to
        /// </summary>
        /// <param name="gameObject">GameObject to set active state</param>
        /// <param name="enable">enable state</param>
        public static void SetActive(this GameObject gameObject, bool enable)
        {
            if (gameObject == null)
                return;
            if (gameObject.activeSelf != enable)
                gameObject.SetActive(enable);
        }
    }
}
