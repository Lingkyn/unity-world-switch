using UnityEngine;

public class ControlHandoffTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other) => TryPublishFrom(other.gameObject);
    void OnTriggerEnter2D(Collider2D other) => TryPublishFrom(other.gameObject);

    static void TryPublishFrom(GameObject go)
    {
        if (go.GetComponent<PlayerMovement3D_ControlHandoff>() != null)
        {
            EventBus.Publish(new ControlHandoffZoneEntered { Who = ControlHandoffZoneEntered.EnteredBy.Player3D });
            return;
        }
        if (go.GetComponent<PlayerMovement2D_ControlHandoff>() != null)
            EventBus.Publish(new ControlHandoffZoneEntered { Who = ControlHandoffZoneEntered.EnteredBy.Player2D });
    }
}
