# Ironwall RTSP Streaming Libraries

### Goal
> Sensorway Framework의 핵심 라이브러리로, **RTSP 기반 실시간 영상 스트리밍**과 **다중 카메라 관리 시스템**을 제공합니다.

### Site : RTSP Streaming & Camera Management
<hr>

## 1. Ironwall.Dotnet.Libraries.Base 소개

### 1.1 개요
`Ironwall.Dotnet.Libraries.Base`는 **Sensorway Framework**의 기반 라이브러리로, 모든 프로젝트에서 공통적으로 사용되는 **핵심 기능과 인터페이스**를 제공합니다.  
해당 라이브러리는 **.NET 8.0 (Windows)** 환경에서 동작하며, **WPF** 기반 애플리케이션을 지원합니다.

### 1.2 프로젝트 구성

#### **📂 Models**
> 기본 데이터 모델 및 인터페이스

- `BaseModel.cs` - 모든 모델의 기본 클래스
- `IBaseModel.cs` - 기본 모델 인터페이스
- `CommonMessageModel.cs` - 공통 메시지 모델
- `ICommonMessageModel.cs` - 공통 메시지 인터페이스
- `IMessageModel.cs` - 메시지 모델 인터페이스

#### **📂 Services**
> 핵심 서비스 및 유틸리티

- `IService.cs` - 서비스 기본 인터페이스
- `ILogService.cs` / `LogService.cs` - 로깅 서비스
- `IDataProviderService.cs` - 데이터 제공자 서비스 인터페이스
- `DispatcherService.cs` - UI 스레드 디스패처 서비스
- `TaskService.cs` - 비동기 작업 관리 서비스
- `TimerService.cs` - 타이머 기반 작업 서비스
- `ILoadable.cs` - 로드 가능 인터페이스

#### **📂 DataProviders**
> 데이터 컬렉션 관리

- `ICollector.cs` - 컬렉터 인터페이스
- `BaseProvider.cs` - 기본 데이터 제공자
- `BaseCommonProvider.cs` - 공통 데이터 제공자
- `EntityCollectionProvider.cs` - 엔티티 컬렉션 제공자
- `EntityListProvider.cs` - 엔티티 리스트 제공자
- `InstanceFactory.cs` - 인스턴스 팩토리

#### **📄 ParentBootstrapper.cs**
> 애플리케이션 부트스트래퍼 (초기화 담당)

#### 개발 환경
- **.NET Version**: `net8.0-windows`
- **언어**: `C#`
- **UI Framework**: `WPF`
- **DI Container**: `Autofac`

---

## 2. Ironwall.Dotnet.Libraries.ViewModel 소개

### 2.1 개요
`Ironwall.Dotnet.Libraries.ViewModel`은 **Caliburn.Micro MVVM 프레임워크**를 기반으로 **WPF 애플리케이션의 ViewModel 계층**을 관리하는 라이브러리입니다.  
**ViewModel 컴포넌트**와 **Conductor 패턴**을 지원하여 **동적 UI 관리**를 구현합니다.

### 2.2 프로젝트 구성

#### **📂 ViewModels/Components**
> 재사용 가능한 ViewModel 컴포넌트

- `BaseViewModel.cs` / `IBaseViewModel.cs` - 기본 ViewModel
- `BasePanelViewModel.cs` / `IBasePanelViewModel.cs` - 패널 ViewModel
- `BaseCustomViewModel.cs` / `IBaseCustomViewModel.cs` - 커스텀 ViewModel
- `BaseDataGridViewModel.cs` - DataGrid 전용 ViewModel
- `BaseDataGridPanelViewModel.cs` - DataGrid 패널 ViewModel
- `SelectableBaseViewModel.cs` / `ISelectableBaseViewModel.cs` - 선택 가능한 ViewModel

#### **📂 ViewModels/Conductors**
> Conductor 패턴 구현 (화면 전환 관리)

