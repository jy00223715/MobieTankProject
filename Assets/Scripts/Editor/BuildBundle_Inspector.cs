using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BuildBundle))]
public class BuildBundle_Inspector : Editor
{
    BuildBundle s_instance;
    int m_index = 0;

    void OnEnable()
    {
        s_instance = target as BuildBundle;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        m_index = GUILayout.Toolbar(m_index, new string[3] { "PC", "IOS", "Android" });

        if (GUILayout.Button("开始编译"))
            BuildBundles();

    }

    void BuildBundles()
    {
        BuildTarget buildTarget = BuildTarget.StandaloneWindows;
        switch(m_index)
        {
            case 0:
                buildTarget = BuildTarget.StandaloneWindows;
                break;
            case 1:
                buildTarget = BuildTarget.iOS;
                break;
            case 2:
                buildTarget = BuildTarget.Android;
                break;
        }
        BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath + "/Bundles");
    }
}