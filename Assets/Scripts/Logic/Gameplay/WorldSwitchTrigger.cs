using UnityEngine;

/// <summary>
/// 挂到场景中「开关」物体上，该物体需带 Collider 且勾选 Is Trigger。
/// Player 碰到 → 切到仅影子动；Shadow 碰到 → 切回一起动。
/// </summary>
[RequireComponent(typeof(Collider))]
public class WorldSwitchTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (CharacterControlManager.Instance == null) return;

        if (other.GetComponent<PlayerMovement>() != null)
        {
            CharacterControlManager.Instance.SwitchToShadowOnly();
            return;
        }

        if (other.GetComponent<ShadowMovement2D>() != null)
        {
            CharacterControlManager.Instance.SwitchToTogether();
        }
    }
}
