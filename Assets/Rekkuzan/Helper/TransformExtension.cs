using UnityEngine;

namespace Rekkuzan.Helper
{
    public static class TransformExtension
    {
        /// <summary>
        /// Will find child of the specified name. You can choose to search amongs inactive children.
        /// </summary>
        /// <param name="transform">Root transform to search</param>
        /// <param name="name">Name of the children to find</param>
        /// <param name="includeInactive">Should inactive children be included</param>
        /// <returns></returns>
        public static Transform Find(this Transform transform, string name, bool includeInactive)
        {
            if (transform == null)
                throw new System.NullReferenceException("Transform is null");

            if (string.IsNullOrEmpty(name)) {
                Debug.LogWarning("Trying to find child with empty name");
                return null;
            }

            // if Inactives gameObject are not included, use built-in Unity method
            if (!includeInactive)
            {
                return transform.Find(name);
            }

            foreach (Transform child in transform)
            {
                if (child.name.Equals(name))
                    return child;

                Transform result = child.Find(name, includeInactive);
                if (result != null)
                    return result;
            }

            // no matching child where found
            return null;
        }

        /// <summary>
        /// Will return the number of child (even the inactive ones)
        /// Be aware this method is will recount every time the number of child.
        /// </summary>
        /// <param name="transform">Root transform to count</param>
        /// <returns></returns>
        public static int AllChildCount(this Transform transform)
        {
            int count = 0;
            foreach (Transform child in transform)
                count++;
            return count;
        }
    }
}
