using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// 扫描 Prefab 与场景，找出 Missing 引用。
/// </summary>
public static class FindMissingReferences
{
    [MenuItem("Tools/Find Missing References")]
    static void Find()
    {
        var results = new List<string>();
        string[] guids = AssetDatabase.FindAssets("t:Prefab t:SceneAsset", new[] { "Assets" });

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Object[] assets;
            try { assets = AssetDatabase.LoadAllAssetsAtPath(path); }
            catch { continue; }
            foreach (var obj in assets)
            {
                if (obj == null) continue;
                try
                {
                    var so = new SerializedObject(obj);
                    var sp = so.GetIterator();
                    while (sp.Next(true))
                    {
                        if (sp.propertyType != SerializedPropertyType.ObjectReference) continue;
                        if (sp.objectReferenceValue != null) continue;
                        if (sp.objectReferenceInstanceIDValue == 0) continue;
                        results.Add($"{path} | {obj.name}.{sp.propertyPath}");
                    }
                }
                catch (System.Exception) { /* 忽略损坏资源的序列化错误 */ }
            }
        }

        if (results.Count == 0)
        {
            Debug.Log("[FindMissingReferences] 未发现 Missing 引用");
            return;
        }

        foreach (var r in results)
            Debug.LogWarning(r);
        Debug.LogWarning($"[FindMissingReferences] 共 {results.Count} 处");
    }
}
