# Level1 场景说明

## 概述

`Level1.unity` 是 **3D/2D 双角色控制权切换** 机制的演示关卡，隶属于 _Project1。3D 角色与 2D 角色在触发器区域内交替响应输入：3D 进入区域 → 仅 2D 可控；2D 进入 → 两者皆可控。

## 场景结构

| 物体 | 说明 |
|------|------|
| **Player3D_ControlHandoff** | 3D 角色，挂 CharacterController + PlayerMovement3D_ControlHandoff |
| **Player2D_ControlHandoff** | 2D 角色，挂 Rigidbody2D + PlayerMovement2D_ControlHandoff |
| **Switch3D_ControlHandoff** / **Switch2D_ControlHandoff** | 触发器平台，挂 ControlHandoffTrigger |
| **GameController_ControlHandoff** | 表现层壳，持有 CharacterControlManager |
| **CharacterControlManager** | 控制权管理，订阅 ControlHandoffZoneEntered |

## 核心机制

- **CharacterControlManager**：根据 ControlHandoffZoneEntered 事件切换 ControlMode（Together / TwoDOnly）
- **PlayerMovement3D_ControlHandoff**：TwoDOnly 模式下不响应输入
- **PlayerMovement2D_ControlHandoff**：始终响应 InputAdapter 输入
- **ControlHandoffTrigger**：检测进入物体，发布事件

## 使用前准备

1. 打开 Scenes/Level1.unity
2. 确保场景中有 CharacterControlManager（可与 GameController 同挂或单独）
3. 3D 角色挂 PlayerMovement3D_ControlHandoff，2D 角色挂 PlayerMovement2D_ControlHandoff
4. 触发器 Collider 勾选 Is Trigger

参数均在组件 Inspector 中配置。

## 依赖

- **New Input System**
