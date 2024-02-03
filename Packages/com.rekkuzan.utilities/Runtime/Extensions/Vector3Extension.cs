using UnityEngine;

namespace Rekkuzan.Utilities.Extensions
{
    public static class Vector3Extension
    {
        /// <summary>
        /// Clamp a Vector3 values.
        /// </summary>
        /// <param name="vector">Vector3 to clamp</param>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        public static void Clamp(this Vector3 vector, float min, float max)
        {
            vector.Set(Mathf.Clamp(vector.x, min, max),  Mathf.Clamp(vector.y, min, max), Mathf.Clamp(vector.z, min, max));
        }
    }
}
