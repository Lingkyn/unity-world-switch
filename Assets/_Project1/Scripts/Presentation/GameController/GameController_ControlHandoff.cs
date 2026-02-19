using UnityEngine;

/// <summary>
/// 表现层壳：ControlHandoff 关卡入口，持有并协调表现层组件。
/// </summary>
public class GameController_ControlHandoff : MonoBehaviour
{
    [Header("可选引用")]
    [SerializeField] private CharacterControlManager characterControlManager;

    void Start()
    {
        if (characterControlManager == null)
            characterControlManager = FindFirstObjectByType<CharacterControlManager>();
    }
}