- `IConductorViewModel.cs` - Conductor 인터페이스
- `ConductorOneViewModel.cs` - 단일 활성 화면 관리
- `ConductorAllViewModel.cs` - 다중 화면 관리

#### **📂 Models**
> ViewModel 전용 모델

- `CommonMessages.cs` - 공통 메시지 정의
- `ValueNotifyEventArgs.cs` - 값 변경 이벤트 아규먼트

#### 개발 환경
- **.NET Version**: `net8.0-windows`
- **언어**: `C#`
- **UI Framework**: `WPF`
- **MVVM Framework**: `Caliburn.Micro`

---

## 3. Ironwall.Dotnet.Libraries.Utils 소개

### 3.1 개요
`Ironwall.Dotnet.Libraries.Utils`는 **WPF 애플리케이션 개발**을 위한 **바인딩 확장**, **값 변환**, **행동(Behavior)** 기능을 제공합니다.

### 3.2 프로젝트 구성

#### **📂 Utils**
> WPF 유틸리티 및 컨버터

- `BindingProxys.cs` - 바인딩 프록시 (데이터 컨텍스트 바인딩 문제 해결)
- `BoolToInverseVisibleConverter.cs` - Bool → Visibility 반전 변환기
  - `true` → `Collapsed`, `false` → `Visible`
- `EnumBindingSourceExtension.cs` - Enum 바인딩 확장

#### **📂 Behaviors**
> WPF Behavior 기능 (UI 동작 제어)

- 다양한 커스텀 Behavior 제공

---

## 4. Ironwall.Dotnet.Libraries.Streaming.Base 소개

### 4.1 개요
`Ironwall.Dotnet.Libraries.Streaming.Base`는 **RTSP 스트리밍 시스템의 기본 모델과 데이터 제공자**를 정의하는 라이브러리입니다.

### 4.2 프로젝트 구성

#### **📂 Models**
> 스트리밍 관련 핵심 모델

- `ICameraModel.cs` / `CameraModel.cs` - 카메라 데이터 모델
- `ICameraEventModel.cs` / `CameraEventModel.cs` - 카메라 이벤트 모델
- `RtspConnectionInfo.cs` - RTSP 연결 정보
- `StreamingOptions.cs` - 스트리밍 옵션
- `EnumPopupCmd.cs` - 팝업 명령 열거형
  - `OPEN_POPUP_CAMERAS` - 팝업 열기
  - `CLOSE_POPUP_CAMERAS` - 팝업 닫기
- `EnumPopupStatus.cs` - 팝업 상태 열거형
  - `Idle` - 대기 중
  - `Processing` - 처리 중
  - `Completed` - 완료
  - `Cancelled` - 취소됨
  - `Failed` - 실패

#### **📂 Providers**
> 데이터 제공자

- `CameraDeviceProvider.cs` - 카메라 장치 데이터 제공자
- `CameraEventProvider.cs` - 카메라 이벤트 데이터 제공자

#### **📂 Messages**
> 메시지 모델

- 이벤트 기반 메시지 전달 시스템 지원

---

## 5. Ironwall.Dotnet.Libraries.Streaming 소개

### 5.1 개요
`Ironwall.Dotnet.Libraries.Streaming`은 **LibVLCSharp 기반 RTSP 스트리밍 엔진**을 제공하는 핵심 라이브러리입니다.  
**다중 카메라 스트리밍**, **재연결 로직**, **메모리 최적화**, **팝업 뷰어** 등을 지원합니다.

### 5.2 프로젝트 구성

#### **📂 Controls**
> WPF 커스텀 컨트롤

- `ImprovedRtspPlayer.cs` - RTSP 플레이어 컨트롤
- `PopupViewer.cs` - 팝업 뷰어 컨트롤

#### **📂 Services**
> 스트리밍 핵심 서비스

