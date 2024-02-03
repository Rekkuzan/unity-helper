using UnityEngine;
using NUnit.Framework;
using Rekkuzan.Utilities.Extensions;

namespace Rekkuzan.Helper
{
    public static class TestUnit
    {
        [Test]
        public static void ClampVector3()
        {
            Vector3 vec = new Vector3(-5, 10, 15);
            vec.Clamp(-1, 1);
            Debug.Assert(vec.x == -1 && vec.y == 1 && vec.z == 1, "Vector.Clamp not working");
        }
    }
}
