using UnityEngine;

namespace Rekkuzan.Utilities.Extensions
{
    public static class TransformExtension
    {
        /// <summary>
        /// This method will find the first child of the specified name.
        /// </summary>
        /// <param name="transform">Root transform to search</param>
        /// <param name="name">Name of the children to find</param>
        /// <param name="includeInactive">Should inactive children be included?</param>
        /// <returns>First child with specified name, <see langword="null" /> if none is found.</returns>
        public static Transform Find(this Transform transform, string name, bool includeInactive)
        {
            if (transform == null)
                throw new System.NullReferenceException("Transform is null");

            if (string.IsNullOrEmpty(name)) {
                Debug.LogWarning("Trying to find child with empty name");
                return null;
            }

            // if disabled gameObjects are not included, use built-in Unity method
            if (!includeInactive)
            {
                return transform.Find(name);
            }

            foreach (Transform child in transform)
            {
                if(child.name.Equals(name))
                {
                    return child;
                }

                Transform result = child.Find(name, true);
                if(result != null)
                {
                    return result;
                }
            }
            
            return null;
        }

        /// <summary>
        /// This method will return the number of children, including the disabled. 
        /// </summary>
        /// <param name="transform">Root transform to count</param>
        /// <returns>Total children count.</returns>
        public static int AllChildCount(this Transform transform)
        {
            int count = 0;
            foreach (Transform _ in transform)
                count++;
            return count;
        }
    }
}