- `IImprovedRtspStreamingService.cs` / `ImprovedRtspStreamingService.cs` - 개선된 RTSP 스트리밍 서비스
- `IPopupViewerService.cs` / `PopupViewerService.cs` - 팝업 뷰어 서비스
- `IStreamingContextPool.cs` / `StreamingContextPool.cs` - 스트리밍 컨텍스트 풀
- `IPlayerRegistry.cs` - 플레이어 레지스트리 인터페이스
- `ImprovedStreamingContext.cs` - 스트리밍 컨텍스트 (RtspPlayer 통합 관리)
- `LibVLCInitializer.cs` - LibVLC 초기화

#### **📂 Models**
> 스트리밍 모델

- `StreamingSetupModel.cs` - 스트리밍 설정 모델
- `StreamingStatistics.cs` - 스트리밍 통계
- `PlaybackState.cs` - 재생 상태
- `EnumDisplayPosition.cs` - 디스플레이 위치 열거형

#### **📂 ViewModel**
> 스트리밍 ViewModel

- `CameraViewModel.cs` - 카메라 ViewModel
- `CameraRowViewModel.cs` - 카메라 행(Row) ViewModel
- `PopupWindowViewModel.cs` - 팝업 윈도우 ViewModel

#### **📂 Views**
> 스트리밍 View

- `PopupWindowView.xaml` - 팝업 윈도우 View

#### **📂 Helpers**
> 유틸리티 헬퍼

- `MemoryHelper.cs` - 메모리 관리 헬퍼
  - 가비지 컬렉션 강제 실행
  - 메모리 압박 체크
  - Large Object Heap 압축

#### **📂 Events**
> 이벤트 아규먼트

- `StreamingStateChangedEventArgs.cs` - 스트리밍 상태 변경 이벤트
- `StreamingErrorEventArgs.cs` - 스트리밍 에러 이벤트
- `StreamingProgressEventArgs.cs` - 스트리밍 진행 이벤트
- `ErrorSeverity.cs` - 에러 심각도 열거형

#### **📂 Commands**
> 명령(Command) 패턴

- `AsyncRelayCommand.cs` - 비동기 릴레이 커맨드
- `RelayCommand.cs` - 릴레이 커맨드

#### **📂 Converters**
> WPF 컨버터

- 다양한 값 변환기 제공

#### **📂 Themes**
> WPF 테마 리소스

- `Generic.xaml` - 기본 테마
- `ImprovedCameraItem.xaml` - 카메라 아이템 스타일
- `PopupViewer.xaml` - 팝업 뷰어 스타일

---

## 6. Ironwall.Dotnet.Libraries.Redis 소개

### 6.1 개요
`Ironwall.Dotnet.Libraries.Redis`는 **Redis 서버 연동**을 지원하는 라이브러리입니다.  
**Pub/Sub 패턴**과 **캐싱 기능**을 제공합니다.

### 6.2 프로젝트 구성

#### **📂 Services**
> Redis 서비스

- `IRedisService.cs` - Redis 서비스 인터페이스

#### **📂 Models**
> Redis 모델

- Redis 메시지 모델

---

## 7. Dotnet.Rtsp.Viewer.Ui 소개

### 7.1 개요
`Dotnet.Rtsp.Viewer.Ui`는 **RTSP 스트리밍 시스템의 메인 애플리케이션**입니다.
**Redis 메시지 기반 이벤트 처리**, **다중 카메라 팝업 뷰어**, **DB 연동**을 제공합니다.

**핵심 기능:**
- Redis Pub/Sub 기반 실시간 이벤트 수신
- Generic Type을 활용한 확장 가능한 메시지 구조
- EventName 기반 간소화된 이벤트-카메라 매핑
- 최대 17개 RTSP 카메라 동시 스트리밍 (1+4+12 Grid Layout)

### 7.2 프로젝트 구성

#### **📂 Views**
> WPF View 파일

- `PopupShellView.xaml` - 팝업 테스트 셸 화면
- `CameraDevicePanelView.xaml` - 카메라 장치 패널
- `CameraEventPanelView.xaml` - 카메라 이벤트 패널

#### **📂 ViewModels**
> MVVM ViewModel

