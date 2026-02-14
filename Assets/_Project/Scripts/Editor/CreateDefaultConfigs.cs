using UnityEngine;
using UnityEditor;

/// <summary>
/// 在 Data/Config 下创建默认配置资源。菜单：Tools > Create Default Configs
/// </summary>
public static class CreateDefaultConfigs
{
    const string ConfigPath = "Assets/_Project/Data/Config";

    [MenuItem("Tools/Create Default Configs")]
    static void Create()
    {
        if (!AssetDatabase.IsValidFolder("Assets/_Project"))
        {
            Debug.LogError("找不到 _Project 目录");
            return;
        }

        CreateIfMissing<PlayerMovementConfig>("PlayerMovementConfig");
        CreateIfMissing<ShadowMovementConfig>("ShadowMovementConfig");
        CreateIfMissing<InputConfig>("InputConfig");

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("默认配置已创建于 " + ConfigPath);
    }

    static void CreateIfMissing<T>(string name) where T : ScriptableObject
    {
        string path = $"{ConfigPath}/{name}.asset";
        if (AssetDatabase.LoadAssetAtPath<T>(path) != null)
        {
            Debug.Log($"已存在: {path}");
            return;
        }

        var asset = ScriptableObject.CreateInstance<T>();
        AssetDatabase.CreateAsset(asset, path);
    }
}
