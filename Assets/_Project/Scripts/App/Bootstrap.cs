using UnityEngine;

/// <summary>
/// Bootstrap / Composition Root：游戏启动入口。
/// 职责：初始化基础服务（EventBus、Data、Persistence 等）、组装依赖、启动首流程。
/// 建议挂到首场景中常驻物体上，且通过 Script Execution Order 使其最先执行（如 -100）。
/// </summary>
public class Bootstrap : MonoBehaviour
{
    void Awake()
    {
        // EventBus 为静态类，无需显式初始化
        // 扩展点：在此注册/初始化 Services，并注入到 Manager & Gameplay
        // 例如：AudioService, SaveService, LogService, TimeService 等
        // 建议通过 Script Execution Order 使 Bootstrap 优先执行（如 -100）
    }
}