- `PopupShellViewModel.cs` - 팝업 셸 ViewModel
- `CameraDevicePanelViewModel.cs` - 카메라 장치 패널 ViewModel
- `CameraEventPanelViewModel.cs` - 카메라 이벤트 패널 ViewModel

#### **📂 ViewModels/Items**
> 아이템 ViewModel

- `CameraDeviceViewModel.cs` - 카메라 장치 아이템 ViewModel

#### **📂 Services**
> 애플리케이션 서비스

- `ICameraDbService.cs` / `CameraDbService.cs` - 카메라 데이터베이스 서비스 (MySQL)
- `IEventDbService.cs` / `EventDbService.cs` - 이벤트 데이터베이스 서비스 (MySQL)
- `IEventService.cs` / `EventService.cs` - 이벤트 처리 서비스

#### **📂 Models**
> 애플리케이션 모델

- 카메라 및 이벤트 관련 ViewModel 전용 모델

#### **📂 Commands**
> 명령 패턴

- `AsyncRelayCommand.cs` - 비동기 릴레이 커맨드

#### **📂 Behaviors**
> 커스텀 Behavior

- 애플리케이션 전용 Behavior

#### 개발 환경
- **.NET Version**: `net8.0-windows`
- **언어**: `C#`
- **UI Framework**: `WPF`
- **MVVM Framework**: `Caliburn.Micro`
- **Video Player**: `LibVLCSharp`
- **Database**: `MySQL` (MySqlConnector)

---

## 8. 주요 기능

### 8.1 RTSP 스트리밍
- **LibVLCSharp** 기반 RTSP 영상 재생
- **다중 카메라 동시 스트리밍** 지원
- **자동 재연결** 및 **타임아웃 처리**
- **메모리 최적화** (Context Pool, WeakReference)

### 8.2 카메라 관리
- **MySQL DB 기반 카메라 정보 관리**
  - CRUD 작업 (생성, 조회, 수정, 삭제)
  - ConnectionInfo (IP, Port, Username, Password)
  - StreamingOptions (버퍼, 타임아웃, HW 가속)
- **CameraProvider를 통한 In-Memory 캐싱**

### 8.3 이벤트 시스템
- **카메라 이벤트 등록 및 관리**
- **팝업 뷰어를 통한 실시간 이벤트 표시**
- **FIFO 방식 이벤트 처리** (최대 3줄 유지)
- **이벤트 상태 관리** (Idle, Processing, Completed, Cancelled, Failed)

### 8.4 팝업 뷰어
- **투명 윈도우 기반 팝업**
- **위치 설정** (TopLeft, TopCenter, TopRight, MiddleLeft, Center, MiddleRight, BottomLeft, BottomCenter, BottomRight)
- **다중 카메라 행(Row) 관리**
- **자동 닫기 및 FIFO 처리**

### 8.5 Redis 메시지 시스템

#### Generic 메시지 구조
프로젝트는 `Ironwall.Dotnet.Libraries.Messages` 라이브러리의 Generic Type 기반 메시지 시스템을 사용합니다.

```csharp
// 기본 메시지 구조
public class BaseMessage<TBody>
{
    public string id { get; set; }           // 메시지 고유 ID (GUID)
    public string m_type { get; set; }       // 메시지 타입 (REQ/RSP)
    public string cmd { get; set; }          // 명령어 타입
    public string from { get; set; }         // 발신자
    public string target { get; set; }       // 수신자
    public string created { get; set; }      // 생성 시간
    public TBody body { get; set; }          // 메시지 본문 (Generic)
}

// 이벤트 호출 메시지
public class RequestMessage<TBody> : BaseMessage<TBody> { }

// 이벤트 본문
public class EventCallRequestBody
{
    public string event_name { get; set; }   // 이벤트 이름
    public string details { get; set; }      // 상세 설명
    public string state { get; set; }        // 이벤트 상태 (ON/OFF)
}
```

#### 열거형 정의

