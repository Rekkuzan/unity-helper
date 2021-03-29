using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Rekkuzan.Helper
{
    public class ExportPackage : MonoBehaviour
    {
        [MenuItem("Rekkkuzan/Helper/Export")]
        static void Export()
        {
            //Export scripts with their dependencies into a .unitypackage
            AssetDatabase.ExportPackage("Assets/Rekkuzan/Helper", Application.dataPath + "/Rekkuzan.Helper.unitypackage", ExportPackageOptions.IncludeDependencies | ExportPackageOptions.Recurse);
        }
    }
}