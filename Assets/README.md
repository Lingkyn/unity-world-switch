# World Switch 合集

**_Project** 为关卡主入口；各 **_ProjectN** 为完全独立关卡，可单独复制到自己的项目中使用。

| 目录          | 说明                                | 场景         |
| ------------- | ----------------------------------- | ------------ |
| **_Project**  | 关卡主入口（主菜单、选关）          | —            |
| **_Project1** | 3D/2D 双角色控制权切换              | Level1.unity |
| **_Project2** | 2D/3D 视角切换（正交↔透视）        | Level2.unity |

## 结构说明（_Project1 / _Project2 通用）

- **Art** - 美术资源（Animations、Materials、Models、Shaders、Sprites、Textures、VFX）
- **Audio** - BGM、SFX、UI
- **Data** - Config、ScriptableObjects、Localization
- **Prefabs** - 多项目共用资源加 _ControlHandoff/_DimensionSwitch 后缀（如 Player_ControlHandoff）
