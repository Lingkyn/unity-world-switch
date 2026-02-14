using UnityEngine;

/// <summary>
/// 控制当前是「一起动」还是「仅影子动」。（流程与调度，Manager 域）
/// 挂到场景中任意常驻物体（如空物体或 GameManager）上即可，只需一个。
/// </summary>
public class CharacterControlManager : MonoBehaviour
{
    public enum ControlMode
    {
        Together,   // Player + Shadow 一起响应输入
        ShadowOnly  // 仅 Shadow 响应输入
    }

    public static CharacterControlManager Instance { get; private set; }

    public ControlMode CurrentMode { get; private set; } = ControlMode.Together;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        EventBus.Subscribe<WorldSwitchZoneEntered>(OnWorldSwitchZoneEntered);
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            EventBus.Unsubscribe<WorldSwitchZoneEntered>(OnWorldSwitchZoneEntered);
            Instance = null;
        }
    }

    void OnWorldSwitchZoneEntered(WorldSwitchZoneEntered e)
    {
        if (e.Who == WorldSwitchZoneEntered.EnteredBy.Player)
            SwitchToShadowOnly();
        else
            SwitchToTogether();
    }

    public void SwitchToShadowOnly()
    {
        CurrentMode = ControlMode.ShadowOnly;
    }

    public void SwitchToTogether()
    {
        CurrentMode = ControlMode.Together;
    }
}
