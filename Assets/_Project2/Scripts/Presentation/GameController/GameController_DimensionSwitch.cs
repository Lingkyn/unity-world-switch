using UnityEngine;

/// <summary>
/// 表现层壳：DimensionSwitch 关卡入口，持有并协调表现层组件。
/// </summary>
public class GameController_DimensionSwitch : MonoBehaviour
{
    [Header("可选引用")]
    [SerializeField] private DimensionSwitchController dimensionSwitchController;

    void Start()
    {
        if (dimensionSwitchController == null)
            dimensionSwitchController = FindFirstObjectByType<DimensionSwitchController>();
    }
}
