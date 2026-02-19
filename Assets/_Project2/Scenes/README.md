# Level2 场景说明

## 概述

`Level2.unity` 是 **2D/3D 维度切换** 机制的演示关卡，隶属于 _Project2。玩家可通过 **Tab** 键在正交（2D）与透视（3D）视角之间切换，同时改变移动轴向与碰撞行为。

## 场景结构

| 物体 | 说明 |
|------|------|
| **Directional Light** | 主光源，提供基础照明 |
| **Main Camera** | 主相机，挂 Cinemachine Brain，负责虚拟相机切换 |
| **VCam2D** | Cinemachine 2D 虚拟相机，正交模式，用于 2D 视角 |
| **VCam3D** | Cinemachine 3D 虚拟相机，透视模式，用于 3D 视角 |
| **Player** | Player_DimensionSwitch 预制体实例，或由 GameSetup 拖入时一并带入 |
| **Cube** | 平台（3×1×3），带 BoxCollider |

## 核心机制

- **DimensionSwitchController**：监听 `ToggleWorld` 输入（默认 Tab），切换 VCam2D / VCam3D 的激活状态
- **2D 模式**：正交相机，适合平台解谜
- **3D 模式**：透视相机，带来空间沉浸感
- **DimensionColliderSwitch**：平台可挂此组件，在两个 BoxCollider 间切换以适配 2D/3D 碰撞

## 平台碰撞体搭建

2D 与 3D 模式下移动轴向不同（2D 仅 X 轴，3D 为 X+Z），平台**需要配置两个 Collider**：

1. 在平台物体上添加两个 BoxCollider（或其它 Collider），分别用于 2D 和 3D 模式
2. 挂载 `DimensionColliderSwitch`，在 Inspector 中指定 **colliderFor2D**（2D 模式用）和 **collider3D**（3D 模式用）
3. 切换维度时组件会自动启用/禁用对应碰撞体；2D 模式下落地时还会修正角色 Z 轴位置

## 使用前准备

### 方式一：GameSetup（推荐）

从 `Prefabs/GameSetup_DimensionSwitch` 拖入场景即可。该预制体已包含：
- **GameController_DimensionSwitch**：输入（PlayerInput、InputAdapter）与控制器
- **Player_DimensionSwitch**：主角（CharacterController、PlayerMovement_DimensionSwitch）
- **CameraRig_DimensionSwitch**：相机组（DimensionSwitchController + VCam2D + VCam3D）

一键完成所有引用配置，无需再手动挂组件。

### 方式二：手动搭建

1. 若场景中已有 VCam2D、VCam3D，从 `Prefabs/GameController_DimensionSwitch` 拖入（含 InputAdapter、PlayerInput、GameController）
2. 根物体添加 `DimensionSwitchController`，在 Inspector 中引用 **VCam2D**、**VCam3D**
3. 主角使用 `Prefabs/Player_DimensionSwitch`
4. 平台添加 `DimensionColliderSwitch`，并配置两个 Collider（见上文「平台碰撞体搭建」）

## 依赖

- **Cinemachine**
- **New Input System**
