using UnityEngine;

/// <summary>
/// 物体维度切换：根据 2D/3D 激活对应碰撞体，落地时修正角色 Z。
/// </summary>
public class DimensionColliderSwitch : MonoBehaviour
{
    [SerializeField] private Collider collider3D;
    [SerializeField] private Collider colliderFor2D;

    void OnEnable()
    {
        EventBus.Subscribe<DimensionSwitched>(OnDimensionSwitched);
        var ctrl = FindFirstObjectByType<DimensionSwitchController>();
        if (ctrl != null) ApplyDimension(ctrl.CurrentDimension);
        if (collider3D == null && colliderFor2D == null)
            Debug.LogWarning("DimensionColliderSwitch: 请赋值 collider3D 与 colliderFor2D。", this);
    }

    void OnDisable() => EventBus.Unsubscribe<DimensionSwitched>(OnDimensionSwitched);
    void OnDimensionSwitched(DimensionSwitched e) => ApplyDimension(e.NewDimension);

    void ApplyDimension(DimensionType dim)
    {
        if (collider3D != null) collider3D.enabled = dim == DimensionType.ThreeD;
        if (colliderFor2D != null) colliderFor2D.enabled = dim == DimensionType.TwoD;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (colliderFor2D == null || !colliderFor2D.enabled) return;
        var m = collision.gameObject.GetComponent<PlayerMovement_DimensionSwitch>();
        if (m != null && m.Is2DMode)
        {
            var pos = collision.transform.position;
            pos.z = transform.position.z;
            collision.transform.position = pos;
        }
    }
}
