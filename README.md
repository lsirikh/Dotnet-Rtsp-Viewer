# Dotnet RTSP Viewer

C# / .NET 8 / WPF에서 LibVLCSharp를 이용해 RTSP 영상을 표시하고, 채널별 연결과 재생 상태를 관리하는 프로젝트입니다. 플레이어 컨트롤과 스트리밍 서비스를 분리하고, 여러 화면에서 사용할 재생 컨텍스트를 관리합니다.

## 주요 구현

- `ImprovedRtspPlayer`: WPF 영상 표시 컨트롤
- `ImprovedRtspStreamingService`: 채널 연결·해제, 재생·정지, 음량, 스냅샷과 상태 조회
- 재생 컨텍스트 관리 및 재연결 상태 처리
- `PopupViewer`: 별도 창에서 영상을 표시하는 UI

## 코드 구성

| 경로 | 내용 |
|---|---|
| [Streaming](Ironwall.Dotnet.Libraries.Streaming) | 컨트롤, 스트리밍 서비스, ViewModel |
| [Streaming.Base](Ironwall.Dotnet.Libraries.Streaming.Base) | 스트리밍 공통 모델과 기반 코드 |
| [ProjectManual.md](ProjectManual.md) | 기존 프로젝트 설명 |

서비스 폴더 이름은 소스에 있는 `Serivces`를 사용합니다.

## 개발 환경과 빌드 조건

- Windows, .NET 8 SDK, WPF 개발 도구
- LibVLCSharp / LibVLCSharp.WPF 3.9.4, VideoLAN.LibVLC.Windows 3.0.21
- Autofac, Caliburn.Micro, Polly

현재 솔루션에는 저장소 밖의 `Ironwall.Dotnet.Libraries` 프로젝트 참조가 있습니다. 해당 라이브러리를 준비하고 `.sln`과 `.csproj`의 경로를 맞춰야 합니다.

`Dotnet.Rtsp.Viewer.Ui`는 일반 소스 폴더가 아닌 Git 링크로 등록되어 있지만 `.gitmodules`가 없습니다. 따라서 이 저장소를 clone하는 것만으로 UI 프로젝트 전체를 받거나 솔루션을 바로 빌드할 수는 없습니다. 먼저 UI 저장소 연결과 외부 참조를 복원해야 합니다.

## 구현 범위

이 저장소의 중심은 영상 표시와 재생 상태 관리입니다. 서버에서 영상을 변환·분배하는 코드는 별도 [TranscodingServer](https://github.com/lsirikh/TranscodingServer)에 있습니다. 장시간 연속 운전, 최대 동시 채널 수, 메모리 사용량에 대한 검증 결과는 이 문서에서 보장하지 않습니다.

기존 Sensorway / Ironwall 프로젝트의 라이브러리와 연결되는 코드이며, 포함된 외부 라이브러리의 저작권과 이용 조건은 각각의 원본을 따릅니다.
