using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
namespace Rekkuzan.Helper.Editor
{
    public class FileManagement : MonoBehaviour
    {

        [MenuItem("Rekkuzan/Persistent Files/Clean persistant files")]
        static void DeletePersistentFiles()
        {
            foreach (var directory in Directory.GetDirectories(Application.persistentDataPath))
            {
                DirectoryInfo data_dir = new DirectoryInfo(directory);
                data_dir.Delete(true);
            }

            foreach (var file in Directory.GetFiles(Application.persistentDataPath))
            {
                FileInfo file_info = new FileInfo(file);
                file_info.Delete();
            }
        }

        [MenuItem("Rekkuzan/Persistent Files/Show")]
        static void ShowPersistentFiles()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }
    }
}
#endif
