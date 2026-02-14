using UnityEngine;

/// <summary>
/// 存档服务：负责游戏进度的保存与读取。
/// TODO: 实现具体逻辑（PlayerPrefs / JSON 文件 / 云存档等）。
/// </summary>
public class SaveService : MonoBehaviour
{
    public void Save(string slotId, object data) { /* TODO */ }
    public T Load<T>(string slotId) where T : class { return null; }
    public bool HasSave(string slotId) { return false; }
}
