/// <summary>
/// 玩法相关事件定义，供 EventBus 发布/订阅。
/// </summary>

/// <summary>
/// 世界切换区域：谁进入了触发器。
/// Player 进入 → 订阅方切到仅影子动；Shadow 进入 → 订阅方切回一起动。
/// </summary>
public struct WorldSwitchZoneEntered
{
    public enum EnteredBy
    {
        Player,
        Shadow
    }

    public EnteredBy Who;
}
