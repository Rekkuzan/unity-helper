using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections;

namespace Rekkuzan.Helper
{
    public static class TestUnit
    {
        [Test]
        public static void ClampVector3()
        {
            Vector3 Vec = new Vector3(-5, 10, 15);
            Vector3 ClampVec3 = Vec.Clamp(-1, 1);
            Debug.Assert(ClampVec3.x == -1 && ClampVec3.y == 1 && ClampVec3.z == 1, "Vector.Clamp not working");
        }

        [Test]
        public static void SetActiveGameObject()
        {
            GameObject Test = new GameObject();
            Test.SetActive(true);
            Debug.Assert(Test.activeSelf == true, "GameObject.SetActive() is not working");
            Test.SetActive(false);
            Debug.Assert(Test.activeSelf == false, "GameObject.SetActive() is not working");
            Test.SetActive(false);
            Test.SetActive(false);
            Debug.Assert(Test.activeSelf == false, "GameObject.SetActive() is not working");
        }
    }
}
