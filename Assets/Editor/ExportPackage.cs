using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Rekkuzan.Utilities
{
    public class ExportPackage : MonoBehaviour
    {
        [MenuItem("Rekkuzan/Helper/Export")]
        static void Export()
        {
            //Export scripts with their dependencies into a .unitypackage
            AssetDatabase.ExportPackage("Assets/Rekkuzan/Helper", Application.dataPath + "/Rekkuzan.Utilities.unitypackage", ExportPackageOptions.IncludeDependencies | ExportPackageOptions.Recurse);
        }
    }
}