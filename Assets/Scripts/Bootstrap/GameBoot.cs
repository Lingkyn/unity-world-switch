using UnityEngine;

/// <summary>
/// Bootstrap / Composition Root：游戏启动入口。
/// 职责：初始化基础服务（EventBus、Data、Persistence 等）、组装依赖、启动首流程。
/// 建议挂到首场景中常驻物体上，且通过 Script Execution Order 使其最先执行（如 -100）。
/// </summary>
public class GameBoot : MonoBehaviour
{
    void Awake()
    {
        // EventBus 为静态类，无需显式初始化；此处预留扩展点
        // 若后续有 Data、Persistence、Logging，在此做 Init / 注入 Manager & Gameplay
        // 例如：Data.Init(); Persistence.Init(); 或向 Manager 注入依赖
    }
}
