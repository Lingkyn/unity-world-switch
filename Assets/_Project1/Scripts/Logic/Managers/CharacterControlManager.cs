using UnityEngine;

public class CharacterControlManager : MonoBehaviour
{
    public enum ControlMode { Together, TwoDOnly }

    public static CharacterControlManager Instance { get; private set; }
    public ControlMode CurrentMode { get; private set; } = ControlMode.Together;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        EventBus.Subscribe<ControlHandoffZoneEntered>(OnControlHandoffZoneEntered);
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            EventBus.Unsubscribe<ControlHandoffZoneEntered>(OnControlHandoffZoneEntered);
            Instance = null;
        }
    }

    void OnControlHandoffZoneEntered(ControlHandoffZoneEntered e)
    {
        if (e.Who == ControlHandoffZoneEntered.EnteredBy.Player3D)
            SwitchTo2DOnly();
        else
            SwitchToTogether();
    }

    public void SwitchTo2DOnly() => CurrentMode = ControlMode.TwoDOnly;
    public void SwitchToTogether() => CurrentMode = ControlMode.Together;
}
