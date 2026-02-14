using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

/// <summary>
/// 查找场景和 Prefab 中的缺失引用。菜单：Tools > Find Missing References
/// </summary>
public static class FindMissingReferences
{
    [MenuItem("Tools/Find Missing References")]
    static void Find()
    {
        int totalCount = 0;

        totalCount += FindInScenes(AssetDatabase.FindAssets("t:Scene", new[] { "Assets" }));
        totalCount += FindInPaths(AssetDatabase.FindAssets("t:Prefab", new[] { "Assets" }), "Prefab");

        Debug.Log(totalCount > 0
            ? $"共发现 {totalCount} 处缺失引用，详见 Console"
            : "未发现缺失引用");
    }

    /// <summary>
    /// 场景：先创建空场景占位，再逐个打开检查，避免 CloseScene 触发
    /// "Unloading the last loaded scene is not supported" 警告
    /// </summary>
    static int FindInScenes(string[] guids)
    {
        if (guids.Length == 0) return 0;

        string originalScene = EditorSceneManager.GetActiveScene().path;
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        int count = 0;
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            var scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
            count += FindMissingInScene(scene, path);
            EditorSceneManager.CloseScene(scene, true);
        }

        if (!string.IsNullOrEmpty(originalScene))
            EditorSceneManager.OpenScene(originalScene, OpenSceneMode.Single);
        else
            EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

        return count;
    }

    static int FindMissingInScene(Scene scene, string assetPath)
    {
        int count = 0;
        foreach (GameObject root in scene.GetRootGameObjects())
        {
            count += FindMissingInGameObject(root, assetPath, "场景");
        }
        return count;
    }

    static int FindMissingInGameObject(GameObject go, string assetPath, string assetType)
    {
        int count = 0;
        count += CheckObjectForMissing(go, assetPath, assetType);
        foreach (Component c in go.GetComponents<Component>())
        {
            if (c != null)
                count += CheckObjectForMissing(c, assetPath, assetType);
        }
        foreach (Transform child in go.transform)
            count += FindMissingInGameObject(child.gameObject, assetPath, assetType);
        return count;
    }

    static int FindInPaths(string[] guids, string assetType)
    {
        int count = 0;
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            int found = FindMissingInPrefab(path, assetType);
            if (found > 0)
                count += found;
        }
        return count;
    }

    /// <summary>
    /// Prefab：使用 LoadAllAssetsAtPath 不会触发场景对象的线程警告
    /// </summary>
    static int FindMissingInPrefab(string assetPath, string assetType)
    {
        Object[] allObjects = AssetDatabase.LoadAllAssetsAtPath(assetPath);
        return CheckObjectsForMissing(allObjects, assetPath, assetType);
    }

    static int CheckObjectForMissing(Object obj, string assetPath, string assetType)
    {
        return CheckObjectsForMissing(new[] { obj }, assetPath, assetType);
    }

    static int CheckObjectsForMissing(Object[] objects, string assetPath, string assetType)
    {
        int count = 0;
        foreach (Object obj in objects)
        {
            if (obj == null) continue;

            SerializedObject so = new SerializedObject(obj);
            SerializedProperty sp = so.GetIterator();

            while (sp.NextVisible(true))
            {
                if (sp.propertyType != SerializedPropertyType.ObjectReference) continue;

                if (sp.objectReferenceValue == null && sp.objectReferenceInstanceIDValue != 0)
                {
                    count++;
                    Debug.LogWarning($"[{assetType}] 缺失引用: {assetPath}\n  对象: {obj.name}\n  属性: {sp.propertyPath}", obj);
                }
            }
        }
        return count;
    }
}
