# _Project2：2D/3D 视角切换

Tab 键切换正交（2D）与透视（3D）相机、移动轴向及碰撞。**复制本文件夹即可**，无其他 _Project 依赖。

## 结构

- Art/ - 美术资源
- Audio/ - 音频
- Data/ - 可选数据
- Prefabs/ - GameSetup_DimensionSwitch、GameController_DimensionSwitch、CameraRig_DimensionSwitch、Player_DimensionSwitch、VCam2D、VCam3D
- Scenes/ - Level2.unity
- Scripts/ - Presentation, Logic, Infrastructure, Editor

## 依赖

- Cinemachine
- New Input System

## 使用

**推荐**：拖入 `Prefabs/GameSetup_DimensionSwitch` 即可（含 GameController、Player、CameraRig）。

**手动搭建**：

1. 打开 Scenes/Level2.unity
2. 拖入 Prefabs/GameController_DimensionSwitch（含 InputAdapter + PlayerInput + GameController）
3. 根物体添加 DimensionSwitchController，引用 VCam2D、VCam3D
4. 主角使用 Prefabs/Player_DimensionSwitch，挂 CharacterController + PlayerMovement_DimensionSwitch
5. 平台添加 DimensionColliderSwitch（两个 BoxCollider）

参数均在组件 Inspector 中配置。
