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

        [Test]
        public static void SetLocalPositionAndRotation()
        {
            GameObject parent = new GameObject();
            GameObject child = new GameObject();

            child.transform.SetParent(parent.transform);
            parent.transform.position = new Vector3(1, 5, 19);
            parent.transform.rotation = Quaternion.Euler(14, 50, 96);

            child.transform.position = Vector3.zero;
            child.transform.rotation = Quaternion.identity;


            child.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            Debug.Assert(child.transform.position == parent.transform.position, $"Set Position in SetLocalPositionAndRotation() is not working ({child.transform.position} expect {parent.transform.position})");
            Debug.Assert(child.transform.rotation.eulerAngles == parent.transform.rotation.eulerAngles, $"Set Rotation in SetLocalPositionAndRotation() is not working ({child.transform.rotation.eulerAngles} expect {parent.transform.rotation.eulerAngles})"); ;


            parent.transform.SetLocalPositionAndRotation(Vector3.one, Quaternion.Euler(-90, 0, 25));
            Debug.Assert(Vector3.one == parent.transform.position, $"Set Position in SetLocalPositionAndRotation() is not working ({Vector3.one} expect {parent.transform.position})");
            Debug.Assert(Quaternion.Euler(-90, 0, 25).eulerAngles == parent.transform.rotation.eulerAngles, $"Set Rotation in SetLocalPositionAndRotation() is not working ({Quaternion.Euler(-90, 0, 25).eulerAngles} expect {parent.transform.rotation.eulerAngles})"); ;

            // todo test with more parent/child link
        }
    }
}
