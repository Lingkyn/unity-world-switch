using System;
using System.Collections.Generic;

/// <summary>
/// 跨系统事件总线：发布/订阅，解耦调用方与处理方。
/// 按事件类型 T 订阅与发布，同一类型可有多个订阅者。
/// </summary>
public static class EventBus
{
    static readonly Dictionary<Type, Delegate> _handlers = new Dictionary<Type, Delegate>();

    public static void Subscribe<T>(Action<T> handler)
    {
        if (handler == null) return;
        var type = typeof(T);
        if (_handlers.TryGetValue(type, out var existing))
            _handlers[type] = Delegate.Combine(existing, handler);
        else
            _handlers[type] = handler;
    }

    public static void Unsubscribe<T>(Action<T> handler)
    {
        if (handler == null) return;
        var type = typeof(T);
        if (!_handlers.TryGetValue(type, out var existing)) return;
        var combined = Delegate.Remove(existing, handler);
        if (combined == null)
            _handlers.Remove(type);
        else
            _handlers[type] = combined;
    }

    public static void Publish<T>(T eventData)
    {
        var type = typeof(T);
        if (!_handlers.TryGetValue(type, out var handler)) return;
        foreach (var d in handler.GetInvocationList())
            (d as Action<T>)?.Invoke(eventData);
    }

    /// <summary> 清空所有订阅（如场景卸载时按需调用）。 </summary>
    public static void Clear()
    {
        _handlers.Clear();
    }
}
