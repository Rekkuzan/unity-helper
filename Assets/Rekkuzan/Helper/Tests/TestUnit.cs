using UnityEngine;

namespace Rekkuzan.Helper
{
    public static class TestUnit
    {
        public static void Test()
        {
            Debug.Log("Starting Tests");
            Vector3 Vec = new Vector3(-5, 10, 15);
            Vector3 ClampVec3 = Vec.Clamp(-1, 1);

            Debug.Assert(ClampVec3.x == -1 && ClampVec3.y == 1 && ClampVec3.z == 1, "Vector.Clamp not working");

            Debug.Log("Test finished");
        }
    }
}
