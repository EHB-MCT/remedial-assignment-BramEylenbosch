#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class OpenPersistentDataPath
{
    [MenuItem("Tools/Open Persistent Data Path")]
    public static void OpenPath()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
}
#endif
