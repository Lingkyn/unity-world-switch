using UnityEditor;

/// <summary>
/// DimensionSwitch 参数均在组件 Inspector 中配置，无需 Config 资产。
/// </summary>
public static class CreateDefaultConfigs_DimensionSwitch
{
    [MenuItem("Tools/DimensionSwitch/Create Default Configs")]
    static void Create()
    {
        UnityEngine.Debug.Log("[DimensionSwitch] 参数在组件 Inspector 中配置即可，无需创建 Config。");
    }
}
