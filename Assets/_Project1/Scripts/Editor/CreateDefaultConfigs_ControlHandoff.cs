using UnityEditor;

/// <summary>
/// ControlHandoff 参数均在组件 Inspector 中配置，无需 Config 资产。
/// </summary>
public static class CreateDefaultConfigs_ControlHandoff
{
    [MenuItem("Tools/ControlHandoff/Create Default Configs")]
    static void Create()
    {
        UnityEngine.Debug.Log("[ControlHandoff] 参数在组件 Inspector 中配置即可，无需创建 Config。");
    }
}