```csharp
// 메시지 타입
public enum EnumMessageType
{
    REQ,  // 요청
    RSP   // 응답
}

// 명령어 타입
public enum EnumCommandType
{
    EVENT_CALL,      // 이벤트 호출
    STATUS_CHECK,    // 상태 확인
    CONFIG_UPDATE    // 설정 업데이트
}

// 이벤트 상태
public enum EnumEventState
{
    ON,   // 이벤트 시작
    OFF   // 이벤트 종료
}
```

#### Redis 메시지 예제

**이벤트 발생 (ON) 메시지:**
```json
{
    "id": "550e8400-e29b-41d4-a716-446655440001",
    "m_type": "REQ",
    "cmd": "EVENT_CALL",
    "from": "TEST_SYSTEM",
    "target": "rtsp-viewer-0001",
    "created": "2025-10-30 16:20:00.000",
    "body": {
        "event_name": "이벤트1",
        "details": "이벤트1 발생",
        "state": "ON"
    }
}
```

**이벤트 종료 (OFF) 메시지:**
```json
{
    "id": "550e8400-e29b-41d4-a716-446655440001",
    "m_type": "REQ",
    "cmd": "EVENT_CALL",
    "from": "TEST_SYSTEM",
    "target": "rtsp-viewer-0001",
    "created": "2025-10-30 16:20:05.000",
    "body": {
        "event_name": "이벤트1",
        "details": "이벤트1 종료",
        "state": "OFF"
    }
}
```

#### 테스트용 Redis 명령어

```bash
# 이벤트1 - ON
PUBLISH StreamingChannel '{"id":"550e8400-e29b-41d4-a716-446655440001","m_type":"REQ","cmd":"EVENT_CALL","from":"TEST_SYSTEM","target":"rtsp-viewer-0001","created":"2025-10-30 16:20:00.000","body":{"event_name":"이벤트1","details":"이벤트1 발생","state":"ON"}}'

# 이벤트1 - OFF
PUBLISH StreamingChannel '{"id":"550e8400-e29b-41d4-a716-446655440001","m_type":"REQ","cmd":"EVENT_CALL","from":"TEST_SYSTEM","target":"rtsp-viewer-0001","created":"2025-10-30 16:20:05.000","body":{"event_name":"이벤트1","details":"이벤트1 종료","state":"OFF"}}'

# 이벤트2 - ON
PUBLISH StreamingChannel '{"id":"550e8400-e29b-41d4-a716-446655440002","m_type":"REQ","cmd":"EVENT_CALL","from":"TEST_SYSTEM","target":"rtsp-viewer-0001","created":"2025-10-30 16:21:00.000","body":{"event_name":"이벤트2","details":"이벤트2 발생","state":"ON"}}'

# 이벤트2 - OFF
PUBLISH StreamingChannel '{"id":"550e8400-e29b-41d4-a716-446655440002","m_type":"REQ","cmd":"EVENT_CALL","from":"TEST_SYSTEM","target":"rtsp-viewer-0001","created":"2025-10-30 16:21:05.000","body":{"event_name":"이벤트2","details":"이벤트2 종료","state":"OFF"}}'

# 이벤트3 - ON
PUBLISH StreamingChannel '{"id":"550e8400-e29b-41d4-a716-446655440003","m_type":"REQ","cmd":"EVENT_CALL","from":"TEST_SYSTEM","target":"rtsp-viewer-0001","created":"2025-10-30 16:22:00.000","body":{"event_name":"이벤트3","details":"이벤트3 발생","state":"ON"}}'

# 이벤트3 - OFF
PUBLISH StreamingChannel '{"id":"550e8400-e29b-41d4-a716-446655440003","m_type":"REQ","cmd":"EVENT_CALL","from":"TEST_SYSTEM","target":"rtsp-viewer-0001","created":"2025-10-30 16:22:05.000","body":{"event_name":"이벤트3","details":"이벤트3 종료","state":"OFF"}}'
```

