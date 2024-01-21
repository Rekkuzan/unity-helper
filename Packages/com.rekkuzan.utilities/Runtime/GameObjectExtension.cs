using UnityEngine;

namespace Rekkuzan.Utilities
{
    public static class GameObjectExtension
    {
        /// <summary>
        /// Prevent setting active a gameObject if it doesn't need to
        /// </summary>
        /// <param name="gameObject">GameObject to set active state</param>
        /// <param name="enable">enable state</param>
        public static void SetActive(this GameObject gameObject, bool enable)
        {
            if (gameObject is null)
                return;
            if (gameObject.activeSelf != enable)
                gameObject.SetActive(enable);
        }
    }

}
