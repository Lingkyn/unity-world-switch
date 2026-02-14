using UnityEngine;

/// <summary>
/// Layer 名称与 ID 的静态引用，避免硬编码字符串。无需挂到场景。
/// 若项目未配置对应 Layer，NameToLayer 返回 -1。
/// </summary>
public static class Layers
{
    public static readonly int Default = LayerMask.NameToLayer("Default");
    public static readonly int Ground = LayerMask.NameToLayer("Ground");
    public static readonly int Player = LayerMask.NameToLayer("Player");
    public static readonly int Shadow = LayerMask.NameToLayer("Shadow");

    /// <summary> 获取 Layer 的位掩码，用于射线检测等。 </summary>
    public static int GetMask(params string[] layerNames)
    {
        return LayerMask.GetMask(layerNames);
    }
}