**참고사항:**
- 같은 이벤트의 ON/OFF 메시지는 동일한 `id` (GUID)를 사용합니다
- `event_name` 필드로 데이터베이스의 Events 테이블과 매핑됩니다
- `state`가 "ON"일 때 카메라 팝업이 열리고, "OFF"일 때 닫힙니다

---

## 9. 시스템 아키텍처

```
┌─────────────────────────────────────────────────────────┐
│           Dotnet.Rtsp.Viewer.Ui (Main App)              │
│  ┌───────────────┐  ┌──────────────┐  ┌──────────────┐  │
│  │  ViewModels   │  │   Services   │  │    Views     │  │
│  └───────────────┘  └──────────────┘  └──────────────┘  │
└────────────────────────────┬────────────────────────────┘
                             │
        ┌────────────────────┼────────────────────┐
        │                    │                    │
┌───────▼───────┐  ┌─────────▼────────┐  ┌───────▼──────┐
│   Streaming   │  │   ViewModel      │  │    Utils     │
│   Libraries   │  │   Libraries      │  │  Libraries   │
└───────┬───────┘  └─────────┬────────┘  └──────────────┘
        │                    │
┌───────▼────────────────────▼────────┐
│      Base Libraries                 │
│  (Models, Services, Providers)      │
└────────────────┬────────────────────┘
                 │
        ┌────────┼────────┐
        │        │        │
    ┌───▼───┐ ┌─▼──┐ ┌───▼────┐
    │ MySQL │ │Redis│ │LibVLC  │
    └───────┘ └────┘ └────────┘
```

---

## 10. 데이터베이스 스키마

### 10.1 전체 ER 다이어그램

```
┌─────────────────────────────────────────────────────────────┐
│                         Events                               │
├─────────────────────────────────────────────────────────────┤
│ • Id (PK, AUTO_INCREMENT)                                   │
│ • EventName (UNIQUE, VARCHAR(255))                          │
│ • Description (VARCHAR(500))                                │
│ • IsEnable (TINYINT, DEFAULT 1)                             │
│ • CreatedAt (DATETIME)                                      │
│ • UpdatedAt (DATETIME)                                      │
└────────────────────────┬────────────────────────────────────┘
                         │ 1
                         │
                         │ N (EventId FK)
                         │
┌────────────────────────▼────────────────────────────────────┐
│                    EventCameras                              │
├─────────────────────────────────────────────────────────────┤
│ • Id (PK, AUTO_INCREMENT)                                   │
│ • EventId (FK → Events.Id, CASCADE)                         │
│ • CameraGuid (VARCHAR(36))                                  │
│ • CreatedAt (DATETIME)                                      │
└────────────────────────┬────────────────────────────────────┘
                         │
                         │ N (CameraGuid 참조)
                         │
┌────────────────────────▼────────────────────────────────────┐
│                       Cameras                                │
├─────────────────────────────────────────────────────────────┤
│ • Id (PK, AUTO_INCREMENT)                                   │
│ • Guid (UNIQUE, VARCHAR(36))                                │
│ • Title (VARCHAR(255))                                      │
│ • AutoPlay, ShowControls (BOOLEAN)                          │
│ • CreatedAt, UpdatedAt (DATETIME)                           │
└─────────────────────────────────────────────────────────────┘
        │
        ├─────── 1:1 ──────┐
        │                  │
        ▼                  ▼
┌──────────────────┐  ┌─────────────────────┐
│ ConnectionInfo   │  │ StreamingOptions    │
└──────────────────┘  └─────────────────────┘
```

### 10.2 테이블 스키마

#### Events 테이블
```sql
CREATE TABLE IF NOT EXISTS `Events` (
    `Id`             INT AUTO_INCREMENT PRIMARY KEY,
    `EventName`      VARCHAR(255) NOT NULL UNIQUE,
    `Description`    VARCHAR(500),
    `IsEnable`       TINYINT(1) DEFAULT 1 COMMENT '이벤트 활성화 여부 (0: 비활성, 1: 활성)',
    `CreatedAt`      DATETIME DEFAULT CURRENT_TIMESTAMP,
    `UpdatedAt`      DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX `idx_event_name` (`EventName`),
    INDEX `idx_is_enable` (`IsEnable`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
```

