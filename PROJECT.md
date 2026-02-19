# World Switch 项目文档

## 一、项目概述

**World Switch** 是一个 Unity 关卡机制合集，具备双重用途：

1. **完整可玩 Demo**：在本项目中通过主入口选关，体验不同 world switch 机制
2. **模板库**：用户选定喜欢的关卡后，可将对应的 _Project 文件夹复制到自己的 Unity 项目中独立使用

每个 _ProjectN 关卡**完全独立**，复制时无需依赖 _Project 或其他 _Project。

---

## 二、项目结构

```
Assets/
├── _Project/           # 关卡主入口（主菜单、选关）
│   ├── Scenes/         # 关卡入口场景
│   ├── MainMenu/       # 主菜单（规划中）
│   ├── Boot/           # 启动逻辑（规划中）
│   └── Editor/         # 主入口相关工具（规划中）
│
├── _Project1/          # 3D/2D 双角色控制权切换
│   ├── Art/            # 独立材质（Player2D/Player3D、Ground、Switch）
│   ├── Audio/
│   ├── Data/           # SampleSceneProfile_ControlHandoff
│   ├── Prefabs/        # 规划：Player_ControlHandoff、GameSetup_ControlHandoff
│   ├── Scenes/         # Level1.unity
│   └── Scripts/
│
├── _Project2/          # 2D/3D 视角切换
│   ├── Art/
│   ├── Audio/
│   ├── Data/           # 可选数据
│   ├── Prefabs/        # GameSetup_DimensionSwitch、Player_DimensionSwitch、VCam2D、VCam3D 等
│   ├── Scenes/         # Level2.unity
│   └── Scripts/
│
├── Editor/             # 全局 Editor 工具（如 FindMissingReferences）
└── Settings/           # URP、Volume 等全局设置
```

---

## 三、关卡说明

| 目录 | 机制 | 场景 | 简介 |
|------|------|------|------|
| **_Project** | 关卡主入口 | — | 主菜单选关后进入对应关卡 |
| **_Project1** | 3D/2D 双角色控制权切换 | Level1.unity | 3D 角色与 2D 角色在区域内交替响应输入 |
| **_Project2** | 2D/3D 视角切换 | Level2.unity | Tab 切换正交/透视相机、移动轴向及碰撞 |

---

## 四、依赖

| 依赖 | 用途 | 使用项目 |
|------|------|----------|
| Unity 2022+ | 引擎 | 全部 |
| URP | 渲染 | 全部 |
| New Input System | 输入 | _Project1、_Project2 |
| Cinemachine | 相机 | _Project2 |

---

## 五、命名规范

### 5.1 资源命名

- **预制体**：`类型_机制` 或 `类型机制`（PascalCase）
  - 示例：`Player_ControlHandoff`、`GameController_DimensionSwitch`
- **Config 资产**：若使用 ScriptableObject 配置，后缀 `类型_机制`
- **场景**：PascalCase
  - 示例：`Level1.unity`、`Level2.unity`

### 5.2 脚本与目录

- **类名**：PascalCase
- **Scripts 子目录**：Editor、Infrastructure、Logic、Presentation

---

## 六、使用方式

### 6.1 本仓库内体验

1. 用 Unity 打开本项目
2. 直接打开 `_Project1/Scenes/Level1.unity` 或 `_Project2/Scenes/Level2.unity`
3. 或从 Build Settings 中运行 `_Project/Scenes/Level2.unity`（主入口）

### 6.2 复制到自己的项目

1. 选定想要的关卡（_Project1 或 _Project2）
2. 将整个 `Assets/_ProjectN` 文件夹复制到目标项目的 `Assets/` 下
3. 打开对应场景即可使用，**无需**复制 _Project 或其他 _Project

### 6.3 快速搭建新关卡

**ControlHandoff（_Project1）**

1. 打开 `Scenes/Level1.unity`
2. 添加 CharacterControlManager
3. 3D 角色挂 PlayerMovement3D_ControlHandoff，2D 角色挂 PlayerMovement2D_ControlHandoff
4. 触发器挂 ControlHandoffTrigger（Collider 勾选 Is Trigger）

**DimensionSwitch（_Project2）**

1. 打开 `Scenes/Level2.unity`
2. 拖入 `Prefabs/GameSetup_DimensionSwitch`（推荐），或 `Prefabs/GameController_DimensionSwitch` 手动搭建
3. 根物体添加 DimensionSwitchController，引用 VCam2D、VCam3D
4. 主角使用 `Prefabs/Player_DimensionSwitch`
5. 平台添加 DimensionColliderSwitch

---

## 七、Build Settings

当前已配置场景：

- `Assets/_Project2/Scenes/Level2.unity`
- `Assets/_Project1/Scenes/Level1.unity`

---

## 八、规划与待办

- [ ] MainMenu 主菜单场景
- [ ] Boot 启动逻辑
- [ ] _Project1 的 GameSetup 预制体（打包 CharacterControlManager + Input 等）
- [x] _Project2 的 GameSetup_DimensionSwitch 预制体（已实现）

---

## 九、参考

- [Assets/README.md](Assets/README.md) - Assets 结构说明
- [Assets/_Project1/README.md](Assets/_Project1/README.md) - ControlHandoff 详细说明
- [Assets/_Project2/README.md](Assets/_Project2/README.md) - DimensionSwitch 详细说明
