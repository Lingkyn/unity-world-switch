# Scripts

## 目录结构

```
Scripts/
├── App/
│   └── GameBootstrap.cs          # 游戏启动入口，初始化服务与场景
├── Presentation/
│   ├── Camera/
│   │   ├── CameraFollow.cs       # 相机跟随目标物体
│   │   ├── CameraShake.cs        # 相机震动效果
│   │   └── CameraSwitch.cs       # 相机切换
│   ├── Views/
│   ├── Viewbinder/
│   ├── Input/
│   │   └── InputAdapter.cs       # 输入适配，将 PlayerInput 转为逻辑层可用数据
│   ├── Interaction/
│   └── GameController/
├── Logic/
│   ├── Gameplay/
│   └── Managers/
│       ├── GameFlow.cs           # 游戏流程控制
│       ├── GameState.cs          # 游戏状态管理
│       ├── SceneFlow.cs          # 场景加载与流转
│       └── UIFlow.cs             # UI 流程与栈管理
├── Infrastructure/
│   ├── Core/
│   │   ├── ServiceLocator.cs     # 服务定位器，获取各 Service
│   │   └── TimeService.cs       # 时间服务（暂停、倍速等）
│   ├── Services/
│   │   ├── EventBus/
│   │   │   └── EventBus.cs      # 事件总线，松耦合发布订阅
│   │   ├── AssetService/
│   │   │   └── AssetService.cs  # 资源加载服务
│   │   ├── Audio/
│   │   │   └── AudioService.cs  # 音效与 BGM 播放
│   │   ├── Persistence/
│   │   │   └── SaveService.cs   # 存档读写
│   │   ├── Logging/
│   │   │   └── LogService.cs    # 日志输出
│   │   ├── Pooling/
│   │   │   └── ObjectPool.cs    # 对象池，复用 GameObject 减少实例化开销
│   │   └── Loading/
│   │       └── LoadingService.cs # 异步场景加载与进度反馈
│   └── DataAccess/
│       ├── Config/
│       └── Localization/
├── Common/          # 各层都会用到的公共代码
│   ├── Constants.cs  # 公共常量
│   ├── Layers.cs     # Layer 常量
│   ├── Tags.cs       # Tag 常量
│   └── Extensions.cs # 扩展方法
└── Editor/
    ├── CreateDefaultConfigs.cs   # 创建默认配置 ScriptableObject
    ├── FindMissingReferences.cs  # 查找场景中缺失引用
    ├── SceneSetup.cs             # 场景快速搭建工具
    └── ProjectValidator.cs       # 项目校验（引用、配置等）
```

## 分层说明

| 目录               | 说明                             |
| ------------------ | -------------------------------- |
| **App**            | 应用入口与引导                   |
| **Presentation**   | 表现层：相机、视图、输入、交互等 |
| **Logic**          | 逻辑层：玩法、管理器             |
| **Infrastructure** | 基础设施：服务、配置、数据访问   |
| **Common**         | 各层通用公共代码                 |
| **Editor**         | 编辑器工具                       |