**주요 변경사항 (v1.0.0):**
- ✅ `EventName`에 UNIQUE 제약 조건 추가 (Redis 메시지 매핑 키)
- ✅ `IsEnable` 필드 추가 (이벤트 활성화/비활성화 제어)
- ❌ `DeviceGroup` 삭제 (EventGroup 개념 제거)
- ❌ `EventStatus` 삭제 (실시간 Redis 메시지로 대체)
- ❌ `Command` 삭제 (명령어는 Redis 메시지로 처리)

#### EventCameras 테이블 (다대다 관계)
```sql
CREATE TABLE IF NOT EXISTS `EventCameras` (
    `Id`             INT AUTO_INCREMENT PRIMARY KEY,
    `EventId`        INT NOT NULL,
    `CameraGuid`     VARCHAR(36) NOT NULL,
    `CreatedAt`      DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT `FK_EventCameras_Event`
        FOREIGN KEY (`EventId`) REFERENCES `Events` (`Id`)
        ON DELETE CASCADE ON UPDATE CASCADE,
    INDEX `idx_event_id` (`EventId`),
    INDEX `idx_camera_guid` (`CameraGuid`),
    UNIQUE INDEX `uq_event_camera` (`EventId`, `CameraGuid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
```

**특징:**
- Events와 Cameras의 다대다(M:N) 관계를 중개
- 하나의 이벤트에 여러 카메라 연결 가능
- `UNIQUE INDEX`로 중복 매핑 방지
- `ON DELETE CASCADE`로 이벤트 삭제 시 자동 정리

#### Cameras 테이블
```sql
CREATE TABLE IF NOT EXISTS `Cameras` (
    `Id`             INT AUTO_INCREMENT PRIMARY KEY,
    `Guid`           VARCHAR(36) UNIQUE NOT NULL,
    `Title`          VARCHAR(255) NOT NULL,
    `AutoPlay`       BOOLEAN DEFAULT FALSE,
    `ShowControls`   BOOLEAN DEFAULT TRUE,
    `CreatedAt`      DATETIME DEFAULT CURRENT_TIMESTAMP,
    `UpdatedAt`      DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    INDEX `idx_guid` (`Guid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_520_ci;
```

### 10.3 샘플 데이터

#### Events 샘플
```sql
INSERT INTO Events (EventName, Description, IsEnable) VALUES
('이벤트1', '입구 침입 감지 이벤트', 1),
('이벤트2', '주차장 이상 행동 감지', 1),
('이벤트3', '창고 화재 감지 이벤트', 1);
```

#### Cameras 샘플
```sql
INSERT INTO Cameras (Guid, Title, AutoPlay, ShowControls) VALUES
('a1b2c3d4-e5f6-7890-abcd-ef1234567890', 'Front Door Camera', TRUE, FALSE),
('b2c3d4e5-f6g7-8901-bcde-f12345678901', 'Parking Lot Camera', TRUE, FALSE),
('c3d4e5f6-g7h8-9012-cdef-123456789012', 'Warehouse Camera', TRUE, FALSE);
```

#### EventCameras 매핑 샘플
```sql
-- 이벤트1에 카메라 3대 연결
INSERT INTO EventCameras (EventId, CameraGuid) VALUES
(1, 'a1b2c3d4-e5f6-7890-abcd-ef1234567890'),
(1, 'b2c3d4e5-f6g7-8901-bcde-f12345678901'),
(1, 'c3d4e5f6-g7h8-9012-cdef-123456789012');
```

### 10.4 데이터 흐름

**Redis 메시지 → 데이터베이스 매핑:**
1. Redis 메시지의 `event_name` 필드 수신
2. `Events` 테이블에서 `EventName`으로 이벤트 검색
3. `EventCameras` 테이블에서 연결된 `CameraGuid` 목록 조회
4. `Cameras` 테이블에서 실제 카메라 정보 조회
5. 팝업 뷰어에 카메라 스트림 표시

```
Redis Message → EventService → EventDbService
  ↓
Events Table (EventName 매칭)
  ↓
EventCameras Table (EventId → CameraGuid)
  ↓
Cameras Table (Guid → Camera Info)
  ↓
PopupViewerService → 카메라 팝업 표시
```

---

## 11. 개발 환경

### 11.1 필수 요구 사항
- **.NET SDK 8.0** 이상
- **Visual Studio 2022** 이상
- **MySQL Server** (카메라/이벤트 DB)
- **Redis Server** (선택 사항)

### 11.2 주요 NuGet 패키지
- `Caliburn.Micro` - MVVM 프레임워크
- `LibVLCSharp` / `LibVLCSharp.WPF` - 비디오 플레이어
- `Autofac` - DI 컨테이너
- `MySqlConnector` - MySQL DB 연동
- `Polly` - 재시도 정책
- `Newtonsoft.Json` - JSON 직렬화

---

## 12. 시작하기

### 12.1 프로젝트 빌드
```bash
# 솔루션 빌드
dotnet build Dotnet.Rtsp.Viewer.sln

# 애플리케이션 실행
dotnet run --project Dotnet.Rtsp.Viewer.Ui
```

### 12.2 데이터베이스 설정
```sql
-- MySQL 데이터베이스 생성
CREATE DATABASE rtsp_viewer_db;

-- 스키마는 애플리케이션 시작 시 자동 생성됨
```

### 12.3 설정 파일
- `appsettings.json` - 애플리케이션 설정
- `StreamingSetupModel` - 스트리밍 옵션

---

## 13. 변경 이력

### v1.0.0 (2025-10-30)

#### ✨ 주요 기능 추가
- **Redis Generic 메시지 시스템 구현**
  - `BaseMessage<TBody>` 기반 확장 가능한 메시지 구조
  - `RequestMessage<EventCallRequestBody>` 이벤트 호출 메시지
  - `EnumMessageType`, `EnumCommandType`, `EnumEventState` 열거형 추가

- **이벤트 기반 카메라 팝업 시스템**
  - Redis Pub/Sub 메시지 수신 및 처리
  - EventName 기반 간소화된 이벤트-카메라 매핑
  - 실시간 팝업 열기/닫기 (ON/OFF 상태 제어)

#### 🗄️ 데이터베이스 스키마 변경
- **Events 테이블 간소화**
  - `EventName` UNIQUE 제약 조건 추가 (Redis 메시지 매핑 키)
  - `IsEnable` 필드 추가 (이벤트 활성화/비활성화 제어)
  - 불필요한 컬럼 삭제: `DeviceGroup`, `EventStatus`, `Command`

- **EventCameras 테이블**
  - Events와 Cameras 다대다(M:N) 관계 구현
  - UNIQUE INDEX로 중복 매핑 방지
  - CASCADE 제약 조건으로 자동 정리

#### 📚 문서화
- ProjectManual.md 작성 (프로젝트 전체 매뉴얼)
- EventIntegrationGuide.md 작성 (이벤트 통합 가이드)
- README.md 업데이트 (Redis 메시지 시스템, DB 스키마)
- 테스트용 Redis 명령어 샘플 추가

#### 🔧 기술 스택
- .NET 8.0 (Windows)
- WPF + Caliburn.Micro 5.0.258
- Autofac 8.4.0 (DI Container)
- Dapper 2.1.66 (Micro ORM)
- MySql.Data 9.3.0
- MaterialDesignThemes 5.2.1
- Ironwall.Dotnet.Libraries (외부 라이브러리)

---

## 14. 라이선스

Copyright © 2025 Sensorway Co., Ltd. All rights reserved.

---

## 15. 문의

- **Company**: Sensorway Co., Ltd.
- **Department**: SW Team
- **Email**: ghlee@sensorway.co.kr