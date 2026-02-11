using UnityEngine;

/// <summary>
/// 表现层·交互检测：碰撞检测「谁进入世界切换区域」，结果通过 EventBus 发布，不直接调逻辑层。
/// 同时支持 3D 与 2D 物理：
/// - 3D 开关：挂到带 Collider（勾选 Is Trigger）的物体上，用于检测 Player（3D）进入。
/// - 2D 开关：挂到带 Collider2D（勾选 Is Trigger）的物体上，用于检测 Shadow（2D）进入。
/// </summary>
public class WorldSwitchTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        TryPublishFrom(other.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        TryPublishFrom(other.gameObject);
    }

    static void TryPublishFrom(GameObject go)
    {
        if (go.GetComponent<PlayerMovement>() != null)
        {
            EventBus.Publish(new WorldSwitchZoneEntered { Who = WorldSwitchZoneEntered.EnteredBy.Player });
            return;
        }

        if (go.GetComponent<ShadowMovement2D>() != null)
        {
            EventBus.Publish(new WorldSwitchZoneEntered { Who = WorldSwitchZoneEntered.EnteredBy.Shadow });
        }
    }
}
