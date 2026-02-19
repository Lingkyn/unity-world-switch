using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// 表现逻辑：2D 模式下不渲染阴影，3D 模式下恢复。
/// 挂到需要按维度切换阴影的物体上（如 Player、敌人等）。
/// </summary>
public class DimensionShadowSwitch : MonoBehaviour
{
    [Tooltip("留空则自动取本物体及子物体的 Renderer")]
    [SerializeField] private Renderer[] renderers;

    void OnEnable()
    {
        EventBus.Subscribe<DimensionSwitched>(OnDimensionSwitched);
        if (renderers == null || renderers.Length == 0)
            renderers = GetComponentsInChildren<Renderer>(true);
        var ctrl = FindFirstObjectByType<DimensionSwitchController>();
        if (ctrl != null) ApplyDimension(ctrl.CurrentDimension);
    }

    void OnDisable() => EventBus.Unsubscribe<DimensionSwitched>(OnDimensionSwitched);
    void OnDimensionSwitched(DimensionSwitched e) => ApplyDimension(e.NewDimension);

    void ApplyDimension(DimensionType dim)
    {
        bool castShadow = dim == DimensionType.ThreeD;
        var mode = castShadow ? ShadowCastingMode.On : ShadowCastingMode.Off;
        foreach (var r in renderers)
        {
            if (r != null) r.shadowCastingMode = mode;
        }
    }
}
