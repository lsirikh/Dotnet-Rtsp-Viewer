# Dotnet.Rtsp.Viewer.Ui - 프로젝트 매뉴얼

## 📋 목차

1. [프로젝트 개요](#1-프로젝트-개요)
2. [기술 스택](#2-기술-스택)
3. [프로젝트 구조](#3-프로젝트-구조)
4. [아키텍처 설계](#4-아키텍처-설계)
5. [핵심 컴포넌트](#5-핵심-컴포넌트)
6. [데이터 흐름](#6-데이터-흐름)
7. [데이터베이스 스키마](#7-데이터베이스-스키마)
8. [코드 연결성 맵](#8-코드-연결성-맵)
9. [주요 기능](#9-주요-기능)
10. [Redis 메시지 포맷](#10-redis-메시지-포맷)
11. [설정 및 구성](#11-설정-및-구성)
12. [빌드 및 배포](#12-빌드-및-배포)
13. [트러블슈팅](#13-트러블슈팅)

---

## 1. 프로젝트 개요

### 1.1 프로젝트 정보

| 항목 | 내용 |
|------|------|
| **프로젝트명** | Dotnet.Rtsp.Viewer.Ui |
| **개발사** | Sensorway Co., Ltd. |
| **프로젝트 타입** | WPF Desktop Application |
| **타겟 프레임워크** | .NET 8.0 (Windows 7.0+) |
| **개발 언어** | C# 12 |
| **목적** | RTSP 스트리밍 카메라 이벤트 기반 팝업 뷰어 |

### 1.2 프로젝트 목표

- **실시간 RTSP 스트리밍**: 다중 카메라 스트림 동시 처리
- **이벤트 기반 팝업**: Redis 메시지 기반 자동 카메라 팝업 표시
- **데이터베이스 관리**: MySQL 기반 카메라 및 이벤트 설정 관리
- **성능 최적화**: 하드웨어 가속, 메모리 관리, 연결 풀링

### 1.3 주요 특징

- ✅ **MVVM 패턴**: Caliburn.Micro 기반 깔끔한 아키텍처
- ✅ **의존성 주입**: Autofac을 통한 IoC 컨테이너 관리
- ✅ **Redis Pub/Sub**: 이벤트 기반 실시간 카메라 팝업
- ✅ **MySQL 데이터베이스**: 카메라 및 이벤트 설정 영구 저장
- ✅ **Material Design**: 현대적인 UI/UX
- ✅ **다중 카메라 지원**: 최대 17개 동시 스트리밍

---

## 2. 기술 스택

### 2.1 프레임워크 및 라이브러리

| 카테고리 | 기술 | 버전 | 용도 |
|----------|------|------|------|
| **UI Framework** | WPF | .NET 8.0 | Windows Desktop UI |
| **MVVM Framework** | Caliburn.Micro | 5.0.258 | MVVM 패턴 구현 |
| **DI Container** | Autofac | 8.4.0 | 의존성 주입 |
| **ORM** | Dapper | 2.1.66 | 데이터베이스 액세스 |
| **Database** | MySQL | 9.3.0 | 데이터 저장소 |
| **Message Queue** | Redis | - | Pub/Sub 메시징 |
| **UI Library** | MaterialDesignThemes | - | Material Design UI |
| **Behavior** | Microsoft.Xaml.Behaviors.Wpf | 1.1.135 | XAML 행동 |
| **Configuration** | Microsoft.Extensions.Configuration | 9.0.9 | 설정 관리 |

### 2.2 외부 라이브러리 (Ironwall.Dotnet.Libraries)

| 라이브러리 | 용도 |
|------------|------|
| **Ironwall.Dotnet.Framework** | 기본 프레임워크 유틸리티 |
| **Ironwall.Dotnet.Libraries.Base** | 기본 서비스 및 인터페이스 |
| **Ironwall.Dotnet.Libraries.Streaming** | RTSP 스트리밍 핵심 기능 |
| **Ironwall.Dotnet.Libraries.Streaming.Base** | 스트리밍 추상화 레이어 |
| **Ironwall.Dotnet.Libraries.Redis** | Redis 클라이언트 통합 |
| **Ironwall.Dotnet.Libraries.ViewModel** | 기본 ViewModel 클래스 |
| **Ironwall.Dotnet.Libraries.Utils** | 유틸리티 함수 |
| **Ironwall.Dotnet.Libraries.Enums** | 열거형 정의 |

---

## 3. 프로젝트 구조

### 3.1 디렉토리 구조

```
Dotnet.Rtsp.Viewer.Ui/
│
├── 📁 Behaviors/                      # WPF Attached Behaviors
│   ├── CameraDeviceSelectedItemsBehavior.cs
│   └── CameraEventSelectedItemsBehavior.cs
│
├── 📁 Commands/                       # ICommand 구현
│   ├── RelayCommand.cs               # 동기 Command
│   └── AsyncRelayCommand.cs          # 비동기 Command
│
├── 📁 Models/                         # 데이터 모델
│   ├── SetupModel.cs                 # 설정 모델
│   ├── MessageModel.cs               # 메시지 래퍼
│   └── IMessageModel.cs              # 메시지 인터페이스
│
├── 📁 Services/                       # 비즈니스 로직
│   ├── CameraDbService.cs            # 카메라 DB 서비스
│   ├── ICameraDbService.cs
│   ├── EventDbService.cs             # 이벤트 DB 서비스
│   ├── IEventDbService.cs
│   ├── EventService.cs               # 이벤트 처리 서비스
│   └── IEventService.cs
│
├── 📁 Tests/                          # 단위 테스트
│   ├── CameraDbUnitTest.cs
│   └── EventDbUnitTest.cs
│
├── 📁 ViewModels/                     # MVVM ViewModel
│   ├── PopupShellViewModel.cs        # 메인 Shell VM (894줄)
│   │
│   ├── 📁 Items/                      # 아이템 VM
│   │   ├── CameraDeviceViewModel.cs
│   │   ├── ICameraDeviceViewModel.cs
│   │   ├── CameraEventViewModel.cs
│   │   └── ICameraEventViewModel.cs
│   │
│   └── 📁 Panels/                     # 패널 VM
│       ├── CameraDevicePanelViewModel.cs
│       └── CameraEventPanelViewModel.cs
│
├── 📁 Views/                          # XAML UI
│   ├── PopupShellView.xaml           # 메인 Shell View (12,707줄)
│   ├── PopupShellView.xaml.cs
│   │
│   └── 📁 Panels/
│       ├── CameraDevicePanelView.xaml
│       ├── CameraDevicePanelView.xaml.cs
│       ├── CameraEventPanelView.xaml
│       └── CameraEventPanelView.xaml.cs
│
├── 📁 Resources/
│   └── ResourceDictionary.xaml       # 공유 스타일
│
├── 📄 App.xaml                        # 애플리케이션 리소스
├── 📄 App.xaml.cs                     # 애플리케이션 진입점
├── 📄 Bootstrapper.cs                # DI 설정 (168줄)
├── 📄 AssemblyInfo.cs                # 어셈블리 메타데이터
├── 📄 appsettings.json               # 애플리케이션 설정
└── 📄 Dotnet.Rtsp.Viewer.Ui.csproj   # 프로젝트 파일
```

### 3.2 파일 통계

| 카테고리 | 파일 수 | 총 라인 수 (추정) |
|----------|---------|-------------------|
| ViewModels | 7개 | ~1,500줄 |
| Services | 6개 | ~1,800줄 |
| Models | 3개 | ~250줄 |
| Commands | 2개 | ~200줄 |
| Views (XAML) | 5개 | ~13,500줄 |
| Behaviors | 2개 | ~200줄 |
| Configuration | 3개 | ~150줄 |
| **총계** | **28개** | **~17,600줄** |

---

## 4. 아키텍처 설계

### 4.1 전체 시스템 아키텍처

```
┌─────────────────────────────────────────────────────────────┐
│                    Presentation Layer                        │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐      │
│  │ PopupShell   │  │ CameraDevice │  │ CameraEvent  │      │
│  │   View       │  │  PanelView   │  │  PanelView   │      │
│  └──────┬───────┘  └──────┬───────┘  └──────┬───────┘      │
│         │ DataBinding     │                  │              │
│  ┌──────▼───────┐  ┌──────▼───────┐  ┌──────▼───────┐      │
│  │ PopupShell   │  │ CameraDevice │  │ CameraEvent  │      │
│  │  ViewModel   │  │PanelViewModel│  │PanelViewModel│      │
│  └──────┬───────┘  └──────┬───────┘  └──────┬───────┘      │
└─────────┼──────────────────┼──────────────────┼─────────────┘
          │                  │                  │
┌─────────▼──────────────────▼──────────────────▼─────────────┐
│                    Business Logic Layer                      │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐      │
│  │   Event      │  │  CameraDb    │  │  EventDb     │      │
│  │  Service     │  │   Service    │  │   Service    │      │
│  └──────┬───────┘  └──────┬───────┘  └──────┬───────┘      │
│         │                  │                  │              │
│  ┌──────▼───────┐  ┌──────▼───────┐  ┌──────▼───────┐      │
│  │   Redis      │  │   Dapper     │  │   Dapper     │      │
│  │  Service     │  │     ORM      │  │     ORM      │      │
│  └──────┬───────┘  └──────┬───────┘  └──────┬───────┘      │
└─────────┼──────────────────┼──────────────────┼─────────────┘
          │                  │                  │
┌─────────▼──────────────────▼──────────────────▼─────────────┐
│                      Data Access Layer                       │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐      │
│  │    Redis     │  │    MySQL     │  │    MySQL     │      │
│  │  Pub/Sub     │  │   Cameras    │  │   Events     │      │
│  └──────────────┘  └──────────────┘  └──────────────┘      │
└─────────────────────────────────────────────────────────────┘
          │                  │                  │
┌─────────▼──────────────────▼──────────────────▼─────────────┐
│                    External Systems                          │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐      │
│  │    Redis     │  │    MySQL     │  │  RTSP Camera │      │
│  │   Server     │  │   Database   │  │   Streams    │      │
│  └──────────────┘  └──────────────┘  └──────────────┘      │
└─────────────────────────────────────────────────────────────┘
```

### 4.2 MVVM 패턴 아키텍처

```
┌─────────────────────────────────────────────────────────────┐
│                         View (XAML)                          │
│  - PopupShellView.xaml                                       │
│  - CameraDevicePanelView.xaml                               │
│  - CameraEventPanelView.xaml                                │
│                                                               │
│  ┌─────────────────────────────────────────────────────┐    │
│  │  UI Controls (Buttons, TextBoxes, ListBoxes, etc.)  │    │
│  └───────────────────┬─────────────────────────────────┘    │
└────────────────────┬─┴──────────────────────────────────────┘
                     │ DataBinding
                     │ Command Binding
                     │ Event Binding
┌────────────────────▼────────────────────────────────────────┐
│                      ViewModel Layer                         │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  PopupShellViewModel                                 │   │
│  │  - Properties (StatusMessages, IsProcessing, etc.)   │   │
│  │  - Commands (ShowSingleCamera, ShowDoubleCamera)     │   │
│  │  - Methods (GetNextCamera, CloneCameraModel)         │   │
│  └─────────────────┬────────────────────────────────────┘   │
│                    │ Uses Services                           │
│  ┌─────────────────▼────────────────────────────────────┐   │
│  │  Dependencies:                                        │   │
│  │  - IPopupViewerService                               │   │
│  │  - IImprovedRtspStreamingService                     │   │
│  │  - CameraDeviceProvider                              │   │
│  │  - StreamingSetupModel                               │   │
│  │  - IWindowManager                                     │   │
│  └─────────────────┬────────────────────────────────────┘   │
└────────────────────┴─────────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────────┐
│                      Model Layer                             │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  CameraModel                                         │   │
│  │  - Id, Guid, Title, AutoPlay, ShowControls           │   │
│  │  - ConnectionInfo (RtspConnectionInfo)               │   │
│  │  - StreamingOptions (StreamingOptions)               │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                               │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  EventMessage                                        │   │
│  │  - EventId, EventGroup, Cmd (EnumPopupCmd)           │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                               │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  SetupModel                                          │   │
│  │  - Database Config, Redis Config, Streaming Config   │   │
│  └──────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────────┐
│                    Service Layer                             │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  CameraDbService                                     │   │
│  │  - CRUD operations for cameras                       │   │
│  │  - MySQL connection management                       │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                               │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  EventService                                        │   │
│  │  - Redis event processing                            │   │
│  │  - Popup management                                   │   │
│  │  - Event-to-camera mapping                           │   │
│  └──────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────────────────┐
│                     Data Layer                               │
│  ┌──────────────────┐  ┌──────────────────┐                │
│  │  MySQL Database  │  │  Redis Server    │                │
│  │  - Cameras       │  │  - Pub/Sub       │                │
│  │  - ConnectionInfo│  │  - Messaging     │                │
│  │  - Options       │  │                  │                │
│  └──────────────────┘  └──────────────────┘                │
└─────────────────────────────────────────────────────────────┘
```

### 4.3 의존성 주입 아키텍처

```
┌─────────────────────────────────────────────────────────────┐
│                    Bootstrapper.cs                           │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  ConfigureContainer(ContainerBuilder builder)        │   │
│  │                                                       │   │
│  │  1. Load appsettings.json                            │   │
│  │  2. Register SetupModel                              │   │
│  │  3. Register ViewModels                              │   │
│  │  4. Register Services (with Order metadata)          │   │
│  │  5. Register Modules                                 │   │
│  └───────────────────┬──────────────────────────────────┘   │
└─────────────────────┬┴──────────────────────────────────────┘
                      │
        ┌─────────────▼─────────────┐
        │   Autofac Container       │
        │   (IoC Container)         │
        └─────────────┬─────────────┘
                      │
        ┌─────────────┴─────────────┐
        │                           │
┌───────▼────────┐         ┌────────▼─────────┐
│   Services     │         │   ViewModels     │
│  (Order: 1-3)  │         │   (Singleton)    │
└───────┬────────┘         └────────┬─────────┘
        │                           │
┌───────▼────────┐         ┌────────▼─────────┐
│ CameraDbService│         │ PopupShellVM     │
│ (Order: 1)     │         │                  │
└───────┬────────┘         └────────┬─────────┘
        │                           │
┌───────▼────────┐         ┌────────▼─────────┐
│ EventDbService │         │ CameraDevicePanelVM
│ (Order: 2)     │         │                  │
└───────┬────────┘         └────────┬─────────┘
        │                           │
┌───────▼────────┐         ┌────────▼─────────┐
│ EventService   │         │ CameraEventPanelVM
│ (Order: 3)     │         │                  │
└────────────────┘         └──────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                     Modules                                  │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  StreamingBaseModule (Order: 10)                     │   │
│  │  - Base streaming components                         │   │
│  │  - CameraDeviceProvider                              │   │
│  │  - CameraEventProvider                               │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                               │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  StreamingModule (Order: 10)                         │   │
│  │  - IImprovedRtspStreamingService                     │   │
│  │  - IPopupViewerService                               │   │
│  │  - StreamingSetupModel                               │   │
│  └──────────────────────────────────────────────────────┘   │
│                                                               │
│  ┌──────────────────────────────────────────────────────┐   │
│  │  RedisModule (Order: 20)                             │   │
│  │  - IRedisService                                     │   │
│  │  - Redis client configuration                        │   │
│  └──────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────┘
```

---

## 5. 핵심 컴포넌트

### 5.1 Bootstrapper.cs - 애플리케이션 부트스트랩

**파일**: [Dotnet.Rtsp.Viewer.Ui/Bootstrapper.cs](Dotnet.Rtsp.Viewer.Ui/Bootstrapper.cs) (168줄)

#### 책임 (Responsibilities)
- ✅ Caliburn.Micro 초기화
- ✅ Autofac DI 컨테이너 구성
- ✅ appsettings.json 로드
- ✅ 서비스, ViewModel, 모듈 등록
- ✅ 중복 실행 방지

#### 주요 메서드

```csharp
protected override void Configure()
```
- `appsettings.json` 로드
- `IConfigurationRoot` 초기화

```csharp
protected override void ConfigureContainer(ContainerBuilder builder)
```
- `SetupModel` 등록 및 바인딩
- ViewModels 등록 (Singleton)
- Services 등록 (Metadata Order 포함)
- Modules 등록 (StreamingBaseModule, StreamingModule, RedisModule)

```csharp
protected override void OnStartup(object sender, StartupEventArgs e)
```
- 중복 프로세스 검사
- 중복 실행 시 경고 및 종료

```csharp
protected override void StartPrograme()
```
- `PopupShellViewModel` 표시

#### 서비스 등록 순서

| Order | Service | 용도 |
|-------|---------|------|
| 1 | CameraDbService | 카메라 DB 초기화 |
| 2 | EventDbService | 이벤트 DB 초기화 |
| 3 | EventService | 이벤트 처리 시작 |

#### 의존성 관계

```
Bootstrapper
  ├── IConfigurationRoot (appsettings.json)
  ├── SetupModel
  ├── ViewModels
  │   ├── PopupShellViewModel
  │   ├── CameraDevicePanelViewModel
  │   └── CameraEventPanelViewModel
  ├── Services
  │   ├── CameraDbService (Order: 1)
  │   ├── EventDbService (Order: 2)
  │   └── EventService (Order: 3)
  └── Modules
      ├── StreamingBaseModule
      ├── StreamingModule
      └── RedisModule
```

---

### 5.2 PopupShellViewModel.cs - 메인 Shell ViewModel

**파일**: [Dotnet.Rtsp.Viewer.Ui/ViewModels/PopupShellViewModel.cs](Dotnet.Rtsp.Viewer.Ui/ViewModels/PopupShellViewModel.cs) (894줄)

#### 책임 (Responsibilities)
- ✅ 메인 UI 상태 관리
- ✅ 카메라 팝업 표시 제어
- ✅ 스트리밍 설정 관리
- ✅ 사용자 명령 처리
- ✅ 상태 메시지 로깅

#### 주요 프로퍼티

| 프로퍼티 | 타입 | 설명 |
|----------|------|------|
| `StatusMessages` | `ObservableCollection<string>` | 상태 메시지 (최대 10개) |
| `IsProcessing` | `bool` | 처리 중 플래그 |
| `DisplayPositions` | `List<EnumDisplayPosition>` | 팝업 위치 목록 |
| `SelectedPosition` | `EnumDisplayPosition` | 선택된 팝업 위치 |
| `IsAutoDiscard` | `bool` | 자동 제거 활성화 |
| `TimeoutSeconds` | `int` | 타임아웃 시간 (초) |

#### 주요 Commands

| Command | 설명 | 타입 |
|---------|------|------|
| `ShowSingleCameraCommand` | 단일 카메라 표시 | AsyncRelayCommand |
| `ShowDoubleCameraCommand` | 2개 카메라 표시 | AsyncRelayCommand |
| `ShowTripleCameraCommand` | 3개 카메라 표시 | AsyncRelayCommand |
| `AddRandomCameraCommand` | 랜덤 카메라 추가 | AsyncRelayCommand |
| `ClosePopupCommand` | 팝업 닫기 | RelayCommand |
| `ChangePositionCommand` | 팝업 위치 변경 | RelayCommand<EnumDisplayPosition> |
| `ApplyStreamingSettingsCommand` | 스트리밍 설정 적용 | RelayCommand |
| `ResetStreamingSettingsCommand` | 스트리밍 설정 초기화 | RelayCommand |
| `OpenCameraDevicePanelCommand` | 카메라 설정 창 열기 | AsyncRelayCommand |
| `OpenCameraEventPanelCommand` | 이벤트 설정 창 열기 | AsyncRelayCommand |

#### 핵심 메서드

```csharp
private ICameraModel? GetNextCameraFromProvider()
```
- CameraProvider에서 다음 카메라 가져오기 (순환)
- 인덱스 자동 순환

```csharp
private CameraModel CloneCameraModel(ICameraModel original)
```
- DB 카메라 모델 복제
- 새 Guid 생성 (팝업용 고유 ID)
- ConnectionInfo 및 StreamingOptions 복제

```csharp
private void ApplyStreamingSettings()
```
- 유효성 검사 (5초 ~ 300초)
- SetupModel에 적용
- PropertyChanged 알림

#### 의존성

```
PopupShellViewModel
  ├── ILogService
  ├── IWindowManager (Caliburn.Micro)
  ├── IPopupViewerService
  ├── IImprovedRtspStreamingService
  ├── CameraDeviceProvider
  └── StreamingSetupModel
```

---

### 5.3 CameraDbService.cs - 카메라 데이터베이스 서비스

**파일**: [Dotnet.Rtsp.Viewer.Ui/Services/CameraDbService.cs](Dotnet.Rtsp.Viewer.Ui/Services/CameraDbService.cs) (727줄)

#### 책임 (Responsibilities)
- ✅ MySQL 데이터베이스 연결 관리
- ✅ 카메라 스키마 생성 및 초기화
- ✅ 카메라 CRUD 작업
- ✅ 트랜잭션 관리
- ✅ CameraProvider 동기화

#### 데이터베이스 테이블 구조

##### 1. Cameras 테이블 (메인)
```sql
CREATE TABLE `Cameras` (
    `Id`             INT AUTO_INCREMENT PRIMARY KEY,
    `Guid`           VARCHAR(36) NOT NULL UNIQUE,
    `Title`          VARCHAR(255),
    `AutoPlay`       BOOLEAN DEFAULT FALSE,
    `ShowControls`   BOOLEAN DEFAULT TRUE,
    `CreatedAt`      DATETIME DEFAULT CURRENT_TIMESTAMP,
    `UpdatedAt`      DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX `idx_guid` (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
```

##### 2. CameraConnectionInfo 테이블 (연결 정보)
```sql
CREATE TABLE `CameraConnectionInfo` (
    `Id`             INT AUTO_INCREMENT PRIMARY KEY,
    `CameraId`       INT NOT NULL UNIQUE,
    `Url`            VARCHAR(255),
    `Username`       VARCHAR(100),
    `Password`       VARCHAR(255),
    `IpAddress`      VARCHAR(45),
    `Port`           INT DEFAULT 554,
    `Protocol`       VARCHAR(10) DEFAULT 'rtsp',
    `StreamPath`     VARCHAR(255),
    `ChannelId`      VARCHAR(50),
    `Description`    VARCHAR(255),
    `CameraName`     VARCHAR(100),
    `Location`       VARCHAR(100),
    `StreamType`     INT DEFAULT 0,
    `IsEnabled`      BOOLEAN DEFAULT TRUE,
    CONSTRAINT `FK_CameraConnection_Camera`
        FOREIGN KEY (`CameraId`) REFERENCES `Cameras` (`Id`)
        ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
```

##### 3. CameraStreamingOptions 테이블 (스트리밍 옵션)
```sql
CREATE TABLE `CameraStreamingOptions` (
    `Id`                        INT AUTO_INCREMENT PRIMARY KEY,
    `CameraId`                  INT NOT NULL UNIQUE,
    `NetworkCaching`            INT DEFAULT 300,
    `UseTcp`                    BOOLEAN DEFAULT TRUE,
    `FrameBufferSize`           INT DEFAULT 100000,
    `ConnectionTimeoutSeconds`  INT DEFAULT 10,
    `UseHardwareAcceleration`   BOOLEAN DEFAULT TRUE,
    `AllowFrameSkip`            BOOLEAN DEFAULT TRUE,
    -- (추가 옵션들...)
    CONSTRAINT `FK_CameraOptions_Camera`
        FOREIGN KEY (`CameraId`) REFERENCES `Cameras` (`Id`)
        ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
```

#### 주요 메서드

```csharp
public async Task Connect(CancellationToken token = default)
```
- DB 존재 확인 및 생성
- MySQL 연결 초기화
- EventAggregator 알림

```csharp
public async Task BuildSchemeAsync(CancellationToken token = default)
```
- 3개 테이블 생성 (IF NOT EXISTS)
- FK 제약 조건 설정
- 인덱스 생성

```csharp
public async Task<List<ICameraModel>?> FetchCamerasAsync(CancellationToken token = default)
```
- LEFT JOIN을 통한 3개 테이블 조회
- Dapper로 매핑
- `CameraJoinSQL` → `CameraModel` 변환

```csharp
public async Task<ICameraModel?> InsertCameraAsync(ICameraModel model, CancellationToken token = default)
```
- 트랜잭션 시작
- Cameras → CameraConnectionInfo → CameraStreamingOptions 순차 삽입
- LAST_INSERT_ID() 활용
- 실패 시 롤백

```csharp
public async Task<ICameraModel?> UpdateCameraAsync(ICameraModel model, CancellationToken token = default)
```
- 트랜잭션 시작
- 3개 테이블 동시 업데이트
- 실패 시 롤백

```csharp
public async Task<bool> DeleteCameraAsync(ICameraModel model, CancellationToken token = default)
```
- CASCADE 삭제 (자식 테이블 자동 삭제)
- Id 기반 삭제

#### 트랜잭션 흐름

```
InsertCameraAsync
  ├── BEGIN TRANSACTION
  ├── INSERT INTO Cameras → Get Id
  ├── INSERT INTO CameraConnectionInfo (CameraId = Id)
  ├── INSERT INTO CameraStreamingOptions (CameraId = Id)
  ├── COMMIT (성공)
  └── ROLLBACK (실패)
```

#### 의존성

```
CameraDbService
  ├── ILogService
  ├── IEventAggregator
  ├── CameraDeviceProvider
  ├── SetupModel
  └── MySqlConnection
```

---

### 5.4 EventService.cs - 이벤트 처리 서비스

**파일**: [Dotnet.Rtsp.Viewer.Ui/Services/EventService.cs](Dotnet.Rtsp.Viewer.Ui/Services/EventService.cs) (563줄)

#### 책임 (Responsibilities)
- ✅ Redis Pub/Sub 메시지 수신
- ✅ EventMessage 역직렬화
- ✅ 이벤트 → 카메라 매핑
- ✅ 팝업 표시/제거
- ✅ 이벤트 상태 추적

#### Redis 메시지 처리 흐름

```
Redis Channel
    ↓
RedisSubscribeEventAsync(MessageArgsModel)
    ↓
ProcessMessageAsync(IMessageModel)
    ↓
MessageSelector(channel, message, settings)
    ↓
JSON Deserialization → EventMessage
    ↓
Switch (EventMessage.Cmd)
    ├── OPEN_POPUP_CAMERAS → OnEventReceivedAsync()
    └── CLOSE_POPUP_CAMERAS → OnEventResolved()
```

#### 주요 메서드

```csharp
private Task RedisSubscribeEventAsync(MessageArgsModel model)
```
- Redis 메시지 수신
- JSON 역직렬화 (StringEnumConverter)
- `ProcessMessageAsync()` 호출

```csharp
private async Task MessageSelector(string channel, string message, JsonSerializerSettings settings)
```
- SemaphoreSlim으로 동시 접근 제어
- EventMessage 역직렬화
- Command 분기 처리

```csharp
public async Task<string?> OnEventReceivedAsync(EventMessage eventMessage)
```
1. EventGroup + Cmd로 DB에서 이벤트 설정 조회
2. 이미 활성화된 이벤트인지 확인
3. CameraGuid 목록으로 실제 카메라 조회
4. `IPopupViewerService.ShowPopup()` 호출
5. EventId → RowId 매핑 저장

```csharp
public bool OnEventResolved(string eventId, bool autoDiscard = false)
```
1. EventId로 RowId 조회
2. `IPopupViewerService.RemoveRowById()` 호출
3. 매핑 삭제
4. 활성 이벤트 카운트 업데이트

```csharp
private ICameraEventModel? FindEventConfigByGroupAndCommand(int eventGroup, EnumPopupCmd command)
```
- CameraEventProvider 검색
- EventGroup과 Command로 필터링
- CameraGuids 포함된 설정 반환

```csharp
private ICameraModel[] GetCamerasByGuids(List<string> cameraGuids)
```
- CameraDeviceProvider 검색
- Guid 매칭
- 유효한 카메라만 반환

#### 이벤트 상태 관리

```csharp
private readonly Dictionary<string, string> _eventRowMapping = new();
private readonly Dictionary<string, string> _eventNameMapping = new();
```

| 딕셔너리 | Key | Value | 용도 |
|----------|-----|-------|------|
| `_eventRowMapping` | EventId | RowId | 이벤트 → 팝업 Row 매핑 |
| `_eventNameMapping` | EventId | EventName | 이벤트 → 이름 매핑 |

#### 동시성 제어

```csharp
private SemaphoreSlim semaphore = new SemaphoreSlim(1);
```
- MessageSelector에서 사용
- 동시에 1개의 스레드만 처리
- 메시지 순서 보장

#### 의존성

```
EventService
  ├── ILogService
  ├── IEventAggregator
  ├── IPopupViewerService
  ├── IRedisService
  ├── CameraEventProvider
  └── CameraDeviceProvider
```

---

### 5.5 SetupModel.cs - 설정 모델

**파일**: [Dotnet.Rtsp.Viewer.Ui/Models/SetupModel.cs](Dotnet.Rtsp.Viewer.Ui/Models/SetupModel.cs) (54줄)

#### 인터페이스 구현

```csharp
internal class SetupModel : IRedisSetupModel, IStreamingSetupModel
```

#### 설정 카테고리

##### 1. 데이터베이스 설정
```csharp
public string IpDbServer { get; set; } = "localhost";
public int PortDbServer { get; set; } = 3306;
public string UidDbServer { get; set; } = "root";
public string PasswordDbServer { get; set; }
public string DbDatabase { get; set; } = "streaming_DB";
```

##### 2. Redis 설정
```csharp
public string IpAddressRedis { get; set; } = string.Empty;
public int PortRedis { get; set; }
public string PasswordRedis { get; set; } = string.Empty;
public string NameChannel { get; set; } = string.Empty;
public string NameChannel2 { get; set; } = string.Empty;
```

##### 3. 스트리밍 연결 관리
```csharp
public int MaxConnections { get; set; } = 17;
public int MaxRetryAttempts { get; set; } = 5;
public int ContextPoolSize { get; set; } = 32;
```

##### 4. 메모리 관리
```csharp
public long MaxMemoryUsageBytes { get; set; } = 1024L * 1024 * 1024; // 1GB
public int MemoryCheckIntervalSeconds { get; set; } = 30;
public bool AutoCleanupInactiveStreams { get; set; } = true;
public int InactiveStreamTimeoutMinutes { get; set; } = 30;
```

##### 5. 성능 최적화
```csharp
public bool UseHardwareAcceleration { get; set; } = true;
public bool EnableFrameSkipping { get; set; } = true;
public int MaxFrameSkip { get; set; } = 5;
public int DefaultNetworkCaching { get; set; } = 300;
```

##### 6. 자동 제거 설정
```csharp
public bool IsAutoDiscard { get; set; } = true;
public int TimeoutSeconds { get; set; } = 10;
```

##### 7. 로깅 및 모니터링
```csharp
public bool EnableDebugLogging { get; set; } = false;
public string LogPath { get; set; } = "logs/streaming";
public string SnapshotPath { get; set; } = "snapshots/";
public bool EnablePerformanceMonitoring { get; set; } = true;
public int StatisticsUpdateIntervalMs { get; set; } = 1000;
```

---

## 6. 데이터 흐름

### 6.1 애플리케이션 시작 시퀀스

```
┌─────────────────────────────────────────────────────────────┐
│ 1. Application Startup                                       │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 2. App.xaml.cs → OnStartup()                                │
│    - Load appsettings.json                                   │
│    - Create IConfiguration                                   │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 3. Bootstrapper → Initialize()                              │
│    - Configure() → Load config again                         │
│    - ConfigureContainer() → Setup DI                         │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 4. Bootstrapper → OnStartup()                               │
│    - Check duplicate process                                 │
│    - Exit if duplicate found                                 │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 5. Bootstrapper → StartPrograme()                           │
│    - DisplayRootViewForAsync<PopupShellViewModel>()         │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 6. Service Initialization (by Order metadata)               │
│    ┌──────────────────────────────────────────────────────┐ │
│    │ CameraDbService (Order: 1)                           │ │
│    │   - Connect to MySQL                                 │ │
│    │   - Create database if not exists                    │ │
│    │   - BuildSchemeAsync() → Create tables               │ │
│    │   - FetchInstanceAsync() → Load cameras              │ │
│    │   - Populate CameraDeviceProvider                    │ │
│    └──────────────────┬───────────────────────────────────┘ │
│                       │                                      │
│    ┌──────────────────▼───────────────────────────────────┐ │
│    │ EventDbService (Order: 2)                            │ │
│    │   - Connect to MySQL                                 │ │
│    │   - BuildSchemeAsync() → Create events table         │ │
│    │   - FetchInstanceAsync() → Load event configs        │ │
│    │   - Populate CameraEventProvider                     │ │
│    └──────────────────┬───────────────────────────────────┘ │
│                       │                                      │
│    ┌──────────────────▼───────────────────────────────────┐ │
│    │ EventService (Order: 3)                              │ │
│    │   - Subscribe to Redis channels                      │ │
│    │   - Register event handlers                          │ │
│    │   - Ready to receive events                          │ │
│    └──────────────────────────────────────────────────────┘ │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 7. PopupShellViewModel → OnActivatedAsync()                │
│    - Check CameraProvider count                             │
│    - Display status message                                  │
│    - Initialize camera index                                 │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 8. PopupShellView → Displayed                               │
│    - Main UI visible                                         │
│    - Ready for user interaction                              │
└─────────────────────────────────────────────────────────────┘
```

### 6.2 Redis 이벤트 처리 시퀀스

```
┌─────────────────────────────────────────────────────────────┐
│ 1. External System publishes message to Redis               │
│    Channel: "StreamingChannel"                              │
│    Message: {                                                │
│      "EventId": "EV-2025-001",                              │
│      "EventGroup": 100,                                      │
│      "Cmd": "OPEN_POPUP_CAMERAS"                            │
│    }                                                         │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 2. IRedisService → RedisSubscribeEventAsync                │
│    - Receive MessageArgsModel                               │
│    - Extract channel and message                             │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 3. EventService → ProcessMessageAsync()                     │
│    - Create MessageModel wrapper                             │
│    - Call MessageSelector()                                  │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 4. EventService → MessageSelector()                         │
│    - Acquire SemaphoreSlim (thread-safe)                    │
│    - Deserialize JSON to EventMessage                        │
│    - Switch on Cmd                                           │
└───────────────────────┬─────────────────────────────────────┘
                        │
         ┌──────────────┴──────────────┐
         │                             │
┌────────▼───────────┐      ┌──────────▼──────────┐
│ OPEN_POPUP_CAMERAS │      │ CLOSE_POPUP_CAMERAS │
└────────┬───────────┘      └──────────┬──────────┘
         │                             │
┌────────▼───────────────────────────────────────────────────┐
│ 5a. OnEventReceivedAsync(eventMessage)                      │
│    ┌─────────────────────────────────────────────────────┐ │
│    │ Step 1: FindEventConfigByGroupAndCommand()         │ │
│    │   - Search CameraEventProvider                      │ │
│    │   - Filter by EventGroup + Cmd                      │ │
│    │   - Return ICameraEventModel                        │ │
│    └─────────────────┬───────────────────────────────────┘ │
│                      │                                      │
│    ┌─────────────────▼───────────────────────────────────┐ │
│    │ Step 2: Check IsEventActive(eventId)               │ │
│    │   - If already active → Return null                 │ │
│    └─────────────────┬───────────────────────────────────┘ │
│                      │                                      │
│    ┌─────────────────▼───────────────────────────────────┐ │
│    │ Step 3: Get CameraGuids from config                │ │
│    │   - eventConfig.CameraGuids                         │ │
│    └─────────────────┬───────────────────────────────────┘ │
│                      │                                      │
│    ┌─────────────────▼───────────────────────────────────┐ │
│    │ Step 4: GetCamerasByGuids(cameraGuids)             │ │
│    │   - Search CameraDeviceProvider                     │ │
│    │   - Match Guid                                      │ │
│    │   - Return ICameraModel[]                           │ │
│    └─────────────────┬───────────────────────────────────┘ │
│                      │                                      │
│    ┌─────────────────▼───────────────────────────────────┐ │
│    │ Step 5: Execute.OnUIThreadAsync()                   │ │
│    │   - IPopupViewerService.ShowPopup(eventId, cameras)│ │
│    │   - Get RowId from ListedCameraRowIds.Last()       │ │
│    └─────────────────┬───────────────────────────────────┘ │
│                      │                                      │
│    ┌─────────────────▼───────────────────────────────────┐ │
│    │ Step 6: Store mappings                              │ │
│    │   - _eventRowMapping[eventId] = rowId               │ │
│    │   - _eventNameMapping[eventId] = eventName          │ │
│    └─────────────────────────────────────────────────────┘ │
└────────────────────────┬────────────────────────────────────┘
                         │
         ┌───────────────┴───────────────┐
         │                               │
┌────────▼───────────┐      ┌────────────▼──────────┐
│ Popup Window       │      │ 5b. OnEventResolved() │
│ Displayed          │      │   - Get RowId         │
└────────────────────┘      │   - RemoveRowById()   │
                            │   - Remove mappings    │
                            └───────────────────────┘
```

### 6.3 카메라 표시 시퀀스 (수동)

```
┌─────────────────────────────────────────────────────────────┐
│ 1. User clicks "Show Single Camera" button                  │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 2. PopupShellView → Command Binding                         │
│    - ShowSingleCameraCommand.Execute()                       │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 3. PopupShellViewModel → ShowSingleCameraAsync()            │
│    ┌─────────────────────────────────────────────────────┐  │
│    │ Step 1: Set IsProcessing = true                     │  │
│    └─────────────────┬───────────────────────────────────┘  │
│                      │                                       │
│    ┌─────────────────▼───────────────────────────────────┐  │
│    │ Step 2: GetNextCameraFromProvider()                 │  │
│    │   - Read from CameraDeviceProvider                  │  │
│    │   - Circular index (_currentCameraIndex++)          │  │
│    │   - Return ICameraModel                             │  │
│    └─────────────────┬───────────────────────────────────┘  │
│                      │                                       │
│    ┌─────────────────▼───────────────────────────────────┐  │
│    │ Step 3: CloneCameraModel(original)                  │  │
│    │   - Generate new Guid (팝업용)                       │  │
│    │   - Clone ConnectionInfo                            │  │
│    │   - Clone StreamingOptions                          │  │
│    │   - Set AutoPlay = true                             │  │
│    │   - Set ShowControls = false                        │  │
│    └─────────────────┬───────────────────────────────────┘  │
│                      │                                       │
│    ┌─────────────────▼───────────────────────────────────┐  │
│    │ Step 4: IPopupViewerService.ShowPopup()            │  │
│    │   - Pass title and cloned camera                    │  │
│    └─────────────────┬───────────────────────────────────┘  │
│                      │                                       │
│    ┌─────────────────▼───────────────────────────────────┐  │
│    │ Step 5: AddStatus(message)                          │  │
│    │   - Insert at index 0                               │  │
│    │   - Keep max 10 messages                            │  │
│    └─────────────────┬───────────────────────────────────┘  │
│                      │                                       │
│    ┌─────────────────▼───────────────────────────────────┐  │
│    │ Step 6: Set IsProcessing = false                    │  │
│    └─────────────────────────────────────────────────────┘  │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 4. IPopupViewerService (from Streaming Library)             │
│    - Create popup window                                     │
│    - Initialize RTSP streaming context                       │
│    - Start playback                                          │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 5. IImprovedRtspStreamingService                            │
│    - Parse RTSP URL                                          │
│    - Connect to RTSP server                                  │
│    - Decode video stream                                     │
│    - Render frames to UI                                     │
└─────────────────────────────────────────────────────────────┘
```

### 6.4 데이터베이스 CRUD 흐름

#### 6.4.1 카메라 삽입 (Insert)

```
┌─────────────────────────────────────────────────────────────┐
│ 1. User clicks "Save" in CameraDevicePanelView              │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 2. CameraDevicePanelViewModel → SaveCommand.Execute()      │
│    - Create CameraModel from UI inputs                       │
│    - Call ICameraDbService.InsertCameraAsync()              │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 3. CameraDbService → InsertCameraAsync(model)               │
│    ┌─────────────────────────────────────────────────────┐  │
│    │ Step 1: BEGIN TRANSACTION                           │  │
│    └─────────────────┬───────────────────────────────────┘  │
│                      │                                       │
│    ┌─────────────────▼───────────────────────────────────┐  │
│    │ Step 2: Generate Guid if empty                      │  │
│    │   - Guid.NewGuid().ToString()                       │  │
│    └─────────────────┬───────────────────────────────────┘  │
│                      │                                       │
│    ┌─────────────────▼───────────────────────────────────┐  │
│    │ Step 3: INSERT INTO Cameras                         │  │
│    │   - Execute SQL                                     │  │
│    │   - SELECT LAST_INSERT_ID() → cameraId              │  │
│    │   - model.Id = cameraId                             │  │
│    └─────────────────┬───────────────────────────────────┘  │
│                      │                                       │
│    ┌─────────────────▼───────────────────────────────────┐  │
│    │ Step 4: INSERT INTO CameraConnectionInfo            │  │
│    │   - CameraId = cameraId                             │  │
│    │   - Execute SQL                                     │  │
│    │   - SELECT LAST_INSERT_ID() → connId                │  │
│    │   - model.ConnectionInfo.Id = connId                │  │
│    └─────────────────┬───────────────────────────────────┘  │
│                      │                                       │
│    ┌─────────────────▼───────────────────────────────────┐  │
│    │ Step 5: INSERT INTO CameraStreamingOptions          │  │
│    │   - CameraId = cameraId                             │  │
│    │   - Execute SQL                                     │  │
│    │   - SELECT LAST_INSERT_ID() → optId                 │  │
│    │   - model.StreamingOptions.Id = optId               │  │
│    └─────────────────┬───────────────────────────────────┘  │
│                      │                                       │
│    ┌─────────────────▼───────────────────────────────────┐  │
│    │ Step 6: COMMIT TRANSACTION                          │  │
│    │   - Success: Return model with Ids                  │  │
│    │   - Failure: ROLLBACK                               │  │
│    └─────────────────────────────────────────────────────┘  │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 4. CameraDeviceProvider.Add(model)                          │
│    - Update in-memory collection                             │
│    - Publish event via IEventAggregator                      │
└─────────────────────────────────────────────────────────────┘
```

#### 6.4.2 카메라 조회 (Select)

```
┌─────────────────────────────────────────────────────────────┐
│ 1. CameraDbService → FetchCamerasAsync()                    │
│    ┌─────────────────────────────────────────────────────┐  │
│    │ Step 1: Execute JOIN query                          │  │
│    │   SELECT c.*, ci.*, so.*                            │  │
│    │   FROM Cameras c                                    │  │
│    │   LEFT JOIN CameraConnectionInfo ci ON c.Id=ci.CameraId
│    │   LEFT JOIN CameraStreamingOptions so ON c.Id=so.CameraId
│    └─────────────────┬───────────────────────────────────┘  │
│                      │                                       │
│    ┌─────────────────▼───────────────────────────────────┐  │
│    │ Step 2: Dapper mapping to CameraJoinSQL (POCO)     │  │
│    │   - Single query, multiple columns                  │  │
│    └─────────────────┬───────────────────────────────────┘  │
│                      │                                       │
│    ┌─────────────────▼───────────────────────────────────┐  │
│    │ Step 3: Transform to Domain Models                  │  │
│    │   - CameraJoinSQL.ToDomain()                        │  │
│    │   - Create CameraModel                              │  │
│    │   - Create RtspConnectionInfo                       │  │
│    │   - Create StreamingOptions                         │  │
│    └─────────────────┬───────────────────────────────────┘  │
│                      │                                       │
│    ┌─────────────────▼───────────────────────────────────┐  │
│    │ Step 4: Return List<ICameraModel>                  │  │
│    └─────────────────────────────────────────────────────┘  │
└───────────────────────┬─────────────────────────────────────┘
                        │
┌───────────────────────▼─────────────────────────────────────┐
│ 2. CameraDeviceProvider.Clear() + Add(cameras)              │
│    - Update in-memory collection                             │
└─────────────────────────────────────────────────────────────┘
```

---

## 7. 데이터베이스 스키마

### 7.1 ER 다이어그램

```
┌─────────────────────────────────────────────────────────────┐
│                         Cameras                              │
├─────────────────────────────────────────────────────────────┤
│ • Id (PK, AUTO_INCREMENT)                                   │
│ • Guid (UNIQUE, VARCHAR(36))                                │
│ • Title (VARCHAR(255))                                      │
│ • AutoPlay (BOOLEAN, DEFAULT FALSE)                         │
│ • ShowControls (BOOLEAN, DEFAULT TRUE)                      │
│ • CreatedAt (DATETIME, DEFAULT CURRENT_TIMESTAMP)           │
│ • UpdatedAt (DATETIME, ON UPDATE CURRENT_TIMESTAMP)         │
└─────────────────────┬───────────────────────────────────────┘
                      │ 1
                      │
                      │ CASCADE
                      │
        ┌─────────────┼─────────────┐
        │             │             │
        │ N           │             │ N
┌───────▼───────────┐ │ ┌───────────▼───────────┐
│ CameraConnectionInfo│ │ │CameraStreamingOptions│
├────────────────────┤ │ ├───────────────────────┤
│ • Id (PK)          │ │ │ • Id (PK)             │
│ • CameraId (FK,UNI)│ │ │ • CameraId (FK,UNIQUE)│
│ • Url              │ │ │ • NetworkCaching      │
│ • Username         │ │ │ • UseTcp              │
│ • Password         │ │ │ • FrameBufferSize     │
│ • IpAddress        │ │ │ • ConnectionTimeout   │
│ • Port             │ │ │ • UseHardwareAccel    │
│ • Protocol         │ │ │ • AllowFrameSkip      │
│ • StreamPath       │ │ │ • MaxDecodingThreads  │
│ • ChannelId        │ │ │ • EnableMulticast     │
│ • Description      │ │ │ • EnableAutoReconnect │
│ • CameraName       │ │ │ • MaxReconnectAttempts│
│ • Location         │ │ │ • ReconnectDelaySeconds│
│ • StreamType       │ │ │ • ExponentialBackoff  │
│ • IsEnabled        │ │ │ • IsMuted             │
└────────────────────┘ │ │ • Volume              │
                       │ │ • EnableAudio         │
                       │ │ • AudioSampleRate     │
                       │ │ • KeepAspectRatio     │
                       │ │ • VideoCodec          │
                       │ │ • EnableMemoryOptim   │
                       │ │ • MaxBufferSizeMB     │
                       │ │ • EnableDebugLogging  │
                       │ │ • EnableStatistics    │
                       │ └───────────────────────┘
                       │
                       │ (Note: EventDbService manages
                       │  separate CameraEvents table
                       │  with event configurations)
                       │
```

### 7.2 테이블 관계

| 관계 | 테이블 A | 테이블 B | 카디널리티 | 제약 조건 |
|------|----------|----------|------------|-----------|
| FK | Cameras | CameraConnectionInfo | 1:1 | ON DELETE CASCADE |
| FK | Cameras | CameraStreamingOptions | 1:1 | ON DELETE CASCADE |

### 7.3 인덱스

| 테이블 | 인덱스 이름 | 컬럼 | 타입 |
|--------|-------------|------|------|
| Cameras | PRIMARY | Id | PRIMARY KEY |
| Cameras | idx_guid | Guid | INDEX (UNIQUE) |
| CameraConnectionInfo | PRIMARY | Id | PRIMARY KEY |
| CameraConnectionInfo | idx_camera_id | CameraId | INDEX |
| CameraStreamingOptions | PRIMARY | Id | PRIMARY KEY |
| CameraStreamingOptions | idx_camera_id | CameraId | INDEX |

### 7.4 샘플 데이터

#### Cameras 테이블
```sql
INSERT INTO Cameras (Guid, Title, AutoPlay, ShowControls) VALUES
('a1b2c3d4-e5f6-7890-abcd-ef1234567890', 'Front Door Camera', TRUE, FALSE),
('b2c3d4e5-f6g7-8901-bcde-f12345678901', 'Parking Lot Camera', TRUE, FALSE),
('c3d4e5f6-g7h8-9012-cdef-123456789012', 'Warehouse Camera', TRUE, FALSE);
```

#### CameraConnectionInfo 테이블
```sql
INSERT INTO CameraConnectionInfo (
    CameraId, IpAddress, Port, Username, Password, StreamPath, IsEnabled
) VALUES
(1, '192.168.1.100', 554, 'admin', 'password123', '/stream1', TRUE),
(2, '192.168.1.101', 554, 'admin', 'password123', '/stream1', TRUE),
(3, '192.168.1.102', 554, 'admin', 'password123', '/stream1', TRUE);
```

#### CameraStreamingOptions 테이블
```sql
INSERT INTO CameraStreamingOptions (
    CameraId, NetworkCaching, UseTcp, UseHardwareAcceleration
) VALUES
(1, 300, TRUE, TRUE),
(2, 300, TRUE, TRUE),
(3, 300, TRUE, TRUE);
```

---

## 8. 코드 연결성 맵

### 8.1 의존성 그래프

```
                     ┌─────────────────┐
                     │  Bootstrapper   │
                     └────────┬────────┘
                              │
              ┌───────────────┼───────────────┐
              │               │               │
    ┌─────────▼────────┐ ┌───▼──────┐ ┌──────▼─────────┐
    │   SetupModel     │ │ Services │ │  ViewModels    │
    └─────────┬────────┘ └───┬──────┘ └──────┬─────────┘
              │              │               │
              │    ┌─────────┼────────┐      │
              │    │         │        │      │
              │ ┌──▼─────────▼──┐ ┌──▼──────▼──┐
              │ │ CameraDbSrv  │ │ EventService│
              │ └──┬───────────┘ └──┬──────────┘
              │    │                │
              │    │ ┌──────────────┼─────────┐
              │    │ │              │         │
   ┌──────────▼────▼─▼──────┐  ┌───▼─────────▼────┐
   │ CameraDeviceProvider  │  │ CameraEventProvider│
   └──────────┬─────────────┘  └───┬───────────────┘
              │                    │
       ┌──────┴──────┐   ┌─────────┴──────────┐
       │             │   │                    │
┌──────▼─────┐ ┌────▼────────┐   ┌───────────▼────────┐
│PopupShellVM│ │PopupViewer  │   │ Redis Events       │
└────────────┘ │Service      │   └────────────────────┘
               └─────────────┘
```

### 8.2 ViewModel ↔ View 매핑

| ViewModel | View | 관계 |
|-----------|------|------|
| `PopupShellViewModel` | `PopupShellView.xaml` | 1:1 (Main Window) |
| `CameraDevicePanelViewModel` | `CameraDevicePanelView.xaml` | 1:1 (Modal Dialog) |
| `CameraEventPanelViewModel` | `CameraEventPanelView.xaml` | 1:1 (Modal Dialog) |
| `CameraDeviceViewModel` | (Item Template) | 1:N (List Item) |
| `CameraEventViewModel` | (Item Template) | 1:N (List Item) |

### 8.3 Service ↔ Provider 관계

```
CameraDbService
    │
    ├─(writes)──→ CameraDeviceProvider
    │               │
    │               └─(reads)──→ PopupShellViewModel
    │                           │
    └─(reads from)──────────────┘

EventDbService
    │
    ├─(writes)──→ CameraEventProvider
    │               │
    │               └─(reads)──→ EventService
    │                           │
    └─(reads from)──────────────┘
```

### 8.4 Command 연결 맵

| Command | ViewModel | Method | View Element |
|---------|-----------|--------|--------------|
| `ShowSingleCameraCommand` | `PopupShellViewModel` | `ShowSingleCameraAsync()` | Button (Single) |
| `ShowDoubleCameraCommand` | `PopupShellViewModel` | `ShowDoubleCameraAsync()` | Button (Double) |
| `ShowTripleCameraCommand` | `PopupShellViewModel` | `ShowTripleCameraAsync()` | Button (Triple) |
| `AddRandomCameraCommand` | `PopupShellViewModel` | `AddRandomCameraAsync()` | Button (Random) |
| `ClosePopupCommand` | `PopupShellViewModel` | `ClosePopup()` | Button (Close) |
| `ChangePositionCommand` | `PopupShellViewModel` | `ChangePosition()` | ComboBox |
| `ApplyStreamingSettingsCommand` | `PopupShellViewModel` | `ApplyStreamingSettings()` | Button (Apply) |
| `ResetStreamingSettingsCommand` | `PopupShellViewModel` | `ResetStreamingSettings()` | Button (Reset) |
| `OpenCameraDevicePanelCommand` | `PopupShellViewModel` | `OpenCameraDevicePanelAsync()` | Button (Camera Config) |
| `OpenCameraEventPanelCommand` | `PopupShellViewModel` | `OpenCameraEventPanelAsync()` | Button (Event Config) |

### 8.5 이벤트 흐름 맵

```
Redis Message
    │
    └──→ IRedisService.RedisSubscribeEventAsync
            │
            └──→ EventService.RedisSubscribeEventAsync
                    │
                    └──→ EventService.ProcessMessageAsync
                            │
                            └──→ EventService.MessageSelector
                                    │
                    ┌───────────────┴───────────────┐
                    │                               │
            ┌───────▼────────┐             ┌────────▼────────┐
            │ OnEventReceived│             │ OnEventResolved │
            │    Async       │             │                 │
            └───────┬────────┘             └────────┬────────┘
                    │                               │
        ┌───────────┴──────────┐                    │
        │                      │                    │
┌───────▼─────────┐  ┌─────────▼──────────┐  ┌─────▼──────────┐
│ FindEventConfig │  │ GetCamerasByGuids  │  │ RemoveRowById  │
│ByGroupAndCmd    │  │                    │  │                │
└─────────────────┘  └─────────┬──────────┘  └────────────────┘
                               │
                     ┌─────────▼──────────┐
                     │ IPopupViewerService│
                     │   .ShowPopup()     │
                     └────────────────────┘
```

---

## 9. 주요 기능

### 9.1 기능 목록

| 기능 | 설명 | 관련 컴포넌트 |
|------|------|---------------|
| **RTSP 스트리밍** | 다중 카메라 RTSP 스트림 재생 | `IImprovedRtspStreamingService` |
| **Redis 이벤트 처리** | Pub/Sub 기반 이벤트 수신 및 처리 | `EventService`, `IRedisService` |
| **카메라 DB 관리** | MySQL 기반 카메라 CRUD | `CameraDbService` |
| **이벤트 DB 관리** | MySQL 기반 이벤트 설정 CRUD | `EventDbService` |
| **팝업 자동 표시** | 이벤트 발생 시 자동 카메라 팝업 | `IPopupViewerService` |
| **수동 카메라 표시** | 사용자 명령으로 카메라 선택 표시 | `PopupShellViewModel` |
| **자동 제거** | 타임아웃 기반 팝업 자동 종료 | `StreamingSetupModel.IsAutoDiscard` |
| **팝업 위치 설정** | 9개 위치 중 선택 가능 | `EnumDisplayPosition` |
| **하드웨어 가속** | GPU 기반 디코딩 | `StreamingOptions.UseHardwareAcceleration` |
| **메모리 관리** | 최대 메모리 사용량 제한 | `SetupModel.MaxMemoryUsageBytes` |
| **연결 풀링** | 스트리밍 컨텍스트 풀 관리 | `SetupModel.ContextPoolSize` |

### 9.2 이벤트 타입

#### EnumPopupCmd
```csharp
public enum EnumPopupCmd
{
    OPEN_POPUP_CAMERAS,   // 카메라 팝업 열기
    CLOSE_POPUP_CAMERAS   // 카메라 팝업 닫기
}
```

#### EnumDisplayPosition
```csharp
public enum EnumDisplayPosition
{
    TopLeft,      // 상단 좌측
    TopCenter,    // 상단 중앙
    TopRight,     // 상단 우측
    MiddleLeft,   // 중앙 좌측
    Center,       // 정중앙
    MiddleRight,  // 중앙 우측
    BottomLeft,   // 하단 좌측
    BottomCenter, // 하단 중앙
    BottomRight   // 하단 우측
}
```

### 9.3 카메라 표시 모드

| 모드 | 카메라 수 | Command | 설명 |
|------|-----------|---------|------|
| Single | 1개 | `ShowSingleCameraCommand` | 단일 카메라 표시 |
| Double | 2개 | `ShowDoubleCameraCommand` | 2개 카메라 동시 표시 |
| Triple | 3개 | `ShowTripleCameraCommand` | 3개 카메라 동시 표시 |
| Random | 1-3개 | `AddRandomCameraCommand` | 랜덤 개수 카메라 표시 |
| Event | N개 | (Redis Event) | 이벤트 설정에 따라 |

---

## 10. Redis 메시지 포맷

### 10.1 개요

이 애플리케이션은 **Redis Pub/Sub** 메커니즘을 통해 외부 시스템으로부터 이벤트 메시지를 수신합니다. Redis 채널에 발행(Publish)된 JSON 메시지를 구독(Subscribe)하여 카메라 팝업을 자동으로 제어합니다.

### 10.2 메시지 처리 흐름

```
External System
    │
    ├─ Publish to Redis Channel
    │   - Channel: "StreamingChannel" or "StreamingChannel2"
    │   - Message: JSON String (EventMessage)
    │
    └─→ Redis Server
            │
            └─→ IRedisService (Subscribe)
                    │
                    └─→ EventService.RedisSubscribeEventAsync()
                            │
                            └─→ JSON Deserialization
                                    │
                                    └─→ EventMessage Object
```

### 10.3 EventMessage 객체 구조

EventMessage는 `Ironwall.Dotnet.Libraries.Streaming.Base.Messages` 네임스페이스에 정의되어 있으며, 다음 프로퍼티를 포함합니다:

#### 프로퍼티 목록

| 프로퍼티 | 타입 | 필수 | 설명 |
|----------|------|------|------|
| `EventId` | `string` | ✅ | 이벤트 고유 식별자 (예: "EV-2025-001") |
| `EventGroup` | `int` | ✅ | 이벤트 그룹 번호 (DB 매핑용) |
| `Cmd` | `EnumPopupCmd` | ✅ | 명령 타입 (OPEN/CLOSE) |

### 10.4 JSON 메시지 포맷

#### 10.4.1 카메라 팝업 열기 (OPEN_POPUP_CAMERAS)

```json
{
    "EventId": "EV-2025-001",
    "EventGroup": 100,
    "Cmd": "OPEN_POPUP_CAMERAS"
}
```

**필드 설명:**
- `EventId`: 이벤트의 고유 식별자. 이 값으로 팝업을 추적하고 관리합니다.
- `EventGroup`: 데이터베이스에 저장된 이벤트 설정 그룹 번호. 이 그룹에 연결된 카메라 목록을 조회합니다.
- `Cmd`: "OPEN_POPUP_CAMERAS" - 카메라 팝업 열기 명령

#### 10.4.2 카메라 팝업 닫기 (CLOSE_POPUP_CAMERAS)

```json
{
    "EventId": "EV-2025-001",
    "EventGroup": 100,
    "Cmd": "CLOSE_POPUP_CAMERAS"
}
```

**필드 설명:**
- `EventId`: 닫을 팝업의 EventId (OPEN 시 사용한 값과 동일)
- `EventGroup`: 이벤트 그룹 번호
- `Cmd`: "CLOSE_POPUP_CAMERAS" - 카메라 팝업 닫기 명령

### 10.5 EnumPopupCmd 열거형

```csharp
public enum EnumPopupCmd
{
    OPEN_POPUP_CAMERAS,   // 카메라 팝업 열기
    CLOSE_POPUP_CAMERAS   // 카메라 팝업 닫기
}
```

**JSON에서의 표현:**
- Enum은 문자열로 직렬화됩니다 (`StringEnumConverter` 사용)
- 대소문자 구분 없음 (권장: 대문자 사용)

### 10.6 실제 메시지 예제

#### 시나리오 1: 입구 침입 감지

```json
{
    "EventId": "INTRUSION-ENTRANCE-20250130-143022",
    "EventGroup": 100,
    "Cmd": "OPEN_POPUP_CAMERAS"
}
```

**처리 과정:**
1. EventService가 메시지 수신
2. EventGroup 100에 해당하는 이벤트 설정을 DB에서 조회
3. 해당 이벤트에 연결된 카메라 GUID 목록 획득 (예: 3대)
4. CameraDeviceProvider에서 실제 카메라 모델 조회
5. `IPopupViewerService.ShowPopup()` 호출하여 3대 카메라 팝업 표시
6. EventId를 RowId에 매핑하여 저장

#### 시나리오 2: 침입 해제

```json
{
    "EventId": "INTRUSION-ENTRANCE-20250130-143022",
    "EventGroup": 100,
    "Cmd": "CLOSE_POPUP_CAMERAS"
}
```

**처리 과정:**
1. EventService가 메시지 수신
2. EventId로 저장된 RowId 조회
3. `IPopupViewerService.RemoveRowById()` 호출하여 팝업 제거
4. 매핑 딕셔너리에서 EventId 삭제

### 10.7 MessageArgsModel 구조

Redis에서 수신한 원시 메시지는 `MessageArgsModel` (from `Ironwall.Dotnet.Libraries.Redis.Models`)로 래핑됩니다:

```csharp
public class MessageArgsModel
{
    public string Channel { get; set; }   // Redis 채널 이름
    public string Message { get; set; }   // JSON 문자열
}
```

### 10.8 JSON 직렬화 설정

```csharp
var settings = new JsonSerializerSettings
{
    Converters = new List<JsonConverter>
    {
        new StringEnumConverter() // Enum을 문자열로 변환
    }
};
```

**특징:**
- `StringEnumConverter` 사용으로 Enum을 문자열로 직렬화/역직렬화
- `EnumPopupCmd.OPEN_POPUP_CAMERAS` → `"OPEN_POPUP_CAMERAS"`

### 10.9 Redis 채널 설정

**appsettings.json:**
```json
{
    "NameChannel": "StreamingChannel",
    "NameChannel2": "StreamingChannel2"
}
```

**사용 예:**
- 주 채널: `StreamingChannel` - 메인 이벤트
- 보조 채널: `StreamingChannel2` - 백업 또는 다른 용도

### 10.10 메시지 발행 예제 (외부 시스템)

#### Python 예제

```python
import redis
import json

# Redis 연결
r = redis.Redis(
    host='localhost',
    port=6379,
    password='123',
    decode_responses=True
)

# 메시지 생성
message = {
    "EventId": "EV-2025-001",
    "EventGroup": 100,
    "Cmd": "OPEN_POPUP_CAMERAS"
}

# JSON 직렬화 및 발행
r.publish('StreamingChannel', json.dumps(message))
print("Message published successfully")
```

#### C# 예제 (StackExchange.Redis)

```csharp
using StackExchange.Redis;
using Newtonsoft.Json;

var redis = ConnectionMultiplexer.Connect("localhost:6379,password=123");
var subscriber = redis.GetSubscriber();

var message = new
{
    EventId = "EV-2025-001",
    EventGroup = 100,
    Cmd = "OPEN_POPUP_CAMERAS"
};

var json = JsonConvert.SerializeObject(message);
subscriber.Publish("StreamingChannel", json);
Console.WriteLine("Message published successfully");
```

#### Redis CLI 예제

```bash
redis-cli -a 123

PUBLISH StreamingChannel "{\"EventId\":\"EV-2025-001\",\"EventGroup\":100,\"Cmd\":\"OPEN_POPUP_CAMERAS\"}"
```

### 10.11 메시지 검증

#### 필수 필드 검증

EventService는 다음을 검증합니다:
1. ✅ JSON 파싱 성공 여부
2. ✅ `EventId` null 체크
3. ✅ `EventGroup` 유효성 (DB에 존재하는지)
4. ✅ `Cmd` 열거형 값 유효성

#### 오류 처리

```csharp
// JSON 파싱 실패
catch (JsonException jsonEx)
{
    _log?.Error($"JSON deserialization error: {jsonEx.Message}");
}

// EventMessage가 null
if (eventMessage == null)
{
    _log?.Warning("Failed to deserialize EventMessage");
    return;
}

// 이벤트 설정 없음
if (eventConfig == null)
{
    _log?.Warning($"No event configuration found for: {eventMessage.EventId}");
    return null;
}
```

### 10.12 이벤트 중복 방지

동일한 `EventId`로 여러 번 OPEN 명령이 들어올 경우:

```csharp
if (IsEventActive(eventMessage.EventId))
{
    _log?.Warning($"Event {eventMessage.EventId} is already active. Skipping.");
    return null;
}
```

**동작:**
- 이미 활성화된 이벤트는 무시
- 로그에 경고 메시지 기록
- 중복 팝업 방지

### 10.13 이벤트 매핑 추적

```csharp
// 이벤트ID → RowId 매핑
private readonly Dictionary<string, string> _eventRowMapping = new();

// 이벤트ID → 이벤트 이름 매핑
private readonly Dictionary<string, string> _eventNameMapping = new();
```

**저장 예:**
```
_eventRowMapping["EV-2025-001"] = "ROW-ABC123"
_eventNameMapping["EV-2025-001"] = "Entrance Event"
```

### 10.14 타임아웃 및 자동 제거

**SetupModel 설정:**
```csharp
public bool IsAutoDiscard { get; set; } = true;
public int TimeoutSeconds { get; set; } = 10;
```

**동작:**
- `IsAutoDiscard = true`: 타임아웃 후 자동으로 팝업 제거
- `TimeoutSeconds`: 타임아웃 시간 (초)
- CLOSE 명령 없이도 자동으로 팝업이 사라짐

### 10.15 테스트 방법

#### 1. Redis CLI로 테스트

```bash
# OPEN 명령 발행
redis-cli -a 123
PUBLISH StreamingChannel "{\"EventId\":\"TEST-001\",\"EventGroup\":100,\"Cmd\":\"OPEN_POPUP_CAMERAS\"}"

# 25초 대기 (또는 수동으로 확인)

# CLOSE 명령 발행
PUBLISH StreamingChannel "{\"EventId\":\"TEST-001\",\"EventGroup\":100,\"Cmd\":\"CLOSE_POPUP_CAMERAS\"}"
```

#### 2. 로그 확인

```
[INFO] [EventService] EventMessage deserialized: EventId=TEST-001
[INFO] [EventService] Processing OPEN command for Group=100
[INFO] [EventService] Found config: Test Event with 2 cameras
[INFO] [EventService] OPEN command processed successfully. RowId: ROW-XYZ
[INFO] [EventService] Event TEST-001 mapped to Row ROW-XYZ. Active: 1
```

#### 3. 애플리케이션 UI 확인

- 팝업 창이 표시되는지 확인
- 카메라 스트림이 재생되는지 확인
- 설정된 위치에 표시되는지 확인

### 10.16 문제 해결

#### 메시지가 수신되지 않음

**확인 사항:**
1. Redis 서버 실행 중인지 확인
   ```bash
   redis-cli ping
   ```
2. 채널 이름 일치 여부 확인
3. Redis 비밀번호 확인
4. IRedisService가 정상 초기화되었는지 로그 확인

#### JSON 파싱 오류

**원인:**
- 잘못된 JSON 형식
- Enum 값 오타 ("OPEN_POPUP_CAMERA**S**" 확인)

**해결:**
- JSON 유효성 검사 (https://jsonlint.com/)
- Enum 값 대소문자 확인

#### 카메라가 표시되지 않음

**원인:**
- EventGroup에 해당하는 설정이 DB에 없음
- 카메라 GUID가 잘못됨
- CameraDeviceProvider가 비어있음

**해결:**
- DB 확인: `SELECT * FROM CameraEvents WHERE DeviceGroup = 100`
- 카메라 설정 확인: `SELECT * FROM Cameras`
- 로그 확인: "No event configuration found"

---

## 11. 설정 및 구성

### 11.1 appsettings.json 상세

**파일**: [Dotnet.Rtsp.Viewer.Ui/appsettings.json](Dotnet.Rtsp.Viewer.Ui/appsettings.json)

```json
{
    "AppSettings": {
        // ── 데이터베이스 설정 ──────────────────────
        "IpDbServer": "127.0.0.1",
        "PortDbServer": 3306,
        "DbDatabase": "streaming_DB",
        "UidDbServer": "root",
        "PasswordDbServer": "root",

        // ── Redis 설정 ────────────────────────────
        "IpAddressRedis": "localhost",
        "PortRedis": 6379,
        "PasswordRedis": "123",
        "NameChannel": "StreamingChannel",
        "NameChannel2": "StreamingChannel2",

        // ── 스트리밍 연결 관리 ────────────────────
        "MaxConnections": 17,
        "MaxRetryAttempts": 5,
        "ContextPoolSize": 32,

        // ── 메모리 관리 ───────────────────────────
        "MaxMemoryUsageBytes": 1073741824,
        "MemoryCheckIntervalSeconds": 30,
        "AutoCleanupInactiveStreams": true,
        "InactiveStreamTimeoutMinutes": 30,

        // ── 성능 최적화 ───────────────────────────
        "UseHardwareAcceleration": true,
        "EnableFrameSkipping": true,
        "MaxFrameSkip": 5,
        "DefaultNetworkCaching": 300,

        // ── 자동 제거 ─────────────────────────────
        "IsAutoDiscard": true,
        "TimeoutSeconds": 25,

        // ── 로깅 ──────────────────────────────────
        "EnableDebugLogging": false,
        "LogPath": "logs/streaming",
        "SnapshotPath": "snapshots/",

        // ── 모니터링 ──────────────────────────────
        "EnablePerformanceMonitoring": true,
        "StatisticsUpdateIntervalMs": 1000
    }
}
```

### 11.2 설정 항목 설명

#### 데이터베이스 설정
| 항목 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `IpDbServer` | string | 127.0.0.1 | MySQL 서버 IP |
| `PortDbServer` | int | 3306 | MySQL 포트 |
| `DbDatabase` | string | streaming_DB | 데이터베이스 이름 |
| `UidDbServer` | string | root | MySQL 사용자 이름 |
| `PasswordDbServer` | string | root | MySQL 비밀번호 |

#### Redis 설정
| 항목 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `IpAddressRedis` | string | localhost | Redis 서버 IP |
| `PortRedis` | int | 6379 | Redis 포트 |
| `PasswordRedis` | string | 123 | Redis 비밀번호 |
| `NameChannel` | string | StreamingChannel | 주 채널 이름 |
| `NameChannel2` | string | StreamingChannel2 | 보조 채널 이름 |

#### 스트리밍 연결 관리
| 항목 | 타입 | 기본값 | 설명 | 권장 범위 |
|------|------|--------|------|-----------|
| `MaxConnections` | int | 17 | 최대 동시 연결 수 | 10-20 |
| `MaxRetryAttempts` | int | 5 | 재연결 최대 시도 횟수 | 3-10 |
| `ContextPoolSize` | int | 32 | 스트리밍 컨텍스트 풀 크기 | 16-64 |

#### 메모리 관리
| 항목 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `MaxMemoryUsageBytes` | long | 1073741824 (1GB) | 최대 메모리 사용량 |
| `MemoryCheckIntervalSeconds` | int | 30 | 메모리 체크 주기 (초) |
| `AutoCleanupInactiveStreams` | bool | true | 비활성 스트림 자동 정리 |
| `InactiveStreamTimeoutMinutes` | int | 30 | 비활성 타임아웃 (분) |

#### 성능 최적화
| 항목 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `UseHardwareAcceleration` | bool | true | 하드웨어 가속 사용 (GPU) |
| `EnableFrameSkipping` | bool | true | 프레임 스킵 활성화 |
| `MaxFrameSkip` | int | 5 | 최대 스킵 프레임 수 |
| `DefaultNetworkCaching` | int | 300 | 네트워크 캐싱 (ms) |

#### 자동 제거
| 항목 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `IsAutoDiscard` | bool | true | 자동 제거 활성화 |
| `TimeoutSeconds` | int | 25 | 자동 제거 타임아웃 (초) |

#### 로깅
| 항목 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `EnableDebugLogging` | bool | false | 디버그 로그 활성화 |
| `LogPath` | string | logs/streaming | 로그 파일 경로 |
| `SnapshotPath` | string | snapshots/ | 스냅샷 저장 경로 |

#### 모니터링
| 항목 | 타입 | 기본값 | 설명 |
|------|------|--------|------|
| `EnablePerformanceMonitoring` | bool | true | 성능 모니터링 활성화 |
| `StatisticsUpdateIntervalMs` | int | 1000 | 통계 업데이트 주기 (ms) |

### 11.3 환경별 설정 권장값

#### 개발 환경
```json
{
    "MaxConnections": 5,
    "UseHardwareAcceleration": false,
    "EnableDebugLogging": true,
    "TimeoutSeconds": 60
}
```

#### 테스트 환경
```json
{
    "MaxConnections": 10,
    "UseHardwareAcceleration": true,
    "EnableDebugLogging": true,
    "TimeoutSeconds": 30
}
```

#### 프로덕션 환경
```json
{
    "MaxConnections": 17,
    "UseHardwareAcceleration": true,
    "EnableDebugLogging": false,
    "TimeoutSeconds": 25,
    "MaxMemoryUsageBytes": 2147483648
}
```

---

## 12. 빌드 및 배포

### 11.1 빌드 요구사항

| 항목 | 요구사항 |
|------|----------|
| **OS** | Windows 7 이상 |
| **.NET SDK** | .NET 8.0 SDK |
| **IDE** | Visual Studio 2022 (권장) |
| **데이터베이스** | MySQL 8.0+ |
| **캐시 서버** | Redis 6.0+ |

### 12.2 빌드 명령

#### Debug 빌드
```bash
dotnet build Dotnet.Rtsp.Viewer.Ui.sln -c Debug
```

#### Release 빌드
```bash
dotnet build Dotnet.Rtsp.Viewer.Ui.sln -c Release
```

#### 플랫폼별 빌드
```bash
# x64
dotnet build -c Release -p:Platform=x64

# x86
dotnet build -c Release -p:Platform=x86
```

### 12.3 실행 방법

#### 개발 모드
```bash
cd Dotnet.Rtsp.Viewer.Ui
dotnet run
```

#### 빌드 출력 실행
```bash
cd Dotnet.Rtsp.Viewer.Ui/bin/Release/net8.0-windows7.0
Dotnet.Rtsp.Viewer.Ui.exe
```

### 12.4 배포 구조

```
Dotnet.Rtsp.Viewer.Ui/
├── Dotnet.Rtsp.Viewer.Ui.exe
├── appsettings.json
├── *.dll (Dependencies)
├── libvlc/
│   ├── win-x64/
│   └── win-x86/
├── logs/
│   └── streaming/
└── snapshots/
```

### 12.5 배포 체크리스트

- [ ] appsettings.json 환경별 설정 확인
- [ ] MySQL 서버 접근 가능 확인
- [ ] Redis 서버 접근 가능 확인
- [ ] 방화벽 포트 개방 (3306, 6379, 554)
- [ ] RTSP 카메라 네트워크 연결 확인
- [ ] 로그 디렉토리 생성
- [ ] 스냅샷 디렉토리 생성

---

## 13. 트러블슈팅

### 12.1 일반적인 문제

#### 문제: 중복 실행 경고
**증상**: "Application is already running..." 메시지 표시

**원인**: 동일 프로세스가 이미 실행 중

**해결**:
1. 작업 관리자에서 `Dotnet.Rtsp.Viewer.Ui.exe` 종료
2. 또는 Bootstrapper.cs의 중복 검사 로직 비활성화 (개발 중)

---

#### 문제: MySQL 연결 실패
**증상**: "Connect Error: Unable to connect to any of the specified MySQL hosts"

**원인**: MySQL 서버 미실행 또는 잘못된 연결 정보

**해결**:
1. MySQL 서버 실행 확인
   ```bash
   mysql -u root -p
   ```
2. appsettings.json 확인
   - `IpDbServer`, `PortDbServer` 확인
   - `UidDbServer`, `PasswordDbServer` 확인
3. 방화벽 포트 3306 개방 확인

---

#### 문제: Redis 연결 실패
**증상**: Redis 이벤트 수신 안 됨

**원인**: Redis 서버 미실행 또는 잘못된 연결 정보

**해결**:
1. Redis 서버 실행 확인
   ```bash
   redis-cli ping
   # 응답: PONG
   ```
2. appsettings.json 확인
   - `IpAddressRedis`, `PortRedis` 확인
   - `PasswordRedis` 확인
3. Redis 채널 이름 확인
   - `NameChannel`, `NameChannel2`

---

#### 문제: RTSP 스트리밍 실패
**증상**: 카메라 화면 표시 안 됨, 검은 화면

**원인**: RTSP URL 오류, 네트워크 연결 실패, 인증 실패

**해결**:
1. RTSP URL 확인
   ```
   rtsp://username:password@ip:port/stream_path
   ```
2. VLC로 RTSP URL 테스트
   ```
   vlc rtsp://admin:password@192.168.1.100:554/stream1
   ```
3. 카메라 네트워크 연결 확인
   ```bash
   ping 192.168.1.100
   ```
4. 방화벽 포트 554 개방 확인
5. 카메라 Username/Password 확인

---

#### 문제: 메모리 부족
**증상**: 애플리케이션 느려짐, 크래시

**원인**: 너무 많은 스트림 동시 실행

**해결**:
1. `MaxConnections` 감소
   ```json
   "MaxConnections": 10
   ```
2. `MaxMemoryUsageBytes` 증가
   ```json
   "MaxMemoryUsageBytes": 2147483648
   ```
3. `AutoCleanupInactiveStreams` 활성화
   ```json
   "AutoCleanupInactiveStreams": true
   ```

---

#### 문제: 팝업 위치 변경 안 됨
**증상**: ChangePosition 명령 실행되지 않음

**원인**: 팝업이 열려있지 않음

**해결**:
1. 팝업 먼저 표시
2. 그 후 위치 변경 시도
3. `_popupService.IsPopupOpen` 확인

---

### 12.2 로그 분석

#### 로그 위치
```
logs/streaming/
├── app-YYYY-MM-DD.log
└── error-YYYY-MM-DD.log
```

#### 주요 로그 패턴

**정상 시작**
```
[INFO] CameraDB 연결 성공: 127.0.0.1:3306/streaming_db
[INFO] Cameras 테이블 생성/확인 완료
[INFO] FetchInstanceAsync 완료 - Cameras: 10
[INFO] [PopupShellViewModel] Initialized
[INFO] [EventService] Initialized
```

**Redis 이벤트 수신**
```
[INFO] [EventService] EventMessage deserialized: EventId=EV-001
[INFO] [EventService] Processing OPEN command for Group=100
[INFO] [EventService] Found config: Entrance Event with 3 cameras
[INFO] [EventService] OPEN command processed successfully. RowId: ...
```

**데이터베이스 오류**
```
[ERROR] Connect Error: Unable to connect to MySQL server
[ERROR] FetchCamerasAsync Error: Timeout expired
```

**스트리밍 오류**
```
[ERROR] [PopupShellViewModel] ShowPopup error: RTSP connection failed
[WARNING] Camera not found: a1b2c3d4-e5f6-7890-abcd-ef1234567890
```

---

### 12.3 성능 튜닝

#### CPU 사용률 높음
- `UseHardwareAcceleration = true` 설정
- `MaxDecodingThreads` 증가
- `ContextPoolSize` 감소

#### 메모리 사용량 높음
- `MaxConnections` 감소
- `FrameBufferSize` 감소
- `AutoCleanupInactiveStreams = true`
- `InactiveStreamTimeoutMinutes` 감소

#### 네트워크 지연
- `DefaultNetworkCaching` 증가 (300 → 500)
- `ConnectionTimeoutSeconds` 증가
- `MaxRetryAttempts` 감소

#### 프레임 드롭
- `EnableFrameSkipping = true`
- `MaxFrameSkip` 증가
- `AllowFrameSkip = true`

---

### 12.4 디버깅 팁

#### 1. 상세 로그 활성화
```json
"EnableDebugLogging": true
```

#### 2. 스트리밍 상태 모니터링
```json
"EnablePerformanceMonitoring": true,
"EnableStatistics": true
```

#### 3. Visual Studio 디버깅
- Bootstrapper.cs에 브레이크포인트 설정
- CameraDbService.FetchCamerasAsync에 브레이크포인트 설정
- EventService.OnEventReceivedAsync에 브레이크포인트 설정

#### 4. Redis 메시지 확인
```bash
redis-cli
SUBSCRIBE StreamingChannel
```

#### 5. MySQL 쿼리 확인
```sql
USE streaming_db;
SELECT * FROM Cameras;
SELECT * FROM CameraConnectionInfo;
SELECT * FROM CameraStreamingOptions;
```

---

## 14. 부록

### 13.1 용어집

| 용어 | 설명 |
|------|------|
| **RTSP** | Real Time Streaming Protocol - 실시간 스트리밍 프로토콜 |
| **MVVM** | Model-View-ViewModel - WPF 디자인 패턴 |
| **DI** | Dependency Injection - 의존성 주입 |
| **IoC** | Inversion of Control - 제어의 역전 |
| **ORM** | Object-Relational Mapping - 객체-관계 매핑 |
| **Pub/Sub** | Publish/Subscribe - 게시/구독 패턴 |
| **POCO** | Plain Old CLR Object - 간단한 C# 객체 |
| **CASCADE** | 연쇄 삭제 - 부모 삭제 시 자식도 삭제 |

### 13.2 참고 문서

- [Caliburn.Micro Documentation](https://caliburnmicro.com/)
- [Autofac Documentation](https://autofac.readthedocs.io/)
- [Dapper Documentation](https://github.com/DapperLib/Dapper)
- [Material Design](https://material.io/design)
- [RTSP Specification](https://datatracker.ietf.org/doc/html/rfc2326)
- [Redis Pub/Sub](https://redis.io/docs/manual/pubsub/)

### 13.3 변경 이력

| 버전 | 날짜 | 변경 내용 |
|------|------|----------|
| 1.0.0 | 2025-01-30 | 초기 프로젝트 매뉴얼 작성 |

---

## 15. 라이선스

Copyright © 2025 Sensorway Co., Ltd. All rights reserved.

---

**문서 작성자**: GHLee (lsirikh@naver.com)
**작성일**: 2025-01-30
**최종 수정**: 2025-01-30
**버전**: 1.0.0
