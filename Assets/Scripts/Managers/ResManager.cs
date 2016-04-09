using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class ResManager
{
    static List<BundleBase> s_bundle = new List<BundleBase>();

    public static void InitRes()
    {
        string filepath = string.Format("file:///{0}/Bundles/Bundles", Application.streamingAssetsPath);

        var cw = WWW.LoadFromCacheOrDownload(filepath, 1);
        var list = (AssetBundleManifest)cw.assetBundle.LoadAsset("AssetBundleManifest");

        foreach (var str in list.GetAllAssetBundles())
        {
            var sw = WWW.LoadFromCacheOrDownload(string.Format("file:///{0}/Bundles/{1}", Application.streamingAssetsPath, str), 1);
            List<string> strList = new List<string>();
            foreach (var item in sw.assetBundle.GetAllAssetNames())
                strList.Add(item);

            s_bundle.Add(new BundleBase(str, sw.assetBundle));

            sw.assetBundle.Unload(true);
            sw.Dispose();
        }
        cw.assetBundle.Unload(true);
        cw.Dispose();
    }

    public static T GetResByName<T>(string name) where T : Object
    {
        foreach (var bundle in s_bundle)
        {
            if (bundle.m_bundle == null)
            {
                var sw = WWW.LoadFromCacheOrDownload(string.Format("file:///{0}/Bundles/{1}", Application.streamingAssetsPath, bundle.m_bundleName), 1);
                bundle.m_bundle = sw.assetBundle;
                sw.Dispose();
            }

            string path = name + ".prefab";
            if (bundle.m_bundle.Contains(path))
                return GetResInBundle<T>(bundle, path);
        }
        return null;
    }

    public static void Release(GameObject obj)
    {
        foreach (var bb in s_bundle)
        {
            if (bb.m_refList.Contains(obj))
            {
                bb.m_refList.Remove(obj);
                GameObject.DestroyImmediate(obj);
                return;
            }
        }
    }

    static T GetResInBundle<T>(BundleBase bundle, string resName) where T : Object
    {
        var obj = bundle.m_bundle.LoadAsset(resName);
        GameObject insObj = (GameObject)GameObject.Instantiate(obj);
        bundle.m_refList.Add(insObj);
        return insObj.GetComponent<T>();
    }

    public static void CheckBundlesRelease()
    {
        foreach (var bb in s_bundle)
        {
            int count = bb.m_refList.RemoveAll(e => e == null);
            Debug.Log("Bundle:" + bb.m_bundleName + "\nRemoveItemCount:" + count);
            if (bb.m_bundle != null && bb.m_refList.Count == 0)
                bb.m_bundle.Unload(true);
        }
    }
}


public class BundleBase
{
    public string m_bundleName;
    public AssetBundle m_bundle;
    public List<GameObject> m_refList = new List<GameObject>();

    public BundleBase(string bundleName, AssetBundle bb)
    {
        m_bundleName = bundleName;
        m_bundle = bb;
    }
}