using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
namespace Rekkuzan.Utilities.Editor
{
    public class FileManagement : MonoBehaviour
    {

        [MenuItem("Rekkuzan/Utilities/Persistent Files/Clean persistant files")]
        private static void DeletePersistentFiles()
        {
            foreach (var directory in Directory.GetDirectories(Application.persistentDataPath))
            {
                DirectoryInfo dataDir = new DirectoryInfo(directory);
                dataDir.Delete(true);
            }

            foreach (var file in Directory.GetFiles(Application.persistentDataPath))
            {
                FileInfo fileInfo = new FileInfo(file);
                fileInfo.Delete();
            }
        }

        [MenuItem("Rekkuzan/Utilities/Persistent Files/Show")]
        private static void ShowPersistentFiles()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }
    }
}
#endif
