using UnityEngine;

/// <summary>
/// 大环境维度切换：监听 InputAction 中的 ToggleWorld，切换 2D/3D 虚拟摄像机。
/// 按键绑定在 PlayerInputActions.inputactions 中配置（默认 Tab）。
/// </summary>
public class DimensionSwitchController : MonoBehaviour
{
    [SerializeField] private GameObject vCamera2D;
    [SerializeField] private GameObject vCamera3D;
    [SerializeField] private DimensionType initialDimension = DimensionType.ThreeD;

    public DimensionType CurrentDimension { get; private set; }

    void Awake()
    {
        CurrentDimension = initialDimension;
    }

    void Start()
    {
        if (vCamera2D == null && vCamera3D == null)
            Debug.LogWarning("DimensionSwitchController: vCamera2D 与 vCamera3D 均为空。", this);
        ApplyDimension(CurrentDimension);
        EventBus.Publish(new DimensionSwitched { NewDimension = CurrentDimension });
    }

    void Update()
    {
        if (InputAdapter.Instance == null) return;
        if (InputAdapter.Instance.ToggleWorldPressedThisFrame)
            ToggleDimension();
    }

    void ToggleDimension()
    {
        CurrentDimension = CurrentDimension == DimensionType.ThreeD ? DimensionType.TwoD : DimensionType.ThreeD;
        ApplyDimension(CurrentDimension);
        EventBus.Publish(new DimensionSwitched { NewDimension = CurrentDimension });
    }

    void ApplyDimension(DimensionType dim)
    {
        if (vCamera2D != null) vCamera2D.SetActive(dim == DimensionType.TwoD);
        if (vCamera3D != null) vCamera3D.SetActive(dim == DimensionType.ThreeD);
    }

    public void SwitchTo(DimensionType dim)
    {
        if (CurrentDimension == dim) return;
        CurrentDimension = dim;
        ApplyDimension(CurrentDimension);
        EventBus.Publish(new DimensionSwitched { NewDimension = CurrentDimension });
    }
}
