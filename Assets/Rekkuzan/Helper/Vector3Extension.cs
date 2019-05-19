using UnityEngine;

namespace Rekkuzan.Helper
{
    public static class Vector3Extension
    {
        /// <summary>
        /// Returns a copy of vector with its values clamped to min and max.
        /// </summary>
        /// <param name="vector">Vector3 to clamp</param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static Vector3 Clamp(this Vector3 vector, float min, float max)
        {
            float x = Mathf.Clamp(vector.x, min, max);
            float y = Mathf.Clamp(vector.y, min, max);
            float z = Mathf.Clamp(vector.z, min, max);

            vector.Set(x, y, z);

            return vector;
        }
    }
}
