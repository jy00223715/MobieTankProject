using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(ColliderHeightSet))]
public class ColliderHeightSet : EditorWindow
{
    bool[] m_flags;
    BoxCollider[] m_colliders;
    Vector2 m_scrollPos = Vector2.zero;

    [MenuItem("CustomTools/ColliderHeightSet")]
    public static void OpenToolBox()
    {
        ColliderHeightSet toolbox = EditorWindow.GetWindow(typeof(ColliderHeightSet)) as ColliderHeightSet;
        toolbox.Init();
    }

    public void Init()
    {
        m_colliders = GameObject.FindObjectsOfType(typeof(BoxCollider)) as BoxCollider[];
        m_flags = new bool[m_colliders.Length];
    }

    void OnGUI()
    {
        m_scrollPos = GUILayout.BeginScrollView(m_scrollPos);
        for(int i = 0 ;i < m_colliders.Length ; i++)
        {
            m_flags[i] = GUILayout.Toggle(m_flags[i], m_colliders[i].name);
        }
        GUILayout.EndScrollView();

        if (GUILayout.Button("SelectAll/Off"))
            SelectAll();

        if (GUILayout.Button("StartModif"))
            Modif();
    }

    void Modif()
    {
        for (int i = 0; i < m_flags.Length; i++)
        {
            if (m_flags[i])
            {
                if (m_colliders[i].size.y < 2.0f)
                    m_colliders[i].size = new Vector3(m_colliders[i].size.x, m_colliders[i].size.y + 5.0f, m_colliders[i].size.z);
            }
        }
    }

    void SelectAll()
    {
        for (int i = 0; i < m_flags.Length; i++)
            m_flags[i] = !m_flags[i];
    }

}