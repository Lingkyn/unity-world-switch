# World Switch

**World Switch** 是一个 Unity 关卡机制**模板库**，提供多种 world switch 机制的可复用实现。选定需要的关卡后，可将对应的 _ProjectN 文件夹复制到自己的 Unity 项目中使用；亦可直接打开场景体验效果。

每个 _ProjectN 关卡**完全独立**，复制时无需依赖 _Project 或其他 _Project。

---

## 项目结构

| 目录          | 机制                   | 场景         | 简介                                  |
| ------------- | ---------------------- | ------------ | ------------------------------------- |
| **_Project**  | 关卡主入口             | —            | 主菜单选关后进入对应关卡              |
| **_Project1** | 3D/2D 双角色控制权切换 | Level1.unity | 3D 角色与 2D 角色在区域内交替响应输入 |
| **_Project2** | 2D/3D 视角切换         | Level2.unity | Tab 切换正交/透视相机、移动轴向及碰撞 |

详见 [Assets/README.md](Assets/README.md)。

---

## 依赖

| 依赖             | 用途 | 使用项目             |
| ---------------- | ---- | -------------------- |
| Unity 2022+      | 引擎 | 全部                 |
| URP              | 渲染 | 全部                 |
| New Input System | 输入 | _Project1、_Project2 |
| Cinemachine      | 相机 | _Project2            |

---

## 快速开始

### 复制到自己的项目

1. 选定需要的关卡（_Project1 或 _Project2）
2. 将整个 `Assets/_ProjectN` 文件夹复制到目标项目的 `Assets/` 下
3. 打开对应场景即可使用，**无需**复制 _Project 或其他 _Project

### 本仓库内体验

用 Unity 打开本项目，打开 `Assets/_Project1/Scenes/Level1.unity` 或 `Assets/_Project2/Scenes/Level2.unity` 可直接体验效果。

---

## 快速搭建新关卡

### ControlHandoff（_Project1）

1. 打开 `Scenes/Level1.unity`
2. 添加 CharacterControlManager
3. 3D 角色挂 PlayerMovement3D_ControlHandoff，2D 角色挂 PlayerMovement2D_ControlHandoff
4. 触发器挂 ControlHandoffTrigger（Collider 勾选 Is Trigger）

### DimensionSwitch（_Project2）

1. 打开 `Scenes/Level2.unity`
2. 拖入 `Prefabs/GameSetup_DimensionSwitch`（推荐），或 `Prefabs/GameController_DimensionSwitch` 手动搭建
3. 根物体添加 DimensionSwitchController，引用 VCam2D、VCam3D
4. 主角使用 `Prefabs/Player_DimensionSwitch`
5. 平台添加 DimensionColliderSwitch