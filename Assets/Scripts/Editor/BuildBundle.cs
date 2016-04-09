using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class BuildBundle : ScriptableObject 
{
    static BuildBundle s_instance = null;

    [MenuItem("CustomTools/BuildAssetBundle")]
    public static void OnShow()
    {
        GetInstance();
        Selection.activeObject = s_instance;
    }

    static void GetInstance()
    {
        if (!Directory.Exists(Application.dataPath + "/Config"))
            Directory.CreateDirectory(Application.dataPath + "/Config");

        if (!File.Exists(Application.dataPath + "/Config/BuildConfig.asset"))
        {
            var temp = ScriptableObject.CreateInstance<BuildBundle>();
            AssetDatabase.CreateAsset(temp, "Assets/Config/BuildConfig.asset");
        }

        s_instance = AssetDatabase.LoadAssetAtPath<BuildBundle>("Assets/Config/BuildConfig.asset");
    }
}
