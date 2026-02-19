# _Project1：3D/2D 双角色控制权切换

3D 角色与 2D 角色在区域内交替响应输入。**复制本文件夹即可**，无其他 _Project 依赖。

## 结构

- Art/ - 美术资源（材质 Player2D/Player3D、Ground、Switch）
- Audio/ - 音频
- Data/ - SampleSceneProfile_ControlHandoff（Volume 配置）
- Prefabs/ - 规划：Player_ControlHandoff、GameSetup_ControlHandoff
- Scenes/ - Level1.unity
- Scripts/ - Presentation, Logic, Infrastructure, Editor

## 依赖

- New Input System

## 使用

1. 打开 Scenes/Level1.unity
2. 场景添加 CharacterControlManager
3. 3D 角色挂 PlayerMovement3D_ControlHandoff，2D 角色挂 PlayerMovement2D_ControlHandoff
4. 触发器挂 ControlHandoffTrigger（Collider 勾选 Is Trigger）
5. 3D 进入 → 仅 2D 动；2D 进入 → 一起动

参数均在组件 Inspector 中配置。
